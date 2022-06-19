using Application.WorkActivities.Command;
using Application.WorkActivities.Query;
using Domain;

namespace API.Controllers
{
    public class WorkActivityController : BaseApiController
    {  
        [HttpGet]
        public async Task<ActionResult> List()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {
            return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(WorkActivity workActivity)
        {
            return HandleResult(await Mediator.Send(new Create.Command{WorkActivity = workActivity}));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, WorkActivity workActivity)
        {
            workActivity.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command{WorkActivity = workActivity}));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command{Id = id}));
        }
    }
    
}
