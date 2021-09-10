using System;

namespace Scheduler
{
    public class Allocation
    {
        public Project Project { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool UntilFinished { get; set; }
        public double Percentage { get; set; }
        public Allocation UntilAllocation { get; set; }
        public Allocation AfterAllocation { get; set; }
    }
    public class AllocationBuilder
    {
        Project _project;
        DateTime? _startDate;
        DateTime? _endDate;
        bool? _untilFinished;
        double? _percentage;
        Allocation _untilAllocation;
        Allocation _afterAllocation;
        private bool ProjectNotSet => _project == null;
        private bool PercentageNotSet => _percentage == null;
        private bool StartMomentUndetermined => _afterAllocation == null && _startDate == null;
        private bool EndMomentUndetermined => _untilAllocation == null && _endDate == null && _untilFinished == null;
        public AllocationBuilder OnProject(Project project)
        {
            _project = project;
            return this;
        }
        public AllocationBuilder StartingOn(DateTime date)
        {
            _startDate = date;
            return this;
        }
        public AllocationBuilder After(Allocation allocation)
        {
            _afterAllocation = allocation;
            return this;
        }
        public AllocationBuilder Until(DateTime date)
        {
            _endDate = date;
            return this;
        }
        public AllocationBuilder Until(Allocation allocation)
        {
            _untilAllocation = allocation;
            return this;
        }
        public AllocationBuilder UntilFinished()
        {
            _untilFinished = true;
            return this;
        }
        public AllocationBuilder ForPercentage(double percentage)
        {
            _percentage = percentage;
            return this;
        }
        public Allocation Build()
        {
            if (ProjectNotSet || PercentageNotSet || StartMomentUndetermined || EndMomentUndetermined)
            {
                throw new InvalidOperationException();
            }
            return new Allocation { Project = _project, StartDate = _startDate, EndDate = _endDate, UntilFinished = _untilFinished ?? false, Percentage = _percentage.Value, UntilAllocation = _untilAllocation, AfterAllocation = _afterAllocation };
        }
    }
}
