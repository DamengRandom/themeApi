using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThemeApi.Data.Context;
using ThemeApi.Domain.Models;

namespace ThemeApi.Handlers
{
    public class ReadThemesHandler
    {
        public class Query : IRequest<List<Theme>>{}

        public class Handler : IRequestHandler<Query, List<Theme>>
        {
            private readonly ThemeDbContext _themeDbContext;
            private readonly ILogger<ReadThemesHandler> _logger;

            public Handler(ThemeDbContext themeDbContext, ILogger<ReadThemesHandler> logger)
            {
                _themeDbContext = themeDbContext;
                _logger = logger;
            }

            public async Task<List<Theme>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var themes = await _themeDbContext.Themes.ToListAsync();
                    return themes;
                }
                catch (Exception error)
                {
                    _logger.LogInformation("Task was finished and failed ..");
                    throw new Exception(error + ". Data fetching failed ..");
                }
            }
        }
    }
}
