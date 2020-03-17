using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ThemeApi.Data.Context;
using ThemeApi.Domain.Models;

namespace ThemeApi.Handlers
{
    public class SingleThemeHandler
    {
        public class Query : IRequest<Theme>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Theme>
        {
            private readonly ThemeDbContext _themeDbContext;
            public Handler(ThemeDbContext themeDbContext)
            {
                _themeDbContext = themeDbContext;
            }
            public async Task<Theme> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var theme = await _themeDbContext.Themes.FindAsync(request.Id);
                    return theme;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex);
                }
            }
        }
    }
}
