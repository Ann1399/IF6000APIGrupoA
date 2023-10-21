
using ProyectoAPIGrupoA.Models;

namespace ProyectoAPIGrupoA.Util
{
    public class Utility
    {
        public static List<game> gameList = new List<game>();

        public Utility()
        {

        }
            

        //        public static bool existGameId(string value)
        //        {
        //            bool gameExist=false;
        //            for (int i = 0; i < gameList.Count(); i++)
        //            {
        //                if (gameList[i].GameId == value)
        //                {
        //                    gameExist=true; 
        //                }


        //            }
        //            return gameExist;


        //        }

        //        public static async Task<bool> existOwner(Game game, string value)
        //        {
        //            bool gameExist = false;

        //                if (game.Owner == value)
        //                {
        //                    gameExist = true;
        //                }



        //            return gameExist;


        //        }
        //        public static bool existPlayer(Game game, string value)
        //        {
        //            bool playerExist = false; 
        //            for(int j=0; j < game.Players.Count(); j++)
        //            {
        //                if (game.Players[j] == value)
        //                {
        //                    playerExist=true;
        //                }

        //            }



        //            return playerExist;


        //        }

        //       public static bool verifyPlayersCount(Game game, int count)
        //        {
        //            bool verify = false;

        //            if (game.Players.Count == 5)
        //            {
        //                if (game.Rounds.Count() == 1 && count == 2)
        //                {
        //                    verify = true;
        //                }else if (game.Rounds.Count() == 2 && count == 3)
        //                {
        //                    verify = true;
        //                }else if (game.Rounds.Count() == 3 && count == 2)
        //                {
        //                    verify = true;
        //                }else if (game.Rounds.Count() == 4 && count == 3)
        //                {
        //                    verify = true;
        //                }else if (game.Rounds.Count() == 5 && count == 3)
        //                {
        //                    verify = true;
        //                }

        //            }else if (game.Players.Count == 6)
        //            {
        //                if (game.Rounds.Count() == 1 && count == 2)
        //                {
        //                    verify = true;
        //                }
        //                else if (game.Rounds.Count() == 2 && count == 3)
        //                {
        //                    verify = true;
        //                }
        //                else if (game.Rounds.Count() == 3 && count == 4)
        //                {
        //                    verify = true;
        //                }
        //                else if (game.Rounds.Count() == 4 && count == 3)
        //                {
        //                    verify = true;
        //                }
        //                else if (game.Rounds.Count() == 5 && count == 4)
        //                {
        //                    verify = true;
        //                }

        //            }else if (game.Players.Count == 7)
        //            {
        //                if (game.Rounds.Count() == 1 && count == 2)
        //                {
        //                    verify = true;
        //                }
        //                else if (game.Rounds.Count() == 2 && count == 3)
        //                {
        //                    verify = true;
        //                }
        //                else if (game.Rounds.Count() == 3 && count == 3)
        //                {
        //                    verify = true;
        //                }
        //                else if (game.Rounds.Count() == 4 && count == 4)
        //                {
        //                    verify = true;
        //                }
        //                else if (game.Rounds.Count() == 5 && count == 4)
        //                {
        //                    verify = true;
        //                }

        //            }else if (game.Players.Count >= 8)
        //            {
        //                if (game.Rounds.Count() == 1 && count == 3)
        //                {
        //                    verify = true;
        //                }
        //                else if (game.Rounds.Count() == 2 && count == 4)
        //                {
        //                    verify = true;
        //                }
        //                else if (game.Rounds.Count() == 3 && count == 4)
        //                {
        //                    verify = true;
        //                }
        //                else if (game.Rounds.Count() == 4 && count == 5)
        //                {
        //                    verify = true;
        //                }
        //                else if (game.Rounds.Count() == 5 && count == 5)
        //                {
        //                    verify = true;
        //                }

        //            }

        //            return verify;
        //        }

        //        public static int getRounds(Game game)
        //        {
        //            return game.Rounds.Count()-1;
        //        }

        //        public static bool verifyPlayersExist(Game game, GroupProposal group)
        //        {
        //            bool gameExist = true;
        //                for (int j = 0; j < group.Group.Count(); j++)
        //                {
        //                    if (!game.Players.Contains(group.Group[j]))
        //                    {
        //                        gameExist = false;
        //                    }

        //                }
        //            return gameExist;
        //        }

        //        public static bool password(string value)
        //        {
        //            bool gameExist = false;
        //            for (int i = 0; i < gameList.Count(); i++)
        //            {
        //                if (gameList[i].Password == value)
        //                {
        //                    gameExist = true;
        //                }


        //            }
        //            return gameExist;


        //        }

        //        public static Game getGame(string gameId)
        //        {
        //            Game game= null;
        //            for (int i = 0; i < gameList.Count(); i++)
        //            {
        //                if (gameList[i].GameId == gameId)
        //                {
        //                    game = gameList[i];
        //                }


        //            }
        //            return game;
        //        }

        //        public static bool verifyPlayerSelection(Game game, string name)//Verifica si el jugador ya eligió un camino
        //        {
        //            bool verify = false;
        //            for (int i = 0; i < game.Rounds[getRounds(game)].Group.Count(); i++)
        //            {
        //                if (game.Rounds[getRounds(game)].Group[i].Name.Equals(name))
        //                {
        //                    if (game.Rounds[getRounds(game)].Group[i].Psycho == null)
        //                    {
        //                        verify = true;
        //                    }
        //                }


        //            }
        //            return verify;

        //        }
        //        public static Group GetGroup(Game game, string name)
        //        {
        //            Group group = null;
        //            for (int i = 0; i < game.Rounds[getRounds(game)].Group.Count(); i++)
        //            {
        //                if (game.Rounds[getRounds(game)].Group[i].Name.Equals(name))
        //                {
        //                    group = game.Rounds[getRounds(game)].Group[i];
        //                }


        //            }
        //            return group;

        //        }
        //        public static bool verifyAllGroupSelection(Game game)//Verifica si el grupo ya realizo todas sus elecciones
        //        {
        //            bool verify = false;
        //            int count = 0;
        //            for (int i = 0; i < game.Rounds[getRounds(game)].Group.Count(); i++)
        //            {
        //                if (game.Rounds[getRounds(game)].Group[i].Psycho!=null)
        //                {
        //                    count++;
        //                }


        //            }
        //            if(count == game.Rounds[getRounds(game)].Group.Count())
        //            {
        //                verify = true;
        //            }

        //            return verify;
        //        }
        //        public static bool verifyPsychoWin(Game game)//Verifica si los psycho ganaron la ronda
        //        {
        //            bool verify = false;
        //            int countPsycho = 0;
        //            for (int i = 0; i < game.Rounds[getRounds(game)].Group.Count(); i++)
        //            {
        //                if (game.Rounds[getRounds(game)].Group[i].Psycho == true)
        //                {
        //                    countPsycho++;
        //                }



        //            }
        //            if (countPsycho>0)
        //            {
        //                verify = true;
        //            }

        //            return verify;
        //        }

        //        public static string getRandomLeader(Game game)
        //        {
        //            Random rd = new Random();
        //            int rand_num = rd.Next(0, (game.Players.Count()-1));
        //            return game.Players[rand_num];

        //        }

        //        public static bool verifyGameWinner(Game game)//Verifica si hay un bando ya es ganador
        //        {
        //            bool verify = false;
        //            int countPsycho = 0;
        //            int countExem = 0;
        //            for (int i = 0; i < game.PsychoWin.Count(); i++)
        //            {
        //                if (game.PsychoWin[i] == true)
        //                {
        //                    countPsycho++;
        //                }
        //                else
        //                {
        //                    countExem++;
        //                }



        //            }
        //            if (countPsycho == 3 || countExem == 3)
        //            {
        //                verify = true;
        //            }

        //            return verify;
        //        }

        //        public static bool verifyGroupList(Game game, string name)//Verifica si el jugador pertenece al grupo enviado
        //        {
        //            bool verify = false;

        //            for (int i = 0; i < game.Rounds[getRounds(game)].Group.Count(); i++)
        //            {
        //                if (game.Rounds[getRounds(game)].Group[i].Name.Equals(name))
        //                {
        //                    verify = true;
        //                }



        //            }
        //            return verify;
        //        }
        //        public static bool psychosWin(Game game)//Verifica si el jugador pertenece al grupo enviado
        //        {
        //            bool verify = false;
        //            int countPsycho = 0;
        //            for (int i = 0; i < game.Rounds[getRounds(game)].Group.Count(); i++)
        //            {
        //                if (game.PsychoWin[i].Equals(true))
        //                {
        //                    countPsycho++;
        //                }



        //            }
        //            if (countPsycho==3)
        //            {
        //                verify=true;
        //            }


        //            return verify;
        //        }

        //        public static int getPsychosCount(Game game)
        //        {
        //            if (game.Players.Count() == 5 || game.Players.Count() == 6)
        //            {
        //                return 2;
        //            }else if(game.Players.Count() == 7 || game.Players.Count() == 8 || game.Players.Count() == 9)
        //            {
        //                return 3;
        //            }
        //            else
        //            {
        //                return 4;
        //            }
        //        }

        //        public static bool verifyPlayersCount(Game game)
        //        {
        //            bool verify = true;
        //            if (game.Players.Count() < 5)
        //            {
        //                verify = false;
        //            }
        //            return verify;
        //        }
    }
}
