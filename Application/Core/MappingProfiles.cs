using Application.WorkActivities;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<WorkActivity, WorkActivityDto>();     // --> List, Details
            CreateMap<WorkActivity, WorkActivity>();    // --> Edit
        }
    }
}
