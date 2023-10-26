
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using ProyectoAPIGrupoA.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace ProyectoAPIGrupoA.Util
{
    public class Utility
    {
        public static List<game> gameList = new List<game>();
        public static List<round> roundList = new List<round>();
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

        public static bool existGameId(string idGame)
        {
            bool gameExist = false;
            for (int i = 0; i < gameList.Count(); i++)
            {
                gameId id = gameList[i].Id;
                if (id.Id == idGame)
                {
                    gameExist = true;
                }


            }
            return gameExist;


        }

        public static bool existRoundId(string idRound)
        {
            bool roundExist = false;
            for (int i = 0; i < roundList.Count(); i++)
            {
                roundId id = roundList[i].Id;
                if (id.Id == idRound)
                {
                    roundExist = true;
                }

            }
            return roundExist;


        }
        public static async Task<bool> existOwner(game game, string value)
        {
            bool gameExist = false;

            if (game.Owner == value)
            {
                gameExist = true;
            }



            return gameExist;


        }
        public static bool existPlayer(game game, string value)
        {
            bool playerExist = false;
            for (int j = 0; j < game.Players.Count(); j++)
            {
                if (game.Players[j].PlayerName == value)
                {
                    playerExist = true;
                }
            }
            return playerExist;

        }

        public static bool existGame(string value)
        {
            bool gameExist = false;
            for (int i = 0; i < gameList.Count(); i++)
            {
                gameName id = gameList[i].Name;
                if (id.Name == value)
                {
                    gameExist = true;
                }


            }
            return gameExist;

        }



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

        public static game getGameId(string gameId)
        {
            game game=null;
            for (int i = 0; i < gameList.Count(); i++)
            {
                if (gameList[i].Id.Id == gameId)
                {
                    game = gameList[i];
                }


            }
            return game;
        }

        public static round getRoundId(string gameId, string roundId)
        {
            round round = null;
            for (int i = 0; i < roundList.Count(); i++)
            {
                if (roundList[i].GameId.Id == gameId&& roundList[i].Id.Id==roundId)
                {
                    round = roundList[i];
                }
            }
            return round;
        }


        public static List<JObject> getGames(string? name, string? status, Int32? page, Int32? limit)
        {
            List<JObject> gameL = new List<JObject>();
            var converter = new StringEnumConverter();
            int length = 0;
            if(limit == null)
            {
                length = 50;
            }
            for (int i = 0; i < length; i++)
            {
                if (i < gameList.Count())
                {
                    if (name == null && status == "" && page == null && limit == null)
                    {
                        string json = JsonConvert.SerializeObject(gameList[i], converter);
                        JObject jsonObject = JObject.Parse(json);
                        gameL.Add(jsonObject);
                    }
                    else
                    {
                        if (status != null && gameList[i].Status.ToString() == status)
                        {
                            string json = JsonConvert.SerializeObject(gameList[i]);
                            JObject jsonObject = JObject.Parse(json);
                            gameL.Add(jsonObject);
                        }
                        else if (status == null && gameList[i].Status.ToString() == "lobby")
                        {
                            string json = JsonConvert.SerializeObject(gameList[i], converter);
                            JObject jsonObject = JObject.Parse(json);
                            gameL.Add(jsonObject);
                        }
                    }
                }
                else { break; }
            }

            return gameL;
        }

        public static JObject errorsToBaseResposne(List<JObject> l, BaseResponse br)
        {
            string json = JsonConvert.SerializeObject(br);
            JObject jsonObject = JObject.Parse(json);

            JArray dataArray = new JArray();
            foreach (var g in l)
            {
                dataArray.Add(g);
            }
            jsonObject["Others"] = dataArray;
            return jsonObject;
        }

        //errores para Create Game
        public static List<JObject> getErrors(string? name, string? owner, string? password)
        {
            List<JObject> eL = new List<JObject>();
            if (existGame(name))
            {
                errorMessage e = new errorMessage("Game already exists",400);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
            }
            if (name.Length < 3 || name.Length > 20 || name == null)
            {
                errorMessage e = new errorMessage("Invalid or missing game name", 400);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
            }
            if (owner.Length < 3 || owner.Length > 20 || owner == null)
            {
                errorMessage e = new errorMessage("Invalid or missing game owner", 400);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
            }
            if (password.Length < 3 || password.Length > 20)
            {
                errorMessage e = new errorMessage("Invalid password", 400);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
            }
            return eL;
        }

        public static List<JObject> getJoinErrors(string? name, string? password, string id)
        {
            List<JObject> eL = new List<JObject>();
            if (existGame(name))
            {
                errorMessage e = new errorMessage("Game already exists", 400);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
            }
            if (name.Length < 3 || name.Length > 20 || name == null)
            {
                errorMessage e = new errorMessage("Invalid or missing game name", 400);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
            }
            if ((password.Length < 3 || password.Length > 20|| getGameId(id).Id.Id != password)&&getGameId(id).Password==true)
            {
                errorMessage e = new errorMessage("Invalid password", 400);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
            }
            return eL;
        }

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

        public static string getRandomLeader(game game)
        {
            Random rd = new Random();
            int rand_num = rd.Next(0, (game.Players.Count() - 1));
            return game.Players[rand_num].PlayerName;

        }

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

        public static bool VerifyGroupList(round r, string name)
        {
            bool verify = r.Group.Any(player => player.PlayerName == name);
            return verify;
        }

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

    public static int getpsychoscountAtStart(game game)
        {
            if (game.Players.Count() == 5 || game.Players.Count() == 6)
            {
                return 2;
            }
            else if (game.Players.Count() == 7 || game.Players.Count() == 8 || game.Players.Count() == 9)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        public static bool verifyPlayersCount(game game)
        {
            bool verify = true;
            if (game.Players.Count() < 5)
            {
                verify = false;
            }
            return verify;
        }
    }
}
