using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Models;

namespace FitnessCenterMVC.ModelMapper
{
    public class HallConversions
    {
        public static Hall ConvertToHall(HallViewModel model)
        {
            var hall = new Hall();

            //hall.Id = model.Id; <-- Id is generated in a database
            hall.Capacity = model.Capacity;
            hall.HallMark = model.HallMark;
            hall.FitnessCenterId = model.FitnessCenterId;
            return hall;
        }

        public static HallViewModel ConvertToHallViewModel(Hall model)
        {
            var hallViemModel = new HallViewModel();

            hallViemModel.Id = model.Id;
            hallViemModel.Capacity = model.Capacity;
            hallViemModel.HallMark = model.HallMark;
            hallViemModel.FitnessCenterId = model.FitnessCenterId;
            return hallViemModel;
        }
    }
}
