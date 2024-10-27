using System;
using TodoApi.Models.Shared;
using Xunit;

public class EntityIdTests
{
    private class TestEntityId : EntityId
    {
        public TestEntityId(string value) : base(value) { }
        public TestEntityId(Guid value) : base(value) { }

        protected override object createFromString(string text) => text;
        public override string AsString() => Value.ToString();
    }

    [Fact]
    public void Equals_WithSameValue_ShouldReturnTrue()
    {
        // Arrange
        var id1 = new TestEntityId("12345");
        var id2 = new TestEntityId("12345");

        // Act & Assert
        Assert.True(id1.Equals(id2));
        Assert.True(id1 == id2);
    }

    [Fact]
    public void CompareTo_WithDifferentValues_ShouldReturnNonZero()
    {
        // Arrange
        var id1 = new TestEntityId("12345");
        var id2 = new TestEntityId("54321");

        // Act & Assert
        Assert.NotEqual(0, id1.CompareTo(id2));
    }
}
