using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Models;

namespace FitnessCenterMVC.ModelMapper
{
    public class TermConversions
    {
        public static Term ConvertToTerm(TermViewModel model)
        {
            var term = new Term();

            term.WorkoutId = model.WorkoutId;
            term.CoachId = model.CoachId;
            term.FreeSpace = model.FreeSpace;

            return term;
        }

        public static TermViewModel ConvertToTermViewModel(Term model)
        {
            var termViewModel = new TermViewModel();

            termViewModel.WorkoutId = model.WorkoutId;
            termViewModel.CoachId = model.CoachId;
            termViewModel.FreeSpace = model.FreeSpace;

            return termViewModel;
        }
    }
}
