using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ProyectoAPIGrupoA.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;
using System.Text.Json.Nodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoAPIGrupoA.Controllers
{
    [Produces("application/json")]
    [Route("/api/games/")]
    public class PublicController : ControllerBase
    {

        ///<summary>
        ///Game Search
        ///</summary>
        ///<remarks>By passing in the appropriate options, you can search for games in the system</remarks>
        /// <param name="name">game property to be used as filter</param>
        /// <param name="status">property value to match with. When empty should return an empty array</param>
        /// <param name="page">game property to be used as filter</param>
        /// <param name="limit">game property to be used as filter</param>
        /// <response code="200">returns all games</response>
        /// 
        [HttpGet]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<GameGet>))]
        public List<game> Get(string? name, string? status, Int32 page, Int32 limit,List<round> result )
        {

        //    if (status == "status")
        //    {
                
                List<game> games = new List<game>();

        //        for (int i = 0; i < Util.Utility.gameList.Count(); i++)
        //        {
        //            if (Util.Utility.gameList[i].Status == status)
        //            {
        //                GameGet game = new GameGet(Util.Utility.gameList[i].GameId, Util.Utility.gameList[i].Name);
        //                games.Add(game);
        //            }


        //        }
        //        return games;

        //    }
        //    else
        //    {
        //        List<GameGet> games = new List<GameGet>();

        //        for (int i = 0; i < Util.Utility.gameList.Count(); i++)
        //        {

        //            GameGet game = new GameGet(Util.Utility.gameList[i].GameId, Util.Utility.gameList[i].Name);
        //            games.Add(game);

        //        }
               return games;
        //    }

        }

        ///<summary>
        ///Game Create
        ///</summary>
        ///<remarks>By passing in the appropriate options, you can create a game.Password is optional</remarks>
        /// <response code="200">Game Created</response>
        ///
        [HttpPost]

        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(errorMessage))] //Agregar el data
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(errorMessage))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(errorMessage))]

        public IActionResult create([FromBody] GameBase gamebase)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(406, new errorMessage("missing game owner or password parameters",406));

            }

            if (gamebase.Name == null || gamebase.Name == "" || gamebase.Name == null || gamebase.Owner == null || gamebase.Owner == "")

            {

                return StatusCode(406, new errorMessage("missing name header or game name parameters",406));

            }

            else{

            game game1 = new game(gamebase.Name, gamebase.Owner, gamebase.Password);      
            Util.Utility.gameList.Add(game1);
            BaseResponse br = new BaseResponse("Game Created!", 201,game1);


                string jsonString = JsonSerializer.Serialize(br);

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
                string idValue3 = (string)customers["Name"]["Id"];
                x3.Remove("Id");
                customers["Name"] = idValue3;

                //copiar Jugadores
                //JObject x4 = (JObject)customers.SelectToken("Players");
                //string idValue4 = (string)customers["Players"]["PlayerName"];
                //x4.Remove("PlayerName");
                //customers["Players"] = idValue4;

                Util.Utility.ConvertirObjetoPlayersAArray(rss);
                Util.Utility.ConvertirPropiedadesAMinuscula(rss);

                return StatusCode(201, rss);

            }

            return null;


        }
        

    }

}
