
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProyectoAPIGrupoA.Models;
using System.ComponentModel;
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
        /// <param name="name">filter games where query string is part of the name</param>
        /// <param name="status">filter games based on status</param>
        /// <param name="page">number of records to skip for pagination</param>
        /// <param name="limit">maximum number of records to return</param>
        /// <response code="200">returns all games</response>
        /// 

        [HttpGet]
        [Tags("Public","Players")]
        [Route("/api/games/")]
        //[SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<GameGet>))]
        public ActionResult Get( string? name,  GameStatus? status, [DefaultValue(0)] Int32? page, [DefaultValue(50)]Int32? limit)
        {
            try
            {
                if (name != null && (name.Length < 3 || name.Length > 20 || name == ""))
                {
                    BaseResponse br1 = new BaseResponse("Invalid or missing game name", 400);
                    List<JObject> list1 = Util.Utility.getSearchErrors(name, status.ToString(), limit, page);
                    JObject rs = Util.Utility.errorsToBaseResposne(list1, br1);
                    return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
                }
                if (status != null && (status.ToString() != "lobby" && status.ToString() != "rounds" && status.ToString() != "ended"))
                {
                    BaseResponse br2 = new BaseResponse("Invalid game status", 400);
                    List<JObject> list2 = Util.Utility.getSearchErrors(name, status.ToString(), limit, page);
                    JObject rs = Util.Utility.errorsToBaseResposne(list2, br2);
                    return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
                }
                if (limit < 0)
                {
                    BaseResponse br3 = new BaseResponse("Invalid page number", 400);
                    List<JObject> list3 = Util.Utility.getSearchErrors(name, status.ToString(), limit, page);
                    JObject rs = Util.Utility.errorsToBaseResposne(list3, br3);
                    return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
                }
                if (page < 0)
                {
                    BaseResponse br4 = new BaseResponse("Invalid limit number", 400);
                    List<JObject> list4 = Util.Utility.getSearchErrors(name, status.ToString(), limit, page);
                    JObject rs = Util.Utility.errorsToBaseResposne(list4, br4);
                    return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
                }

                BaseResponse br = new BaseResponse("Games found", 200);
                List<JObject> list = Util.Utility.getGames(name, status.ToString(), page, limit);
                foreach (var g in list)
                {
                    Util.Utility.cleanGame(g);

                    //copiar Jugadores
                    Util.Utility.ConvertirObjetoPlayersAArray(g, "Players");
                    Util.Utility.ConvertirObjetoPlayersAArray(g, "Enemies");
                }
                string jsonString = JsonConvert.SerializeObject(br);

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
            catch (Exception ex)
            {
                // Manejar la excepción según tus requisitos
                Console.WriteLine($"Error en el controlador: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(406, new errorMessage("Invalid Model", 406));

                }

                if (gamebase.Name == null || gamebase.Name == "" || gamebase.Name == null || gamebase.Owner == null || gamebase.Owner == "")

                {

                    return StatusCode(406, new errorMessage("missing name header or game name parameters", 406));

                }
                if (Util.Utility.existGame(gamebase.Name))
                {
                    return StatusCode(409, new errorMessage("Game already exists", 409));
                }
                if (gamebase.Name.Length < 3 || gamebase.Name.Length > 20 || gamebase.Name == null)
                {
                    BaseResponse br1 = new BaseResponse("Invalid or missing game name", 400);
                    List<JObject> list = Util.Utility.getCreateErrors(gamebase.Name, gamebase.Owner, gamebase.Password);
                    JObject rs = Util.Utility.errorsToBaseResposne(list, br1);
                    return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
                }
                if (gamebase.Owner.Length < 3 || gamebase.Owner.Length > 20 || gamebase.Owner == null)
                {
                    BaseResponse br2 = new BaseResponse("Invalid or missing game owner", 400);
                    List<JObject> list = Util.Utility.getCreateErrors(gamebase.Name, gamebase.Owner, gamebase.Password);
                    JObject rs = Util.Utility.errorsToBaseResposne(list, br2);
                    return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
                }
                if (gamebase.Password != null && (gamebase.Password.Length < 3 || gamebase.Password.Length > 20))
                {
                    BaseResponse br3 = new BaseResponse("Invalid password", 400);
                    List<JObject> list = Util.Utility.getCreateErrors(gamebase.Name, gamebase.Owner, gamebase.Password);
                    JObject rs = Util.Utility.errorsToBaseResposne(list, br3);
                    return StatusCode(400, Util.Utility.ConvertirPropiedadesAMinuscula(rs));
                }
                game game1 = new game(gamebase.Name, gamebase.Owner, gamebase.Password);
                //se agrega juego a la lista
                Util.Utility.gameList.Add(game1);

                BaseResponse br = new BaseResponse("Game Created", 201, game1);


                string jsonString = JsonConvert.SerializeObject(br);

                JObject rss = JObject.Parse(jsonString);
                JObject customers = (JObject)rss.SelectToken("Data");
                customers.Remove("UpdatedAt");
                customers.Remove("CreatedAt");

                Util.Utility.cleanGame(customers);

                //copiar Jugadores
                Util.Utility.ConvertirObjetoPlayersAArray(rss, "Players");
                Util.Utility.ConvertirPropiedadesAMinuscula(rss);

                return StatusCode(201, rss);

            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus requisitos
                Console.WriteLine($"Error en el controlador: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }


        }
        

    }

}
