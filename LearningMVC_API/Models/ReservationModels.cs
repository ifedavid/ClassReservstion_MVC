using LearningMVC_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningMVC_API.Models
{
    public class ReservationModels
    {

        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public ClassModel Class { get; set; }
        
    }
}
