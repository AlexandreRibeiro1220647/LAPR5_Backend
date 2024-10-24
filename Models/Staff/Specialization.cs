using TodoApi.Models.Shared;

namespace TodoApi.Models.Staff
{
    public class Specialization
    {
        public String Area { get; private set; }

        public Specialization(String area)
        {
            var allowedSpecializations = new[] { "Dermatology","Neurology", "Cardiology","Orthopedics"  };
            if (!allowedSpecializations.Contains(area))
                throw new ArgumentException("Invalid specialization provided.");

            this.Area = area;
        }

        public void ChangeSpecialization(String area)
        {
            var allowedSpecializations = new[] { "Dermatology","Neurology", "Cardiology","Orthopedics" };
            if (!allowedSpecializations.Contains(area))
                throw new ArgumentException("Invalid specialization provided.");

            this.Area = area;
        }
    }
}