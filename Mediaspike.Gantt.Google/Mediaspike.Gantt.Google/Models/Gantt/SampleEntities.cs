using System;

namespace Mediaspike.Gantt.Models
{
    public class SampleEntities : IDisposable
    {
        public SampleEntities()
        {
        }

        public object GanttDependencies { get; internal set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}