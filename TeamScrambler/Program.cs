using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace TeamScrambler
{
    class Program
    {
        public static string view = "Default";
        public static List<string> activePlayerList = new List<string>();
        public static List<string> playerList = new List<string>();
        public static bool gameover = false;
        public static string file = "players.dat";
        public static string msg = "";

        static void Main(string[] args)
        {
            string[] allPlayers;

            if (File.Exists(file))
            {
                allPlayers = File.ReadAllLines(file);
                foreach(string player in allPlayers)
                {
                    playerList.Add(player);
                }
            } else
            {
                File.Create(file);
            }

            do
            {
                switch (view)
                {
                    case "Default":
                        ShowDefaultView();
                        break;
                    case "AllUsers":
                        ShowAllUsersView();
                        break;
                }
                Console.Clear();
            } while (!gameover);

            Environment.Exit(0);
        }

        static void ShowDefaultView()
        {
            ShowLogo();
            Console.WriteLine("Welcome to Team Scrambler! Here is the skinny on how to use this little utility.");
            Console.WriteLine("To view/edit the player list: edit players");
            Console.WriteLine("To activate/deactivate players type: activate [playername] or deactivate [playername]");
            Console.WriteLine("To mix players between two teams type: mixitup");
            Console.WriteLine("To exit type: exit\n");
            ShowActivePlayers(activePlayerList);
            Console.WriteLine(msg);
            string cmd = Console.ReadLine();
            string[] cmdArray = cmd.Split(' ');
            if (cmdArray[0] == "edit")
            {
                if (cmdArray[1] == "players")
                {
                    view = "AllUsers";
                    return;
                }
            }
            if(cmdArray[0] == "activate" && cmdArray[1] != "")
            {
                activePlayerList.Add(cmdArray[1]);
                return;
            }
            if(cmdArray[0] == "deactivate" && cmdArray[1] != "")
            {
                if (activePlayerList.Contains(cmdArray[1]))
                {
                    activePlayerList.Remove(cmdArray[1]);
                    return;
                }
                return;
            }
            if(cmdArray[0] == "mixitup")
            {
                RandomizePlayers();
                return;
            }
            if (cmdArray[0] == "exit")
            {
                gameover = true;
                return;
            }
            return;
        }

        static void ShowAllUsersView()
        {
            ShowLogo();
            Console.WriteLine("Welcome. Here is the skinny on how to use this little utility.");
            Console.WriteLine("To add or remove players type: add [playername] or remove[playername]");
            Console.WriteLine("To go back to the main menu type: back");
            Console.WriteLine("To exit type: exit\n");
            ShowPlayers(playerList);
            Console.WriteLine(msg);
            string cmd = Console.ReadLine();
            string[] cmdArray = cmd.Split(' ');
            if (cmdArray[0] == "back")
            {
                    view = "Default";
                    return;
            }
            if (cmdArray[0] == "add" && cmdArray[1] != "")
            {
                playerList.Add(cmdArray[1]);
                using (TextWriter tw = new StreamWriter(file))
                {
                    foreach (string player in playerList)
                    {
                        tw.WriteLine(player);
                    }
                }
                return;
            }
            if (cmdArray[0] == "remove" && cmdArray[1] != "")
            {
                if (playerList.Contains(cmdArray[1]))
                {
                    playerList.Remove(cmdArray[1]);
                    using(TextWriter tw = new StreamWriter(file))
                    {
                        foreach(string player in playerList)
                        {
                            tw.WriteLine(player);
                        }
                    }
                    return;
                }
                return;
            }
            if (cmdArray[0] == "exit")
            {
                gameover = true;
            }
            return;
        }

        static void ShowLogo()
        {
            Console.WriteLine("\n\n ████████╗███████╗ █████╗ ███╗   ███╗\n ╚══██╔══╝██╔════╝██╔══██╗████╗ ████║\n    ██║   █████╗  ███████║██╔████╔██║\n    ██║   ██╔══╝  ██╔══██║██║╚██╔╝██║\n    ██║   ███████╗██║  ██║██║ ╚═╝ ██║\n    ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝\n\n ███████╗ ██████╗██████╗  █████╗ ███╗   ███╗██████╗ ██╗     ███████╗██████╗ \n ██╔════╝██╔════╝██╔══██╗██╔══██╗████╗ ████║██╔══██╗██║     ██╔════╝██╔══██╗\n ███████╗██║     ██████╔╝███████║██╔████╔██║██████╔╝██║     █████╗  ██████╔╝\n ╚════██║██║     ██╔══██╗██╔══██║██║╚██╔╝██║██╔══██╗██║     ██╔══╝  ██╔══██╗\n ███████║╚██████╗██║  ██║██║  ██║██║ ╚═╝ ██║██████╔╝███████╗███████╗██║  ██║\n ╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝╚═════╝ ╚══════╝╚══════╝╚═╝  ╚═╝\n\n\n");
        }

        static void ShowPlayers(List<string> players)
        {
            string listed_players = "";
            bool left = true;

            for (int i = 0; i < players.Count; i++)
            {
                if (left)
                {
                    listed_players += "\t{" + i + ",-20}";
                }
                else
                {
                    listed_players += "{" + i + ",-20}\n";
                }
                left = !left;
            }
            Console.WriteLine("\n\tPlayers:");
            Console.WriteLine(listed_players, players.ToArray());
            Console.WriteLine("\n");
        }

        static void ShowActivePlayers(List<string> players)
        {
            string listed_players = "";
            bool left = true;

            for (int i = 0; i < players.Count; i++)
            {
                if (left)
                {
                    listed_players += "\t{" + i + ",-20}";
                }
                else
                {
                    listed_players += "{" + i + ",-20}\n";
                }
                left = !left;
            }
            Console.WriteLine("\n\tActive Players:");
            Console.WriteLine(listed_players, players.ToArray());
            Console.WriteLine("\n");
        }

        static void RandomizePlayers()
        {
            activePlayerList.Shuffle();
        }
    }

    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}