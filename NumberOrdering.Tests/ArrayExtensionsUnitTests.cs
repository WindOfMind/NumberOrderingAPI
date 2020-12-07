using System;
using System.Collections.Generic;
using FluentAssertions;
using NumberOrdering.Domain.Extensions;
using Xunit;

namespace NumberOrdering.Tests
{
    public class ArrayExtensionsUnitTests
    {
        public static IEnumerable<object[]> GetNumbers()
        {
            yield return new object[] { new int[] {}, new int[] {} };
            yield return new object[] { new [] { 1, 2, 3 }, new [] { 1, 2, 3 } };
            yield return new object[] { new [] { 3, 1, 2, 7 }, new [] { 1, 2, 3, 7 } };
            yield return new object[] { new [] { 3, 5, 1, 3, 3, 1, 2, 7 }, new [] { 1, 1, 2, 3, 3, 3, 5, 7 } };
        }

        [Theory]
        [MemberData(nameof(GetNumbers))]
        public void Sort_InputArray_ShouldSort(int[] array, int[] expected)
        {
            // Arrange

            // Act
            array.Sort();

            // Assert
            array.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Sort_Null_ShouldThrow()
        {
            // Arrange
            int[] array = null;

            // Act
            Action action = () => array.Sort();

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }
    }
}
