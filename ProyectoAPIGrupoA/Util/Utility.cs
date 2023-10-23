
using Newtonsoft.Json.Linq;
using ProyectoAPIGrupoA.Models;
using System.Text.Json.Nodes;

namespace ProyectoAPIGrupoA.Util
{
    public class Utility
    {
        public static List<game> gameList = new List<game>();

        public Utility()
        {

        }
        public static JObject ConvertirPropiedadesAMinuscula(JObject jsonObject)
        {
            foreach (var property in jsonObject.Properties().ToList())
            {
                string nuevaClave = char.ToLower(property.Name[0]) + property.Name.Substring(1);
                property.AddAfterSelf(new JProperty(nuevaClave, property.Value));
                property.Remove();
            }

            foreach (var property in jsonObject.Properties())
            {
                if (property.Value.Type == JTokenType.Object)
                {
                    ConvertirPropiedadesAMinuscula((JObject)property.Value);
                }
                else if (property.Value.Type == JTokenType.Array)
                {
                    foreach (var item in property.Value.Children())
                    {
                        if (item.Type == JTokenType.Object)
                        {
                            ConvertirPropiedadesAMinuscula((JObject)item);
                        }
                    }
                }
            }
            return jsonObject;
        }

        public static JObject ConvertirObjetoPlayersAArray(JObject jsonObject)
        {
            JProperty playersProperty = jsonObject.DescendantsAndSelf().OfType<JProperty>()
             .FirstOrDefault(p => p.Name.Equals("Players", StringComparison.OrdinalIgnoreCase));

            if (playersProperty != null)
            {
                // Obtener el valor de la propiedad "Players"
                JToken playersValue = playersProperty.Value;

                if (playersValue.Type == JTokenType.Array)
                {
                    // Filtrar objetos "players" y obtener nombres de jugadores
                    var playerNames = playersValue.Children()
                        .Where(player => player["PlayerName"] != null)
                        .Select(player => (string)player["PlayerName"])
                        .ToList();

                    // Reemplazar la propiedad "Players" con un arreglo de nombres de jugadores
                    playersProperty.Value = JArray.FromObject(playerNames);
                }
            }
            return jsonObject;
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
