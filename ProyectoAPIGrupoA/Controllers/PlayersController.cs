using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProyectoAPIGrupoA.Models;
using ProyectoIIRedesAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.Json;



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

            //BaseResponse game = new BaseResponse("Games found", 200);
            game gameFound = Util.Utility.getGameId(gameId);

            if ((password.Length < 3 || password.Length > 20 || Util.Utility.getGameId(gameId).Pdw != password) && Util.Utility.getGameId(gameId).Password == true)
            {
                BaseResponse br2 = new BaseResponse("Invalid password", 400);
                List<JObject> list = Util.Utility.getJoinErrors(player, password, gameId);
                JObject rs = Util.Utility.errorsToBaseResposne(list, br2);
                return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }

            gameFound.Players.Add(new gamePlayerName(player));
            BaseResponse br = new BaseResponse("Game Found", 200, gameFound);


            string jsonString = JsonConvert.SerializeObject(br);

            JObject rss = JObject.Parse(jsonString);
            JObject customers = (JObject)rss.SelectToken("Data");

            customers.Remove("CreatedAt");
            customers.Remove("Owner");
            customers.Remove("UpdatedAt");
            customers.Remove("pdw");

            //copiar RoundId
            JObject x1 = (JObject)customers.SelectToken("CurrentRound");
            string idValue1 = (string)customers["CurrentRound"]["Id"];
            x1.Remove("Id");
            customers["CurrentRound"] = idValue1;

            //copiar Id
            JObject x2 = (JObject)customers.SelectToken("Id");
            string idValue2 = (string)customers["Id"]["Id"];
            x2.Remove("Id");
            customers["Id"] = idValue2;

            //copiar Nombre
            JObject x3 = (JObject)customers.SelectToken("Name");
            string idValue3 = (string)customers["Name"]["Name"];
            x3.Remove("Name");
            customers["Name"] = idValue3;

            //copiar Jugadores
            Util.Utility.ConvertirObjetoPlayersAArray(rss);
            Util.Utility.ConvertirPropiedadesAMinuscula(rss);

            return StatusCode(201, rss);

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

            //copiar RoundId
            JObject x1 = (JObject)customers.SelectToken("CurrentRound");
            string idValue1 = (string)customers["CurrentRound"]["Id"];
            x1.Remove("Id");
            customers["CurrentRound"] = idValue1;

            //copiar Id
            JObject x2 = (JObject)customers.SelectToken("Id");
            string idValue2 = (string)customers["Id"]["Id"];
            x2.Remove("Id");
            customers["Id"] = idValue2;

            //copiar Nombre
            JObject x3 = (JObject)customers.SelectToken("Name");
            string idValue3 = (string)customers["Name"]["Name"];
            x3.Remove("Name");
            customers["Name"] = idValue3;

            //copiar Jugadores
            Util.Utility.ConvertirObjetoPlayersAArray(rss);
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
            g.CurrentRound = r.Id;
            g.Status = GameStatus.rounds;
            r.Group = g.Players;
            Response.Headers.Add("status", "200 Game Started");
            return StatusCode(200, "");
        }
        ///<summary>
        ///Get Rounds
        ///</summary>
        [HttpGet]
        [Tags("Players")]
        [Route("/api/games/{gameId}/rounds")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<GameGet>))]
        public ActionResult getRound([Required] string gameId, [FromHeader] string? password, [Required][FromHeader] string player)
        {

            return StatusCode(200, "");
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
            round r = Util.Utility.getRoundId(gameId, roundId);
            return StatusCode(200, r);
        }
        
        ///<summary>
        ///Propose a group
        ///</summary>    
        [HttpPatch]
            [Tags("Players")]
            [Route("/api/games/{gameId}/rounds/{roundId}")]
            public ActionResult proposeGroup([Required] string gameId, [Required] string roundId, [FromHeader] string? password, [Required][FromHeader] string player, [FromBody] string[] group)
            {
                return Ok();
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
                if(r.Votes.RoundVotes.Count() == g.Players.Count()) {
                    int trueCount = r.Votes.RoundVotes.Count(vote => vote);
                    int falseCount = r.Votes.RoundVotes.Count(vote => !vote);
                    if (trueCount > falseCount)
                    {
                        r.Status = roundStatus.waiting_on_group;

                    }else if(trueCount<falseCount && r.Phase == roundPhase.vote3)
                    {
                        r.Result = roundResult.enenmies;
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
            Util.Utility.ConvertirObjetoPlayersAArray(rss);
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
            return StatusCode(200, "hecho");

        }
        public class Vote
        {
            /// <example>false</example>
            public bool vote { get; set; }
        }
        public class Action
        {
            /// <example>false</example>
            public bool action { get; set; }
        }
    }
}