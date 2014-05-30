using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FrontClass.DAL;

namespace FrontClass.Models
{
    public class Classroom
    {
        private List<Step> _availableSteps;
        private Step _currentStep;

        public Module CurrentModule { get; private set; }
        public List<ApplicationUser> Students { get; private set; }
        public List<ApplicationUser> Instructors { get; private set; }

        private FrontClassContext db = new FrontClassContext();

        public Classroom()
        {
            _availableSteps = new List<Step>();
            Students = new List<ApplicationUser>();
            Instructors = new List<ApplicationUser>();
        }

        public IEnumerable<Step> AvailableSteps
        {
            get
            {
                return _availableSteps.OrderBy(s => s.StepId).ToList();
            } 
        }

        public Step CurrentStep
        {
            get { return _currentStep; }
        }

        public void StartModule(int moduleId)
        {
            _availableSteps = new List<Step>();

            var module = db.Modules
                .Include(m => m.Course)
                .First(m => m.ModuleId == moduleId);

            CurrentModule = module;
        }

        public void MakeStepAvailable(int stepId)
        {
            var step = db.Steps.Find(stepId);

            if (!_availableSteps.Contains(step))
            {
                _availableSteps.Add(step);
            }
            _currentStep = step;
        }


        
    }
}