using TodoApi.Models.Shared;

namespace TodoApi.Models.Staff
{
    public class FullName
    {
        public string fullName { get; private set; }

        public FullName(string fullName)
        {
            this.fullName = fullName;
        }

        public void ChangeFullName(string fullName)
        {
            this.fullName = fullName;
        }
    }
}