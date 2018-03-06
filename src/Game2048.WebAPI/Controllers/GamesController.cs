using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Game2048.Core;
using Game2048.Services;
using Game2048.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Game2048.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly GameService _gameService;

        private readonly IMapper _mapper;

        public GamesController(GameService gameService, IMapper mapper)
        {
            this._gameService = gameService;
            this._mapper = mapper;
        }
        
        /// <summary>
        /// Получить игру по уникальному идентификатору
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        [HttpGet("{gameID}")]
        public IActionResult Get(int gameID)
        {
            Game game = _gameService.GetGame(gameID);

            if(game == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameDTO>(game));
        }

        /// <summary>
        /// Создать игру
        /// </summary>
        /// <param name="userID">Пользователь</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromQuery]int userID)
        {
            if(userID <= 0)
            {
                return BadRequest($"Parameter \"{nameof(userID)}\" must be greater than zero");
            }

            Game game = _gameService.CreateGame(userID);
            _gameService.SaveGame(game);

            return Ok(_mapper.Map<GameDTO>(game));
        }

        /// <summary>
        /// Сделать ход
        /// </summary>
        /// <param name="gameID">Уникальный идентификатор игры</param>
        /// <param name="moveInput">Направление хода</param>
        /// <returns></returns>
        [HttpPost("{gameID}/move")]
        public IActionResult Post(int gameID, [FromQuery(Name = "moveType")] string moveInput)
        {
            MoveType moveType;
            if(!TryParseMoveType(moveInput, out moveType))
            {
                return BadRequest($"Invalid \"moveType\" parameters. Valid values = \"{string.Join(",", Enum.GetNames(typeof(MoveType)))}\"");
            }

            Game game = _gameService.GetGame(gameID);

            if (game == null)
            {
                return NotFound();
            }

            game.Move(moveType);

            _gameService.SaveGame(game);

            return Ok(_mapper.Map<GameDTO>(game));
        }

        #region Private methods

        public bool TryParseMoveType(string moveTypeStr, out MoveType moveType)
        {
            return Enum.TryParse(moveTypeStr, out moveType);
        }

        #endregion
    }
}
