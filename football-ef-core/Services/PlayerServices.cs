using football_ef_core.Data;
using football_ef_core.Models;
using Microsoft.EntityFrameworkCore;

namespace football_ef_core.Services;

internal class PlayerServices
{
    private readonly AppDbContextFactory _factory;

    public PlayerServices(AppDbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<Player> AddPlayerAsync(string name, byte age)
    {
        CheckPlayer(name, age);

        using var db = _factory.Create();
        var player = new Player { Name = name.Trim(), Age = age };

        db.Players.Add(player);
        await db.SaveChangesAsync();

        return player;
    }

    public async Task<List<Player>> GetAllPlayersAsync()
    {
        using var db = _factory.Create();
        return await db.Players.ToListAsync();
    }

    // returns null if the player doesn't exist
    public async Task<Player?> GetPlayerByIdAsync(int id)
    {
        using var db = _factory.Create();
        return await db.Players
            .Include(p => p.TrainingSessions)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // returns false if the player doesn't exist
    public async Task<bool> UpdatePlayerAsync(int id, string name, byte age)
    {
        CheckPlayer(name, age);

        using var db = _factory.Create();
        var player = await db.Players.FindAsync(id);
        if (player == null) return false;

        player.Name = name.Trim();
        player.Age = age;
        await db.SaveChangesAsync();

        return true;
    }

    // returns false if the player doesn't exist
    public async Task<bool> DeletePlayerAsync(int id)
    {
        using var db = _factory.Create();
        var player = await db.Players
            .Include(p => p.TrainingSessions) // load them so the join rows are deleted too
            .FirstOrDefaultAsync(p => p.Id == id);

        if (player == null) return false;

        db.Players.Remove(player);
        await db.SaveChangesAsync();

        return true;
    }

    private static void CheckPlayer(string name, byte age)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Trim().Length > 50)
            throw new ArgumentException("Name is required and must be at most 50 characters.");

        if (age < 1 || age > 50)
            throw new ArgumentException("Age must be between 1 and 50.");
    }
}
