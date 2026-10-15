using football_ef_core.Data;
using football_ef_core.Models;
using Microsoft.EntityFrameworkCore;

namespace football_ef_core.Services;

internal class TrainingSessionServices
{
    private readonly AppDbContextFactory _factory;

    public TrainingSessionServices(AppDbContextFactory factory)
    {
        _factory = factory;
    }

    // if date is null, the database fills in its default date
    public async Task<TrainingSession> AddTrainingSessionAsync(string title, int coachId, DateTime? date)
    {
        CheckTitle(title);
        title = title.Trim();

        using var db = _factory.Create();

        if (!await db.Coaches.AnyAsync(c => c.Id == coachId))
            throw new InvalidOperationException("Coach not found.");

        if (await db.TrainingSessions.AnyAsync(s => s.Title == title))
            throw new InvalidOperationException("A training session with this title already exists.");

        var session = new TrainingSession { Title = title, CoachId = coachId };
        if (date != null) session.Date = date.Value;

        db.TrainingSessions.Add(session);
        await db.SaveChangesAsync();

        return session;
    }

    public async Task<List<TrainingSession>> GetAllTrainingSessionsAsync()
    {
        using var db = _factory.Create();
        return await db.TrainingSessions
            .Include(s => s.Coach)
            .ToListAsync();
    }

    // returns null if the session doesn't exist
    public async Task<TrainingSession?> GetTrainingSessionByIdAsync(int id)
    {
        using var db = _factory.Create();
        return await db.TrainingSessions
            .Include(s => s.Coach)
            .Include(s => s.Players)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    // returns false if the session doesn't exist; if date is null the old date is kept
    public async Task<bool> UpdateTrainingSessionAsync(int id, string title, int coachId, DateTime? date)
    {
        CheckTitle(title);
        title = title.Trim();

        using var db = _factory.Create();
        var session = await db.TrainingSessions.FindAsync(id);
        if (session == null) return false;

        if (!await db.Coaches.AnyAsync(c => c.Id == coachId))
            throw new InvalidOperationException("Coach not found.");

        if (await db.TrainingSessions.AnyAsync(s => s.Title == title && s.Id != id))
            throw new InvalidOperationException("A training session with this title already exists.");

        session.Title = title;
        session.CoachId = coachId;
        if (date != null) session.Date = date.Value;

        await db.SaveChangesAsync();

        return true;
    }

    // returns false if the session doesn't exist
    public async Task<bool> DeleteTrainingSessionAsync(int id)
    {
        using var db = _factory.Create();
        var session = await db.TrainingSessions
            .Include(s => s.Players) // load them so the join rows are deleted too
            .FirstOrDefaultAsync(s => s.Id == id);
        if (session == null) return false;

        db.TrainingSessions.Remove(session);
        await db.SaveChangesAsync();

        return true;
    }

    // returns false if the player is already in the session
    public async Task<bool> AddPlayerToSessionAsync(int sessionId, int playerId)
    {
        using var db = _factory.Create();

        var session = await db.TrainingSessions
            .Include(s => s.Players)
            .FirstOrDefaultAsync(s => s.Id == sessionId);
        if (session == null)
            throw new InvalidOperationException("Training session not found.");

        var player = await db.Players.FindAsync(playerId);
        if (player == null)
            throw new InvalidOperationException("Player not found.");

        if (session.Players.Any(p => p.Id == playerId)) return false;

        session.Players.Add(player);
        await db.SaveChangesAsync();

        return true;
    }

    // returns false if the player is not in the session
    public async Task<bool> RemovePlayerFromSessionAsync(int sessionId, int playerId)
    {
        using var db = _factory.Create();

        var session = await db.TrainingSessions
            .Include(s => s.Players)
            .FirstOrDefaultAsync(s => s.Id == sessionId);
        if (session == null)
            throw new InvalidOperationException("Training session not found.");

        var player = session.Players.FirstOrDefault(p => p.Id == playerId);
        if (player == null) return false;

        session.Players.Remove(player);
        await db.SaveChangesAsync();

        return true;
    }

    private static void CheckTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title) || title.Trim().Length > 100)
            throw new ArgumentException("Title is required and must be at most 100 characters.");
    }
}
