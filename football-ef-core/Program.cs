using football_ef_core.Services;

namespace football_ef_core;

internal class Program
{
    // singleton
    static readonly FootballService Football = FootballService.Instance;

    static async Task Main(string[] args)
    {
        DotNetEnv.Env.TraversePath().Load(); // load the .env file

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Football Manager ===");
            Console.WriteLine("1. Players");
            Console.WriteLine("2. Coaches");
            Console.WriteLine("3. Training sessions");
            Console.WriteLine("0. Exit");

            string choice = GetStringInput("\nChoose");

            switch (choice)
            {
                case "1":
                    await PlayersMenu();
                    break;
                case "2":
                    await CoachesMenu();
                    break;
                case "3":
                    await SessionsMenu();
                    break;
                case "0":
                    return;
            }
        }
    }

    // ========== Players ==========
    static async Task PlayersMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Players ===");
            Console.WriteLine("1. List all");
            Console.WriteLine("2. View one");
            Console.WriteLine("3. Add");
            Console.WriteLine("4. Update");
            Console.WriteLine("5. Delete");
            Console.WriteLine("0. Back");

            string choice = GetStringInput("\nChoose");
            if (choice == "0") return;
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        {
                            var players = await Football.Players.GetAllPlayersAsync();

                            foreach (var p in players)
                            {
                                Console.WriteLine($"[{p.Id}] {p.Name}, age {p.Age}");
                            }

                            if (players.Count == 0) Console.WriteLine("No players yet.");
                            break;
                        }

                    case "2":
                        {
                            var p = await Football.Players.GetPlayerByIdAsync(GetIntInput("Player id"));

                            if (p == null)
                            {
                                Console.WriteLine("Player not found.");
                            }
                            else
                            {
                                Console.WriteLine($"[{p.Id}] {p.Name}, age {p.Age}");

                                foreach (var s in p.TrainingSessions!)
                                {
                                    Console.WriteLine($"  - session: {s.Title}");
                                }
                            }

                            break;
                        }

                    case "3":
                        {
                            var p = await Football.Players.AddPlayerAsync
                                (
                                    GetStringInput("Name"),
                                    GetByteInput("Age (1-50)")
                                );

                            Console.WriteLine($"Added player with id {p.Id}.");
                            break;
                        }

                    case "4":
                        {
                            bool found = await Football.Players.UpdatePlayerAsync
                                (
                                    GetIntInput("Player id"),
                                    GetStringInput("New name"),
                                    GetByteInput("New age (1-50)")
                                );

                            Console.WriteLine(found ? "Player updated." : "Player not found.");
                            break;
                        }

                    case "5":
                        {
                            bool found = await Football.Players.DeletePlayerAsync
                                (
                                    GetIntInput("Player id")
                                );

                            Console.WriteLine(found ? "Player deleted." : "Player not found.");
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.GetBaseException().Message);
            }

            Wait();
        }
    }

    // ========== Coaches ==========
    static async Task CoachesMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Coaches ===");
            Console.WriteLine("1. List all");
            Console.WriteLine("2. View one");
            Console.WriteLine("3. Add");
            Console.WriteLine("4. Update");
            Console.WriteLine("5. Delete");
            Console.WriteLine("0. Back");

            string choice = GetStringInput("\nChoose");
            if (choice == "0") return;
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        {
                            var coaches = await Football.Coaches.GetAllCoachesAsync();

                            foreach (var c in coaches)
                            {
                                Console.WriteLine($"[{c.Id}] {c.Name}");
                            }

                            if (coaches.Count == 0) Console.WriteLine("No coaches found.");
                            break;
                        }

                    case "2":
                        {
                            var c = await Football.Coaches.GetCoachByIdAsync(GetIntInput("Coach id"));

                            if (c == null)
                            {
                                Console.WriteLine("Coach not found.");
                            }
                            else
                            {
                                Console.WriteLine($"[{c.Id}] {c.Name}");

                                foreach (var s in c.TrainingSessions)
                                {
                                    Console.WriteLine($"  - session: {s.Title}");
                                }
                            }

                            break;
                        }

                    case "3":
                        {
                            var c = await Football.Coaches.AddCoachAsync(GetStringInput("Name"));

                            Console.WriteLine($"Added coach with id {c.Id}.");
                            break;
                        }

                    case "4":
                        {
                            bool found = await Football.Coaches.UpdateCoachAsync
                                (
                                    GetIntInput("Coach id"),
                                    GetStringInput("New name")
                                );

                            if (found)
                            {
                                Console.WriteLine("Coach updated.");
                            }
                            else
                            {
                                Console.WriteLine("Coach not found.");
                            }

                            break;
                        }

                    case "5":
                        {
                            bool found = await Football.Coaches.DeleteCoachAsync
                                (
                                    GetIntInput("Coach id")
                                );

                            if (found)
                            {
                                Console.WriteLine("Coach deleted.");
                            }
                            else
                            {
                                Console.WriteLine("Coach not found.");
                            }

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.GetBaseException().Message);
            }

            Wait();
        }
    }

    // ========== Training sessions ==========
    static async Task SessionsMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Training sessions ===");
            Console.WriteLine("1. List all");
            Console.WriteLine("2. View one");
            Console.WriteLine("3. Add");
            Console.WriteLine("4. Update");
            Console.WriteLine("5. Delete");
            Console.WriteLine("6. Add a player to a session");
            Console.WriteLine("7. Remove a player from a session");
            Console.WriteLine("0. Back");

            string choice = GetStringInput("\nChoose");
            if (choice == "0") return;

            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        {
                            var sessions = await Football.TrainingSessions.GetAllTrainingSessionsAsync();

                            foreach (var s in sessions)
                            {
                                Console.WriteLine($"[{s.Id}] {s.Title}, {s.Date:yyyy-MM-dd HH:mm}, coach: {s.Coach.Name}");
                            }
                            if (sessions.Count == 0)
                            {
                                Console.WriteLine("No training sessions yet.");
                            }

                            break;
                        }

                    case "2":
                        {
                            var s = await Football.TrainingSessions.GetTrainingSessionByIdAsync
                                (
                                    GetIntInput("Session id")
                                );

                            if (s == null)
                            {
                                Console.WriteLine("Training session not found.");
                            }
                            else
                            {
                                Console.WriteLine($"[{s.Id}] {s.Title}, {s.Date:yyyy-MM-dd HH:mm}, coach: {s.Coach.Name}");

                                foreach (var p in s.Players)
                                {
                                    Console.WriteLine($"  - player: {p.Name}");
                                }
                            }

                            break;
                        }

                    case "3":
                        {
                            var s = await Football.TrainingSessions.AddTrainingSessionAsync
                                (
                                    GetStringInput("Title"),
                                    GetIntInput("Coach id"),
                                    GetDateInput()
                                );

                            Console.WriteLine($"Added training session with id {s.Id}.");
                            break;
                        }

                    case "4":
                        {
                            bool found = await Football.TrainingSessions.UpdateTrainingSessionAsync
                                (
                                    GetIntInput("Session id"),
                                    GetStringInput("New title"),
                                    GetIntInput("New coach id"),
                                    GetDateInput()
                                );

                            if (found)
                            {
                                Console.WriteLine("Training session updated.");
                            }
                            else
                            {
                                Console.WriteLine("Training session not found.");
                            }

                            break;
                        }

                    case "5":
                        {
                            bool found = await Football.TrainingSessions.DeleteTrainingSessionAsync(GetIntInput("Session id"));

                            if (found)
                            {
                                Console.WriteLine("Training session deleted.");
                            }
                            else
                            {
                                Console.WriteLine("Training session not found.");
                            }

                            break;
                        }

                    case "6":
                        {
                            bool added = await Football.TrainingSessions.AddPlayerToSessionAsync
                                (
                                    GetIntInput("Session id"),
                                    GetIntInput("Player id")
                                );

                            if (added)
                            {
                                Console.WriteLine("Player added to the session.");
                            }
                            else
                            {
                                Console.WriteLine("That player is already in the session.");
                            }

                            break;
                        }

                    case "7":
                        {
                            bool removed = await Football.TrainingSessions.RemovePlayerFromSessionAsync
                                (
                                    GetIntInput("Session id"),
                                    GetIntInput("Player id")
                                );

                            if (removed)
                            {
                                Console.WriteLine("Player removed from the session.");
                            }
                            else
                            {
                                Console.WriteLine("That player is not in the session.");
                            }

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.GetBaseException().Message);
            }

            Wait();
        }
    }

    // ========== helper finctions ==========
    static string GetStringInput(string label)
    {
        Console.Write(label + ": ");

        return Console.ReadLine()?.Trim() ?? "";
    }

    static int GetIntInput(string label)
    {
        int number;

        while (!int.TryParse(GetStringInput(label), out number))
        {
            Console.WriteLine("Please enter a whole number.");
        }

        return number;
    }

    static byte GetByteInput(string label)
    {
        byte number;

        while (!byte.TryParse(GetStringInput(label), out number))
        {
            Console.WriteLine("Please enter a number between 0 and 255.");
        }

        return number;
    }

    // empty input returns null, which will make the system use the default date value, which is current date
    static DateTime? GetDateInput()
    {
        while (true)
        {
            string text = GetStringInput("Date, e.g. 2026-10-14 17:30 (empty = default)");

            if (text == "") return null;

            if (DateTime.TryParse(text, out DateTime date)) return date;

            Console.WriteLine("That is not a valid date.");
        }
    }

    static void Wait()
    {
        Console.Write("\nPress Enter to continue...");
        Console.ReadLine();
    }
}
