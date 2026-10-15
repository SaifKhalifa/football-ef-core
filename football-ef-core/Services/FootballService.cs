using football_ef_core.Data;

namespace football_ef_core.Services;

// Singleton: one shared object among the app
internal class FootballService
{
    // created once, the first time the class is used
    public static readonly FootballService Instance = new FootballService();

    public PlayerServices Players { get; }
    public CoachServices Coaches { get; }
    public TrainingSessionServices TrainingSessions { get; }

    // private, so no other human being besides me ;D, can create another one
    private FootballService()
    {
        var factory = new AppDbContextFactory();

        Players = new PlayerServices(factory);
        Coaches = new CoachServices(factory);
        TrainingSessions = new TrainingSessionServices(factory);
    }
}
