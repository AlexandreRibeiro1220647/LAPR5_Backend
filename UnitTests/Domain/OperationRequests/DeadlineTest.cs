using System;
using Xunit;

public class DeadlineTests
{
    [Fact]
    public void Deadline_Should_Set_Deadline_Correctly()
    {
        
        var expectedDeadline = DateOnly.FromDateTime(DateTime.Today);

        var deadline = new Deadline(expectedDeadline);

        Assert.Equal(expectedDeadline, deadline.deadline);
    }
}