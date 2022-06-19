using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.WorkActivities.Query
{
    public class List
    {
        public class Query : IRequest<Result<List<WorkActivityDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<WorkActivityDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            
            public async Task<Result<List<WorkActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _context.WorkActivities.ToListAsync();
                var workActivities = _mapper.Map<List<WorkActivityDto>>(result);
                
                return Result<List<WorkActivityDto>>.Success(workActivities);
            }
        }
    }

}
