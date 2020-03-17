using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ThemeApi.Data.Context;

namespace ThemeApi.Handlers
{
    public class DeleteThemeHandler
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                if (theme == null) throw new Exception("No record found ..");
                _themeDbContext.Remove(theme);
                var success = await _themeDbContext.SaveChangesAsync() > 0;
                if (success) return Unit.Value;
                throw new Exception("Problem of deleting a record, please try it again later ..");
            }
        }
    }
}
