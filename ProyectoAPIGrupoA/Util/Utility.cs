
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using ProyectoAPIGrupoA.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml.Linq;

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
                JProperty jp = new JProperty(nuevaClave, property.Value);
                if (property.Name != jp.Name)
                {
                    property.AddAfterSelf(jp);
                    property.Remove();
                }
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

        public static JObject ConvertirObjetoPlayersAArray(JObject jsonObject, string property)
        {
            JProperty playersProperty = jsonObject.DescendantsAndSelf().OfType<JProperty>()
             .FirstOrDefault(p => p.Name.Equals(property, StringComparison.OrdinalIgnoreCase));

            if (playersProperty != null)
            {
                // Obtener el valor de la propiedad 
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
        public static bool existPlayer(game game, string player)
        {
            bool playerExist = false;
            for (int j = 0; j < game.Players.Count(); j++)
            {
                if (game.Players[j].PlayerName == player)
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


        // verifica los jugadores y las rondas para proponer el grupo
        public static bool verifyPlayersCount(game gameInfo, groupModel playerList, List<JObject> round)
        {
            bool verify = false;

            if (gameInfo.Players.Count == 5)
            {
                if (round.Count == 1 && playerList.group.Count == 2)
                {
                    verify = true;
                }
                else if (round.Count == 2 && playerList.group.Count == 3)
                {
                    verify = true;
                }
                else if (round.Count == 3 && playerList.group.Count == 2)
                {
                    verify = true;
                }
                else if (round.Count == 4 && playerList.group.Count == 3)
                {
                    verify = true;
                }
                else if (round.Count == 5 && playerList.group.Count == 3)
                {
                    verify = true;
                }

            }
            else if (gameInfo.Players.Count == 6)
            {
                if (round.Count == 1 && playerList.group.Count == 2)
                {
                    verify = true;
                }
                else if (round.Count == 2 && playerList.group.Count == 3)
                {
                    verify = true;
                }
                else if (round.Count == 3 && playerList.group.Count == 4)
                {
                    verify = true;
                }
                else if (round.Count == 4 && playerList.group.Count == 3)
                {
                    verify = true;
                }
                else if (round.Count == 5 && playerList.group.Count == 4)
                {
                    verify = true;
                }

            }
            else if (gameInfo.Players.Count == 7)
            {
                if (round.Count == 1 && playerList.group.Count == 2)
                {
                    verify = true;
                }
                else if (round.Count == 2 && playerList.group.Count == 3)
                {
                    verify = true;
                }
                else if (round.Count == 3 && playerList.group.Count == 3)
                {
                    verify = true;
                }
                else if (round.Count == 4 && playerList.group.Count == 4)
                {
                    verify = true;
                }
                else if (round.Count == 5 && playerList.group.Count == 4)
                {
                    verify = true;
                }

            }
            else if (gameInfo.Players.Count == 8)
            {
                if (round.Count == 1 && playerList.group.Count == 3)
                {
                    verify = true;
                }
                else if (round.Count == 2 && playerList.group.Count == 4)
                {
                    verify = true;
                }
                else if (round.Count == 3 && playerList.group.Count == 4)
                {
                    verify = true;
                }
                else if (round.Count == 4 && playerList.group.Count == 5)
                {
                    verify = true;
                }
                else if (round.Count == 5 && playerList.group.Count == 5)
                {
                    verify = true;
                }

            }
            else if (gameInfo.Players.Count == 9)
            {
                if (round.Count == 1 && playerList.group.Count == 3)
                {
                    verify = true;
                }
                else if (round.Count == 2 && playerList.group.Count == 4)
                {
                    verify = true;
                }
                else if (round.Count == 3 && playerList.group.Count == 4)
                {
                    verify = true;
                }
                else if (round.Count == 4 && playerList.group.Count == 5)
                {
                    verify = true;
                }
                else if (round.Count == 5 && playerList.group.Count == 5)
                {
                    verify = true;
                }

            }
            else if (gameInfo.Players.Count == 10)
            {
                if (round.Count == 1 && playerList.group.Count == 3)
                {
                    verify = true;
                }
                else if (round.Count == 2 && playerList.group.Count == 4)
                {
                    verify = true;
                }
                else if (round.Count == 3 && playerList.group.Count == 4)
                {
                    verify = true;
                }
                else if (round.Count == 4 && playerList.group.Count == 5)
                {
                    verify = true;
                }
                else if (round.Count == 5 && playerList.group.Count == 5)
                {
                    verify = true;
                }

            }
            return verify;
        }

        public static List<JObject> getRounds(game game)
        {
            List<JObject> roundL = new List<JObject>();
            var converter = new StringEnumConverter();

            for (int i = 0; i < roundList.Count(); i++)
            {
                if (roundList[i].GameId.Id == game.Id.Id)
                {
                    string json = JsonConvert.SerializeObject(roundList[i], converter);
                    JObject jsonObject = JObject.Parse(json);
                    roundL.Add(jsonObject);
                }          
            }

            return roundL;
        }

        public static void cleanGame(JObject g)
        {
            //copiar RoundId
            JObject x1 = (JObject)g.SelectToken("CurrentRound");
            string idValue1 = (string)g["CurrentRound"]["Id"];
            x1.Remove("Id");
            g["CurrentRound"] = idValue1;

            //copiar Id
            JObject x2 = (JObject)g.SelectToken("Id");
            string idValue2 = (string)g["Id"]["Id"];
            x2.Remove("Id");
            g["Id"] = idValue2;

            //copiar Nombre
            JObject x3 = (JObject)g.SelectToken("Name");
            string idValue3 = (string)g["Name"]["Name"];
            x3.Remove("Name");
            g["Name"] = idValue3;
        }

        public static void cleanRound(JObject g)
        {
            //copiar Id
            JObject x2 = (JObject)g.SelectToken("Id");
            string idValue2 = (string)g["Id"]["Id"];
            x2.Remove("Id");
            g["Id"] = idValue2;

            JObject votesObject = (JObject)g["Votes"];
            JToken roundVotesToken = votesObject.SelectToken("RoundVotes");

            if (roundVotesToken != null)
            {
                List<bool> roundVotes = roundVotesToken.ToObject<List<bool>>();

                // Remueve la propiedad "Votes" existente
                g.Remove("Votes");

                // Agrega una nueva propiedad "votes" con el arreglo de booleanos
                g.Add("votes", new JArray(roundVotes));
            }

        }
        //public static bool verifyPlayersExist(Game game, GroupProposal group)
        //{
        //    bool gameExist = true;
        //    for (int j = 0; j < group.Group.Count(); j++)
        //    {
        //        if (!game.Players.Contains(group.Group[j]))
        //        {
        //            gameExist = false;
        //        }

        //    }
        //    return gameExist;
        //}

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
            
            if(limit == null)
            {
                limit = 50;
            }

            int startIndex = (int)(page * limit.Value);

            // Calcular el índice de final
            int endIndex = startIndex + limit.Value;

            // Filtrar juegos según el estado (si se proporciona)
            var filteredGames = string.IsNullOrEmpty(status)
                ? gameList
                : gameList.Where(game => game.Status.ToString() == status).ToList();

            // Asegurarse de que el índice de inicio no sea mayor que el tamaño de la lista
            if (startIndex >= filteredGames.Count)
            {
                startIndex = filteredGames.Count - 1;
            }

            // Asegurarse de que el índice de final no exceda el tamaño de la lista
            if (endIndex > filteredGames.Count)
            {
                endIndex = filteredGames.Count;
            }
            // Asegurarse de que el índice de inicio esté dentro de los límites
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            // Obtener los juegos de la página actual
            for (int i = startIndex; i < endIndex; i++)
            {
                string json = JsonConvert.SerializeObject(filteredGames[i], converter);
                JObject jsonObject = JObject.Parse(json);
                gameL.Add(jsonObject);
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
        public static List<JObject> getCreateErrors(string? name, string? owner, string? password)
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
        public static List<JObject> getRoundErrors(string gameId, string? password, string player)
        {
            List<JObject> eL = new List<JObject>();
            if (existGameId(gameId) == false)
            {
                errorMessage e = new errorMessage("Game does not exists", 404);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
            }
            if (existGameId(gameId) == true&&(player.Length < 3 || player.Length > 20 || player == null))
            {
                errorMessage e = new errorMessage("Invalid or missing game name", 400);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
            }
            if (getGameId(gameId).Password == true && (password.Length < 3 || password.Length > 20 || getGameId(gameId).Id.Id != password))
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

        public static List<JObject> getSearchErrors(string? name,string? status, Int32? limit, Int32? page)
        {
            List<JObject> eL = new List<JObject>();

            if (name!=null&&(name.Length<3 || name.Length>10 || name == ""))
            {
                errorMessage e = new errorMessage("Invalid or missing game name", 400);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
            }
            if (status != "lobby" && status != "rounds"&& status != "ended")
            {
                errorMessage e = new errorMessage("Invalid game status", 400);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
            }
            if (limit < 0)
            {
                errorMessage e = new errorMessage("Invalid page number", 400);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
            }
            if (page <0)
            {
                errorMessage e = new errorMessage("Invalid limit number", 400);
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

        public static bool verifyGameWinner(game game) //Verifica si hay un bando ya es ganador
        {
            bool verify = false;
            int countCitizen = 0;
            int countEnemies = 0;
            for (int i = 0; i < roundList.Count(); i++)
            {
                if (roundList[i].GameId==game.Id)
                {
                    if (roundList[i].Result == roundResult.citizen)
                    {
                        countCitizen++;
                    }
                    else if (roundList[i].Result == roundResult.enemies)
                    {
                        countEnemies++;
                    }
                }             

            }
            if (countCitizen == 3 || countEnemies == 3)
            {
                verify = true;
            }

            return verify;
        }

        public static bool VerifyGroupList(round r, string name)
        {
            bool verify = r.Group.Any(player => player.PlayerName == name);
            return verify;
        }

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

        static public bool verifyAlreadyVote(string roundId, string player)
        {
            bool verify = false;
            for (int i = 0; i < roundList.Count(); i++)
            {
                if (roundList[i].Id.Id == roundId )
                {
                    if (roundList[i].AlreadyVote.Contains(player))
                    {
                        verify = true;
                    }
                    
                }
            }
            return verify;
        }
        static public void setRoundPhase(round r)
        {
            if(r.Phase == roundPhase.vote1)
            {
                r.Phase = roundPhase.vote2;
            }else if(r.Phase == roundPhase.vote2)
            {
                r.Phase = roundPhase.vote3;
            }else if(r.Phase != roundPhase.vote3)
            {
                r.Phase = roundPhase.vote3;
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
