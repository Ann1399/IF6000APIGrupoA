using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ProyectoAPIGrupoA.Models;

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
        //public List<GameGet> Get(string? name, string? status, Int32 page, Int32 limit)
        //{
            
        //    if (status == "status")
        //    {
        //        List<GameGet> games = new List<GameGet>();

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
        //        return games;
        //    }

        //}

        ///<summary>
        ///Game Create
        ///</summary>
        ///<remarks>Request the creation of a new game</remarks>
        ///<param name="game">Game info</param>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(errorMessage))] //Agregar el data
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(errorMessage))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(errorMessage))]
        public IActionResult create([FromBody] GameBase gamebase)
        {
            //if (!ModelState.IsValid)
            //{
            //    return StatusCode(406, new ErrorMsg("missing game owner or password parameters"));

            //}

            //if (name == null || name == "" || game.Name == null || game.Password == null || game.Name == "")

            //{

            //    return StatusCode(406, new ErrorMsg("missing name header or game name parameters"));

            //}

            //else

            //{

            //game game1 = new game(game.Name, game.Owner, game.Password);

            //Util.Utility.gameList.Add(game1);

            //return StatusCode(201, game1);

            //}


            return null;


        }
    }
}
