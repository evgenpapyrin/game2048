using AutoMapper;
using Game2048.Core;
using Game2048.Core.Generators;
using Game2048.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game2048.WebAPI.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddNextGenerator(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<INextGenerator, NextGenerator>();
        }

        public static void AddGameService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<GameService, GameService>();
        }

        public static void AddMapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMapper>(Mapper.Instance);
        }
    }
}
