using System;

namespace LearningMVC_API.Data
{
    public class ClassModel
    {

        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        public bool IsDelete { get; set; }
    }
}
