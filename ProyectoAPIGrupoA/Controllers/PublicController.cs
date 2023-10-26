
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProyectoAPIGrupoA.Models;
using System.Text.Json;


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
        [Tags("Public","Players")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<GameGet>))]
        public ActionResult Get(string? name, string? status, Int32 page, Int32 limit)
        {
            BaseResponse game = new BaseResponse("Games found",200);
            List<JObject> list = Util.Utility.getGames(name,status,page,limit);
            foreach (var g in list)
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

                //copiar Jugadores
                Util.Utility.ConvertirObjetoPlayersAArray(g);
                //Util.Utility.ConvertirPropiedadesAMinuscula(g);
            }
            string jsonString = JsonConvert.SerializeObject(game);

            JObject rss = JObject.Parse(jsonString);
            JArray dataArray = new JArray();
            foreach (var g in list)
            {
                dataArray.Add(g);
            }
            rss["Data"] = dataArray;
            Util.Utility.ConvertirPropiedadesAMinuscula(rss);
            return StatusCode(200, rss); 

        }

        ///<summary>
        ///Game Create
        ///</summary>
        ///<remarks>By passing in the appropriate options, you can create a game.Password is optional</remarks>
        /// <response code="200">Game Created</response>
        ///
        [HttpPost]
        [Tags("Players", "Public")]
        [Swagger.Net.Annotations.SwaggerResponse(StatusCodes.Status201Created, Type = typeof(errorMessage))] //Agregar el data
        [Swagger.Net.Annotations.SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(errorMessage))]
        [Swagger.Net.Annotations.SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(errorMessage))]

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
            if (Util.Utility.existGame(gamebase.Name))
            {
                return StatusCode(409, new errorMessage("Game already exists", 409));
            }
            if (gamebase.Name.Length < 3 || gamebase.Name.Length > 20 || gamebase.Name == null)
            {
                BaseResponse br = new BaseResponse("Invalid or missing game name",400);
                List<JObject> list = Util.Utility.getErrors(gamebase.Name, gamebase.Owner, gamebase.Password);
                JObject rs = Util.Utility.errorsToBaseResposne(list,br);
                return StatusCode(400,Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }
            if (gamebase.Owner.Length < 3 || gamebase.Owner.Length > 20 || gamebase.Owner == null)
            {
                BaseResponse br = new BaseResponse("Invalid or missing game owner", 400);
                List<JObject> list = Util.Utility.getErrors(gamebase.Name, gamebase.Owner, gamebase.Password);
                JObject rs = Util.Utility.errorsToBaseResposne(list, br);
                return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }
            if (gamebase.Password.Length < 3 || gamebase.Password.Length > 20)
            {
                BaseResponse br = new BaseResponse("Invalid password", 400);
                List<JObject> list = Util.Utility.getErrors(gamebase.Name, gamebase.Owner, gamebase.Password);
                JObject rs = Util.Utility.errorsToBaseResposne(list, br);
                return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
            }
            else
            {

            game game1 = new game(gamebase.Name, gamebase.Owner, gamebase.Password);    
            //se agrega juego a la lista
            Util.Utility.gameList.Add(game1);

            BaseResponse br = new BaseResponse("Game Created", 201,game1);


                string jsonString = JsonConvert.SerializeObject(br);

                JObject rss = JObject.Parse(jsonString); 
                JObject customers = (JObject)rss.SelectToken("Data");
                customers.Remove("UpdatedAt");
                customers.Remove("CreatedAt");
                customers.Remove("Pdw");
                customers.Remove("Owner");

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

            return null;


        }
        

    }

}
