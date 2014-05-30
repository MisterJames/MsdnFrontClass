using System.Linq;
using Microsoft.AspNet.SignalR;

namespace FrontClass.Hubs
{
    public class ClassroomHub : Hub
    {
        [Authorize(Roles = MvcApplication.AdministratorRoleName)]
        public void MakeStepAvailable(int stepId)
        {
            MvcApplication.Classroom.MakeStepAvailable(stepId);

            Clients.All.notifyStepAvailable(stepId);
        }

        [Authorize(Roles = MvcApplication.AdministratorRoleName)]
        public void StartModule(int moduleId)
        {
            MvcApplication.Classroom.StartModule(moduleId);
        }
        
        [Authorize]
        public void GetAvailableSteps()
        {
            var steps = MvcApplication.Classroom.AvailableSteps
                .Select(s => new {s.StepId, s.Title});

            Clients.Caller.notifyAvailableSteps(steps);
        }
    }
}