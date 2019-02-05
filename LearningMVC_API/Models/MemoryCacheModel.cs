using LearningMVC_API.Models;
using Microsoft.AspNetCore.Identity;

namespace LearningMVC_API.Models
{
    public class MemoryCacheModel
    {
        public string currentusername { get; set; }
        public string cachedusername { get; set; }
    }
}