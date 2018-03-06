using AutoMapper;
using Game2048.Core;
using Game2048.Services;
using Game2048.WebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game2048.WebAPI
{
    public class MappingProfile
        : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDTO>()
                .ForMember(g => g.StatusGame, m => m.MapFrom(g => g.StateGame.ToString()))
                .ForMember(g => g.Board, m => m.MapFrom(g => g.Board.GetTiles().Select(t => Mapper.Map<TileDTO>(t))));

            CreateMap<Tile, TileDTO>();
        }
    }
}
