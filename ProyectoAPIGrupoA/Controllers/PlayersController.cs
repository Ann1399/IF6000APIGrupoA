using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using ProyectoAPIGrupoA.Models;
using ProyectoIIRedesAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using Action = ProyectoAPIGrupoA.Models.Action;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoAPIGrupoA.Controllers
{
    [Produces("application/json")]
    [Route("/api/games/")]

    public class PlayersController : ControllerBase
    {
        ///<summary>
        ///Get Game
        ///</summary>
        ///<remarks>Gets the content of a game. Returns an error if it doesn't exist</remarks>
        /// <param name="gameId"></param>
        /// <param name="password"></param>
        /// <param name="player"></param>
        /// <response code="200">returns all games</response>
        /// 

        [HttpGet]
        [Tags("Players")]
        [Route("/api/games/{gameId}/")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GameGet))]
        public ActionResult Get([Required] string gameId, string password, [Required] string player)
        {
            if (Util.Utility.existGameId(gameId) == false)
            {
                BaseResponse br3 = new BaseResponse("Game does not exists", 404);
                List<JObject> eL = new List<JObject>();
                errorMessage e = new errorMessage("Game does not exists", 404);
                string json = JsonConvert.SerializeObject(e);
                JObject jsonObject = JObject.Parse(json);
                eL.Add(jsonObject);
                JObject rs = Util.Utility.errorsToBaseResposne(eL, br3);
                return StatusCode(404, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }
            if (Util.Utility.getGameId(gameId).Password == true && (password.Length < 3 || password.Length > 20 || Util.Utility.getGameId(gameId).Pdw != password))
            {
                BaseResponse br2 = new BaseResponse("Invalid password", 400);
                List<JObject> list = Util.Utility.getJoinErrors(player, password, gameId);
                JObject rs = Util.Utility.errorsToBaseResposne(list, br2);
                return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }
            if (Util.Utility.getGameId(gameId).Password == false && Util.Utility.existPlayer(Util.Utility.getGameId(gameId), player) == false)
            {
                game gameFound1 = Util.Utility.getGameId(gameId);
                gameFound1.Players.Add(new gamePlayerName(player));
                BaseResponse br4 = new BaseResponse("Player is not part of the game", 401, gameFound1);


                string jsonString1 = JsonConvert.SerializeObject(br4);

                JObject rss1 = JObject.Parse(jsonString1);
                JObject customers1 = (JObject)rss1.SelectToken("Data");

                Util.Utility.cleanGame(customers1);

                //copiar Jugadores
                Util.Utility.ConvertirObjetoPlayersAArray(rss1, "players");

                return StatusCode(401, Util.Utility.ConvertirPropiedadesAMinuscula(rss1));
            }

            game gameFound2 = Util.Utility.getGameId(gameId);
            gameFound2.Players.Add(new gamePlayerName(player));
            BaseResponse br = new BaseResponse("Game Found", 200, gameFound2);


            string jsonString2 = JsonConvert.SerializeObject(br);

            JObject rss2 = JObject.Parse(jsonString2);
            JObject customers2 = (JObject)rss2.SelectToken("Data");

            Util.Utility.cleanGame(customers2);

            //copiar Jugadores
            Util.Utility.ConvertirObjetoPlayersAArray(rss2, "players");

            return StatusCode(201, Util.Utility.ConvertirPropiedadesAMinuscula(rss2));

        }

        ///<summary>
        ///Join Game
        ///</summary>
        [HttpPut]
        [Tags("Players")]
        [Route("/api/games/{gameId}/")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<GameGet>))]
        public ActionResult joinGame([Required] string gameId, [FromHeader] string? password, [Required][FromHeader] string player, [FromBody] gamePlayerName playerb)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(406, new errorMessage("missing game owner or password parameters", 406));
            }
            if (player.Length < 3 || player.Length > 20)
            {
                BaseResponse br1 = new BaseResponse("Invalid or missing game name", 400);
                List<JObject> list = Util.Utility.getJoinErrors(player, password, gameId);
                JObject rs = Util.Utility.errorsToBaseResposne(list, br1);
                return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }
            if ((password.Length < 3 || password.Length > 20 || Util.Utility.getGameId(gameId).Pdw != password) && Util.Utility.getGameId(gameId).Password == true)
            {
                BaseResponse br2 = new BaseResponse("Invalid password", 400);
                List<JObject> list = Util.Utility.getJoinErrors(player, password, gameId);
                JObject rs = Util.Utility.errorsToBaseResposne(list, br2);
                return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }

            game g = Util.Utility.getGameId(gameId);
            g.Players.Add(new gamePlayerName(player));
            BaseResponse br = new BaseResponse("Joinned successfuly", 201, g);


            string jsonString = JsonConvert.SerializeObject(br);

            JObject rss = JObject.Parse(jsonString);
            JObject customers = (JObject)rss.SelectToken("Data");

            Util.Utility.cleanGame(customers);

            //copiar Jugadores
            Util.Utility.ConvertirObjetoPlayersAArray(rss, "players");
            Util.Utility.ConvertirPropiedadesAMinuscula(rss);

            return StatusCode(201, rss);
        }
        ///<summary>
        ///Game Start
        ///</summary>
        [HttpHead]
        [Tags("Players")]
        [Route("/api/games/{gameId}/start/")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<GameGet>))]
        public ActionResult gameStart([Required] string gameId, [FromHeader] string? password, [Required][FromHeader] string player)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(406, new errorMessage("missing game owner or password parameters", 406));
            }
            if (Util.Utility.getGameId(gameId) == null)
            {

                Response.Headers.Add("status", "404 Not Found");
                Response.Headers.Add("x-msg", "Game does not exists");

                return StatusCode(404);
            }
            if (Util.Utility.existPlayer(Util.Utility.getGameId(gameId), player) == false)
            {

                Response.Headers.Add("status", "409 Conflict");
                Response.Headers.Add("x-msg", "Player is already part of the game");

                return StatusCode(409);
            }
            if (Util.Utility.verifyPlayersCount(Util.Utility.getGameId(gameId)) == false)
            {
                Response.Headers.Add("status", "428 Precondition Required ");
                Response.Headers.Add("x-msg", "Need 5 players to start.");

                return StatusCode(428);
            }
            if (Util.Utility.getGameId(gameId).Status.ToString() != "lobby")
            {
                Response.Headers.Add("status", "409 Game already started");
                Response.Headers.Add("x-msg", "Game already started.");

                return StatusCode(409);
            }
            if (Util.Utility.getGameId(gameId).Owner != player)
            {
                Response.Headers.Add("status", "403 Forbidden");
                Response.Headers.Add("x-msg", "You are not the owner of this game");

                return StatusCode(409);
            }

            game g = Util.Utility.getGameId(gameId);
            round r = new round(g.Id);
            int enemiesC = Util.Utility.getpsychoscountAtStart(g);
            int count = 0;
            while (count < enemiesC)
            {
                string enemy = Util.Utility.getRandomLeader(g);
                bool enemyExists = g.Enemies.Any(e => e.PlayerName == enemy);

                if (!enemyExists)
                {
                    g.Enemies.Add(new gamePlayerName(enemy));
                    count++;
                }
            }
            r.GameId = g.Id;
            ///////////////pruebas---eliminar
            //round r2 = new round(g.Id);
            //round r3 = new round(g.Id);
            //Util.Utility.roundList.Add(r2);
            //Util.Utility.roundList.Add(r3);
            //r.Votes.RoundVotes.Add(true);
            //r.Votes.RoundVotes.Add(false);
            //r.Votes.RoundVotes.Add(false);
            ///////////////
            g.CurrentRound = r.Id;
            g.Status = GameStatus.rounds;
            r.Group = g.Players;
            Util.Utility.roundList.Add(r);
            Response.Headers.Add("status", "200 OK");
            Response.Headers.Add("x-msg", "Started successfuly");
            return StatusCode(200, "");
        }
        ///<summary>
        ///Get Rounds
        ///</summary>
        [HttpGet]
        [Tags("Players")]
        [Route("/api/games/{gameId}/rounds")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<GameGet>))]
        public ActionResult getRounds([Required] string gameId, [FromHeader] string? password, [Required][FromHeader] string player)
        {
            if (Util.Utility.existGameId(gameId) == false)
            {
                BaseResponse br1 = new BaseResponse("Game does not exists", 404);
                List<JObject> list1 = Util.Utility.getRoundErrors(gameId, password, player);
                JObject rs = Util.Utility.errorsToBaseResposne(list1, br1);
                return StatusCode(404, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }
            if (Util.Utility.existPlayer(Util.Utility.getGameId(gameId), player) == false)
            {
                BaseResponse br2 = new BaseResponse("Player is not part of the game", 409);
                string jsonString2 = JsonConvert.SerializeObject(br2);
                JObject rss2 = JObject.Parse(jsonString2);
                return StatusCode(409, Util.Utility.ConvertirPropiedadesAMinuscula(rss2));
            }
            if (player.Length < 3 || player.Length > 20 || player == "")
            {
                BaseResponse br3 = new BaseResponse("Invalid or missing player name", 400);
                List<JObject> list3 = Util.Utility.getRoundErrors(gameId, password, player);
                JObject rs = Util.Utility.errorsToBaseResposne(list3, br3);
                return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }
            if (Util.Utility.getGameId(gameId).Password == true && (password.Length < 3 || password.Length > 20 || Util.Utility.getGameId(gameId).Pdw != password))
            {
                BaseResponse br2 = new BaseResponse("Invalid password", 400);
                List<JObject> list2 = Util.Utility.getJoinErrors(player, password, gameId);
                JObject rs = Util.Utility.errorsToBaseResposne(list2, br2);
                return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }
            game g = Util.Utility.getGameId(gameId);
            List<JObject> list = Util.Utility.getRounds(g);
            BaseResponse br = new BaseResponse("Results found", 200, list);

            foreach (var l in list)
            {
                Util.Utility.cleanRound(l);
                Util.Utility.ConvertirObjetoPlayersAArray(l, "Group");
            }
            string jsonString = JsonConvert.SerializeObject(br);

            JObject rss = JObject.Parse(jsonString);
            Util.Utility.ConvertirPropiedadesAMinuscula(rss);
            return StatusCode(200, rss);
        }

        ///<summary>
        ///Show Round
        ///</summary>
        [HttpGet]
        [Tags("Players")]
        [Route("/api/games/{gameId}/rounds/{roundId}")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<GameGet>))]
        public ActionResult showRound([Required] string gameId, [Required] string roundId, [FromHeader] string? password, [Required][FromHeader] string player)
        {

            if (Util.Utility.existGameId(gameId) == false)
            {
                BaseResponse br1 = new BaseResponse("Game does not exists", 404);
                List<JObject> list1 = Util.Utility.getRoundErrors(gameId, password, player);
                JObject rs = Util.Utility.errorsToBaseResposne(list1, br1);
                return StatusCode(404, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }
            if (Util.Utility.existPlayer(Util.Utility.getGameId(gameId), player) == false)
            {
                BaseResponse br2 = new BaseResponse("Player is not part of the game", 409);
                string jsonString2 = JsonConvert.SerializeObject(br2);
                JObject rss2 = JObject.Parse(jsonString2);
                return StatusCode(409, Util.Utility.ConvertirPropiedadesAMinuscula(rss2));
            }
            if (player.Length < 3 || player.Length > 20 || player == "")
            {
                BaseResponse br3 = new BaseResponse("Invalid or missing player name", 400);
                List<JObject> list3 = Util.Utility.getRoundErrors(gameId, password, player);
                JObject rs = Util.Utility.errorsToBaseResposne(list3, br3);
                return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }
            if (Util.Utility.getGameId(gameId).Password == true && (password.Length < 3 || password.Length > 20 || Util.Utility.getGameId(gameId).Pdw != password))
            {
                BaseResponse br2 = new BaseResponse("Invalid password", 400);
                List<JObject> list2 = Util.Utility.getJoinErrors(player, password, gameId);
                JObject rs = Util.Utility.errorsToBaseResposne(list2, br2);
                return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }

            var converter = new StringEnumConverter();
            round r = Util.Utility.getRoundId(gameId, roundId);
            string json = JsonConvert.SerializeObject(r, converter);
            JObject jsonObject = JObject.Parse(json);

            BaseResponse br = new BaseResponse("Results found", 200, jsonObject);


            Util.Utility.cleanRound(jsonObject);
            Util.Utility.ConvertirObjetoPlayersAArray(jsonObject, "Group");

            string jsonString = JsonConvert.SerializeObject(br);

            JObject rss = JObject.Parse(jsonString);
            Util.Utility.ConvertirPropiedadesAMinuscula(rss);
            return StatusCode(200, rss);
        }

        ///<summary>
        ///Propose a group
        ///</summary>    
        [HttpPatch]
        [Tags("Players")]
        [Route("/api/games/{gameId}/rounds/{roundId}")]
        public ActionResult proposeGroup([Required] string gameId, [Required] string roundId, [FromHeader] string? password, [Required][FromHeader] string player, [FromBody] string[] group)
        {

            return StatusCode(200, "");
        }
        /// <summary>
        /// Vote Group
        /// </summary>
        [HttpPost]
        [Tags("Players")]
        [Route("/api/games/{gameId}/rounds/{roundId}")]
        public ActionResult voteGroup([Required] string gameId, [Required] string roundId, [FromHeader] string? password, [Required][FromHeader] string player, [FromBody] Vote vote)
        {

            game g = Util.Utility.getGameId(gameId);
            round r = Util.Utility.getRoundId(gameId, roundId);

            if (Util.Utility.VerifyGroupList(r, player))
            {
                r.Votes.RoundVotes.Add(vote.vote);
                if (r.Votes.RoundVotes.Count() == g.Players.Count())
                {
                    int trueCount = r.Votes.RoundVotes.Count(vote => vote);
                    int falseCount = r.Votes.RoundVotes.Count(vote => !vote);
                    if (trueCount > falseCount)
                    {
                        r.Status = roundStatus.waiting_on_group;

                    }
                    else if (trueCount < falseCount && r.Phase == roundPhase.vote3)
                    {
                        r.Result = roundResult.enemies;
                        round r2 = new round(g.Id);
                        g.CurrentRound = r2.Id;
                    }


                }
            }
            BaseResponse br = new BaseResponse("Game Found", 200, r);


            string jsonString = JsonConvert.SerializeObject(br);

            JObject rss = JObject.Parse(jsonString);
            JObject customers = (JObject)rss.SelectToken("Data");

            //copiar Id
            JObject x2 = (JObject)customers.SelectToken("Id");
            string idValue2 = (string)customers["Id"]["Id"];
            x2.Remove("Id");
            customers["Id"] = idValue2;

            //copiar Jugadores
            Util.Utility.ConvertirObjetoPlayersAArray(rss, "players");
            Util.Utility.ConvertirPropiedadesAMinuscula(rss);
            return StatusCode(200, br);
        }

        ///<summary>
        ///Submit action as member of the Round group
        ///</summary>
        [HttpPut]
        [Tags("Players")]

        [Route("/api/games/{gameId}/rounds/{roundId}")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<GameGet>))]
        public ActionResult submitAction([Required] string gameId, [Required] string roundId, [FromHeader] string? password, [Required][FromHeader] string player, [FromBody] Action action)
        {

            game g = Util.Utility.getGameId(gameId);
            round r = Util.Utility.getRoundId(gameId, roundId);
            r.Actions.Add(action.action);
            if (r.Actions.Count == r.Group.Count())
            {
                if (r.Actions.Any(x => x == false))
                {
                    r.Result = roundResult.enemies;
                }
                else
                {
                    r.Result = roundResult.citizen;
                }
                r.Status = roundStatus.ended;
                round r2 = new round(g.Id);
                g.CurrentRound = r2.Id;

                BaseResponse br = new BaseResponse("round ended", 200, r2);
                return StatusCode(200, r2);
            }
            BaseResponse br2 = new BaseResponse("round ended", 200, r);
            return StatusCode(200, r);

        }
    }
}