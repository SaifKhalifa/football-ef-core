using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace football_ef_core;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        //try { await _db.SaveChangesAsync(ct); }
        //catch (DbUpdateException ex) when (ex.InnerException is MySqlException { Number: 1062 })
        //{
        //    // duplicate title -> return a conflict result
        //}
    }
}
