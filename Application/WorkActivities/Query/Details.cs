using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.WorkActivities.Query
{
    public class Details
    {
        public class Query : IRequest<Result<WorkActivityDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<WorkActivityDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<WorkActivityDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.WorkActivities.FirstOrDefaultAsync(x => x.Id == request.Id);
                
                if (result == null)
                {
                    return Result<WorkActivityDto>.Failure("Work Activity doesn't exist");
                }
                
                var workActivityDto = _mapper.Map<WorkActivityDto>(result);
                
                return Result<WorkActivityDto>.Success(workActivityDto);
            }
        }
    }
}
