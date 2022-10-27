using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class Enums
    {
        public enum UserRole
        {
            Administrator,
            Coach,
            FitnessCenterMember
        }

        public enum WorkoutType
        {
            Conditional,
            PowerLifting,
            Bodybuilding
        }

        public enum WorkoutState
        {
            NonCompleted,
            Completed
        }
    }
}
