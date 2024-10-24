
public class Deadline
{
    public DateOnly deadline { get; private set; }
    
    public Deadline(DateOnly deadline)
    {
        this.deadline = deadline;
    }
}