using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.WorkActivities.Command
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public WorkActivity WorkActivity { get; set; }
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
                var workActivity = new WorkActivity
                {
                    Description = request.WorkActivity.Description  
                };
                
                _context.WorkActivities.Add(workActivity);
                
                var result = await _context.SaveChangesAsync() > 0;
                
                if (!result)
                {
                    return Result<Unit>.Failure("Work Activity couldn't be created");
                }
                
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
