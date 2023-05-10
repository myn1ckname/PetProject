﻿namespace PetProject.ProductAPI.Domain.Tests;

public class CategoryTests
{
    [InlineData(null)]
    [InlineData("")]
    [Theory]
    public void Category_name_not_null_or_empty(string name)
    {
        Action action = () => new Category(name);
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Category_name_max_lenght_is_valid()
    {
        var name = new string('c', Category.MAX_CATEGORY_LENGHT);
        var sut = new Category(name);
        sut.Value.Should().Be(name);
    }

    [Fact]
    public void Category_name_is_more_max_lenght_is_invalid()
    {
        var name = new string('c', Category.MAX_CATEGORY_LENGHT + 1);
        Action action = () => new Category(name);
        action.Should().Throw<ArgumentException>();
    }
}
