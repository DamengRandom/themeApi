using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ThemeApi.Data.Context;
using ThemeApi.Domain.Models;

namespace ThemeApi.Handlers
{
    public class CreateThemeHandler
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public DateTime CreatedAt { get; set; } // When we created the theme
            public string Name { get; set; } // Theme name
            public string DateForShown { get; set; } // When to use theme
            public string IconImage { get; set; } // Theme icon URL string
            public string MarqueeText { get; set; } // Animated float text
            public string PrimaryColor { get; set; } // Could be background color
            public string SecondaryColor { get; set; } // Could be text color
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ThemeDbContext _themeDbContext;
            public Handler(ThemeDbContext themeDbContext)
            {
                _themeDbContext = themeDbContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var theme = new Theme
                {
                    Id = request.Id,
                    CreatedAt = request.CreatedAt,
                    Name = request.Name,
                    DateForShown = request.DateForShown,
                    IconImage = request.IconImage,
                    MarqueeText = request.MarqueeText,
                    PrimaryColor = request.PrimaryColor,
                    SecondaryColor = request.SecondaryColor
                };

                _themeDbContext.Themes.Add(theme);
                var success = await _themeDbContext.SaveChangesAsync() > 0;
                if (success) return Unit.Value;
                throw new Exception("Problem with saving chnages, please try it again later ..");
            }
        }
    }
}
