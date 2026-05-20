using Hosts.BusinessLogic;
using Hosts.WPF.ViewModels;
using Xunit;

namespace Hosts.Tests
{
    public class WebsiteViewModelTests
    {
        [Fact]
        public void Constructor_WithValidWebsite_CalculatesDiskUsageCorrectly()
        {
            // Arrange
            var plan = new HostingPlan
            {
                Id = 1,
                Name = "Тестовый тариф",
                DiskSpaceGB = 10,
                Price = 500,
                SiteLimit = 5
            };

            var website = new Website
            {
                Id = 1,
                Domain = "test.com",
                Status = "Active",
                Plan = plan,
                PlanId = plan.Id,
                UsedDiskMB = 2048 // 2 GB
            };

            // Act
            var vm = new WebsiteViewModel(website);

            // Assert
            Assert.Equal("test.com", vm.Domain);
            Assert.Equal("Тестовый тариф", vm.PlanName);
            Assert.Equal("Active", vm.Status);
            Assert.Equal(20, vm.DiskUsagePercent); // 2048/10240 = 0.2 = 20%
            Assert.Equal("2048 / 10 GB", vm.DiskUsageText);
        }

        [Fact]
        public void Constructor_WhenPlanIsNull_UsesDefaultValues()
        {
            // Arrange
            var website = new Website
            {
                Id = 1,
                Domain = "test.com",
                Status = "Active",
                Plan = null,
                UsedDiskMB = 500
            };

            // Act
            var vm = new WebsiteViewModel(website);

            // Assert
            Assert.Equal("Без тарифа", vm.PlanName);
            Assert.Equal(0, vm.DiskUsagePercent);
            Assert.Equal("500 / 0 GB", vm.DiskUsageText);
        }

        [Fact]
        public void Constructor_WithZeroDiskSpace_ReturnsZeroPercent()
        {
            // Arrange
            var plan = new HostingPlan
            {
                Id = 1,
                Name = "Без диска",
                DiskSpaceGB = 0,
                Price = 0,
                SiteLimit = 1
            };

            var website = new Website
            {
                Id = 1,
                Domain = "test.com",
                Status = "Active",
                Plan = plan,
                UsedDiskMB = 500
            };

            // Act
            var vm = new WebsiteViewModel(website);

            // Assert
            // При DiskSpaceGB = 0, процент должен быть 0 (чтобы избежать деления на ноль)
            Assert.Equal(0, vm.DiskUsagePercent);
        }

        [Fact]
        public void Constructor_WithHighDiskUsage_CalculatesCorrectPercent()
        {
            // Arrange
            var plan = new HostingPlan
            {
                Id = 1,
                Name = "Тестовый тариф",
                DiskSpaceGB = 5,
                Price = 500,
                SiteLimit = 5
            };

            var website = new Website
            {
                Id = 1,
                Domain = "test.com",
                Status = "Active",
                Plan = plan,
                PlanId = plan.Id,
                UsedDiskMB = 4600 // 4.6 GB (92%)
            };

            // Act
            var vm = new WebsiteViewModel(website);

            // Assert
            double expected = (4600.0 / (5 * 1024)) * 100;
            Assert.Equal(expected, vm.DiskUsagePercent, 2); // tolerance 2 decimal places
            Assert.Equal("4600 / 5 GB", vm.DiskUsageText);
        }
    }
}