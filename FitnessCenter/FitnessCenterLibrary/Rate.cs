using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary
{
    internal class Rate
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int CoachId { get; set; }
        public double Rating { get; set; }
    }
}