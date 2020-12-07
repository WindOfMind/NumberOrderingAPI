using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NumberOrdering.Domain;
using Xunit;

namespace NumberOrdering.Tests
{
    public class NumbersServiceUnitTests
    {
        private readonly Mock<INumbersFileRepository> _repository;
        private string _testFilePath = "test";
        private readonly int[] _testArray = {1, 2, 3, 4};

        public NumbersServiceUnitTests()
        {
            _repository = new Mock<INumbersFileRepository>();

            _repository.Setup(r => r.SaveAsync(It.IsAny<int[]>()))
                .ReturnsAsync(_testFilePath);

            _repository.Setup(r => r.LoadAsync(It.IsAny<string>()))
                .ReturnsAsync(_testArray);
        }

        [Fact]
        public async Task SaveAsync_Numbers_ShouldSave()
        {
            // Arrange
            var service = new NumbersService(_repository.Object);
            var array = _testArray;

            // Act
            await service.SaveAsync(array);

            // Assert
            _repository.Verify(r => r.SaveAsync(It.Is<int[]>(a => a == array)), Times.Once);
        }

        [Fact]
        public async Task SaveAsync_EmptyArray_ShouldThrow()
        {
            // Arrange
            var service = new NumbersService(_repository.Object);
            var array = new int[]{};

            // Act
            Func<Task> action = () => service.SaveAsync(array);

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task SaveAsync_NullArray_ShouldThrow()
        {
            // Arrange
            var service = new NumbersService(_repository.Object);
            int[] array = null;

            // Act
            Func<Task> action = () => service.SaveAsync(array);

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetLatestNumbersAsync_NoNumbersSaved_ShouldReturnEmpty()
        {
            // Arrange
            var service = new NumbersService(_repository.Object);

            // Act
            var numbers = await service.GetLatestNumbersAsync();

            // Assert
            numbers.Should().BeEmpty();
        }

        [Fact]
        public async Task GetLatestNumbersAsync_NumbersSaved_ShouldLoadTheLatestFile()
        {
            // Arrange
            var service = new NumbersService(_repository.Object);
            var array = _testArray;
            await service.SaveAsync(array);

            // Act
            await service.GetLatestNumbersAsync();

            // Assert
            _repository.Verify(r => r.LoadAsync(It.Is<string>(s => s == _testFilePath)), Times.Once);
        }

        [Fact]
        public async Task GetLatestNumbersAsync_TwoSetOfNumbersSaved_ShouldLoadTheLatestFile()
        {
            // Arrange
            var array1 = _testArray;
            var array2 = new [] {1, 2, 3, 5, 9, 10};

            _repository.Setup(r => r.SaveAsync(It.Is<int[]>(a => a == array1)))
                .ReturnsAsync("notLatestFilePath");

            _repository.Setup(r => r.SaveAsync(It.Is<int[]>(a => a == array2)))
                .ReturnsAsync("latestFilePath");

            var service = new NumbersService(_repository.Object);


            // Act
            await service.SaveAsync(array1);
            await service.SaveAsync(array2);

            await service.GetLatestNumbersAsync();

            // Assert
            _repository.Verify(r => r.LoadAsync(It.Is<string>(s => s == "latestFilePath")), Times.Once);
        }


    }
}
