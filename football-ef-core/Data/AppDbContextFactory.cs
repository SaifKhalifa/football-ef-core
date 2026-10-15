namespace football_ef_core.Data;

internal class AppDbContextFactory
{
    public AppDbContext Create()
    {
        return new AppDbContext();
    }
}
