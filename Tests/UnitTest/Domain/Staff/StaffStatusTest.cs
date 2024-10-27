using Xunit;
using TodoApi.Models.Staff;
using System;
namespace TodoApi.Tests.UniTest.Domain.Staff
{
    public class StaffStatusTests
    {
        [Fact]
        public void StaffStatus_ShouldHaveInactiveValue()
        {
            // Verifica se o valor INACTIVE existe e é igual a 0
            Assert.Equal(0, (int)StaffStatus.INACTIVE);
        }

        [Fact]
        public void StaffStatus_ShouldHaveActiveValue()
        {
            // Verifica se o valor ACTIVE existe e é igual a 1
            Assert.Equal(1, (int)StaffStatus.ACTIVE);
        }

        [Fact]
        public void StaffStatus_ShouldContainBothValues()
        {
            // Verifica que ambos os valores estão presentes
            Assert.Contains("INACTIVE", Enum.GetNames(typeof(StaffStatus)));
            Assert.Contains("ACTIVE", Enum.GetNames(typeof(StaffStatus)));
        }
    }
}
