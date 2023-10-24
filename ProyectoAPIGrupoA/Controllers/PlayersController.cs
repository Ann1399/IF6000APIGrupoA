using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ProyectoAPIGrupoA.Models;
using ProyectoIIRedesAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
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
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<GameGet>))]
        public game Get([Required] string gameId, gamePwd? password, [Required] string player)
        {

            BaseResponse game = new BaseResponse("Games found", 200);
            game gameFound = Util.Utility.getGameId(gameId);
            //foreach (var g in list)
            //{

            //    //copiar RoundId
            //    JObject x1 = (JObject)g.SelectToken("CurrentRound");
            //    string idValue1 = (string)g["CurrentRound"]["Id"];
            //    x1.Remove("Id");
            //    g["CurrentRound"] = idValue1;

            //    //copiar Id
            //    JObject x2 = (JObject)g.SelectToken("Id");
            //    string idValue2 = (string)g["Id"]["Id"];
            //    x2.Remove("Id");
            //    g["Id"] = idValue2;

            //    //copiar Nombre
            //    JObject x3 = (JObject)g.SelectToken("Name");
            //    string idValue3 = (string)g["Name"]["Name"];
            //    x3.Remove("Name");
            //    g["Name"] = idValue3;

            //    //copiar Jugadores
            //    Util.Utility.ConvertirObjetoPlayersAArray(g);
            //    Util.Utility.ConvertirPropiedadesAMinuscula(g);
            //}
            //string jsonString = JsonSerializer.Serialize(game);

            //JObject rss = JObject.Parse(jsonString);
            //JArray dataArray = new JArray();
            //foreach (var g in list)
            //{
            //    dataArray.Add(g);
            //}
            //rss["Data"] = dataArray;
            //Util.Utility.ConvertirPropiedadesAMinuscula(rss);
            //return StatusCode(406, rss); ;
            return gameFound;

        }


    }
}
