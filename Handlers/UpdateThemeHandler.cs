using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ThemeApi.Data.Context;

namespace ThemeApi.Handlers
{
    public class UpdateThemeHandler
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
                var theme = await _themeDbContext.Themes.FindAsync(request.Id);

                if (theme == null) throw new Exception("No record can be found ..");

                theme.Name = request.Name ?? theme.Name;
                theme.DateForShown = request.DateForShown ?? theme.DateForShown;
                theme.IconImage = request.IconImage ?? theme.IconImage;
                theme.MarqueeText = request.MarqueeText ?? theme.MarqueeText;
                theme.PrimaryColor = request.PrimaryColor ?? theme.PrimaryColor;
                theme.SecondaryColor = request.SecondaryColor ?? theme.SecondaryColor;

                var success = await _themeDbContext.SaveChangesAsync() > 0;
                if (success) return Unit.Value;
                throw new Exception("Problem of saving changes, please try it again later ..");
            }
        }
    }
}
