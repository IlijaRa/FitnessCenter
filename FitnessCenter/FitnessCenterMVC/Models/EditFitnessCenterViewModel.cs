namespace FitnessCenterMVC.Models
{
    public class EditFitnessCenterViewModel
    {
        public FitnessCenterViewModel fitnessCenter { get; set; }
        public List<HallViewModel> halls { get; set; } = new List<HallViewModel>();
    }
}
