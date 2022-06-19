using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.WorkActivities.Command
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public WorkActivity WorkActivity { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var workActivity = await _context.WorkActivities.FindAsync(request.WorkActivity.Id);

                if (workActivity == null)
                {
                    return Result<Unit>.Failure("The Work Activity doesn't exist");
                }

                _mapper.Map(request.WorkActivity, workActivity);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Work Activity couldn't be updated");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
