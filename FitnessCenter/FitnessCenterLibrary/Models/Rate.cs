using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class Rate
    {
        public Guid Id { get; set; }
        public int MemberId { get; set; }
        public int CoachId { get; set; }
        public double Rating { get; set; }
    }
}