using football_ef_core.Data;
using football_ef_core.Models;
using Microsoft.EntityFrameworkCore;

namespace football_ef_core.Services;

internal class CoachServices
{
    private readonly AppDbContextFactory _factory;

    public CoachServices(AppDbContextFactory factory)
    {
        _factory = factory;
    }

    public async Task<Coach> AddCoachAsync(string name)
    {
        CheckName(name);

        using var db = _factory.Create();
        var coach = new Coach { Name = name.Trim() };

        db.Coaches.Add(coach);
        await db.SaveChangesAsync();

        return coach;
    }

    public async Task<List<Coach>> GetAllCoachesAsync()
    {
        using var db = _factory.Create();
        return await db.Coaches.ToListAsync();
    }

    // returns null if the coach doesn't exist
    public async Task<Coach?> GetCoachByIdAsync(int id)
    {
        using var db = _factory.Create();
        return await db.Coaches
            .Include(c => c.TrainingSessions)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    // updates the whole coach profile (name + monthly salary)
    // returns false if the coach doesn't exist
    public async Task<bool> UpdateCoachAsync(int id, string name, decimal salary)
    {
        CheckName(name);
        CheckSalary(salary);

        using var db = _factory.Create();
        var coach = await db.Coaches.FindAsync(id);
        if (coach == null) return false;

        coach.Name = name.Trim();
        coach.Salary = salary;
        await db.SaveChangesAsync();

        return true;
    }

    // returns false if the coach doesn't exist
    public async Task<bool> DeleteCoachAsync(int id)
    {
        using var db = _factory.Create();
        var coach = await db.Coaches.FindAsync(id);
        if (coach == null) return false;

        // the database refuses to delete a coach that still has sessions, so check first
        if (await db.TrainingSessions.AnyAsync(s => s.CoachId == id))
            throw new InvalidOperationException("This coach still has training sessions. Delete or reassign them first.");

        db.Coaches.Remove(coach);
        await db.SaveChangesAsync();

        return true;
    }

    private static void CheckSalary(decimal salary)
    {
        // the column is decimal(10,2), so it holds up to 99,999,999.99
        if (salary < 0 || salary > 99999999.99m)
            throw new ArgumentException("Salary must be between 0 and 99,999,999.99.");
    }

    private static void CheckName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Trim().Length > 50)
            throw new ArgumentException("Name is required and must be at most 50 characters.");
    }
}
