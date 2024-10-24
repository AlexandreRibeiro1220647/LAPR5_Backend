using TodoApi.Models.Shared;

namespace TodoApi.Models.Staff;

public class LastName
{
    public String lastName { get; private set; }

    public LastName(String lastName)
    {
        this.lastName = lastName;
    }

    public void changeLastName(String lastName)
    {
        this.lastName = lastName;
    }
}