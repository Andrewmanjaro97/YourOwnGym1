using Application.Core;
using MediatR;
using Persistence;

namespace Application.WorkActivities.Command
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var workActivity = await _context.WorkActivities.FindAsync(request.Id);
                
                if (workActivity == null)
                {
                    return Result<Unit>.Failure("The Work Activity doesn't exist");
                }
                
                _context.WorkActivities.Remove(workActivity);
                
                var result = await _context.SaveChangesAsync() > 0;
                
                if (!result)
                {
                    return Result<Unit>.Failure("Work Activity couldn't be deleted");
                }
                
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
