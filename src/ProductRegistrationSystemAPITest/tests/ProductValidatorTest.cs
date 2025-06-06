using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductRegistrationSystemAPI.data.entities;
using ProductRegistrationSystemAPI.validators;

namespace ProductRegistrationSystemAPITest.tests;

[TestClass]
public class ProductValidatorTest
{
    private ProductValidator _validator = new();

    [TestMethod]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var product = new Product { Price = 10 };
        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.Name);
    }

    [TestMethod]
    public void Should_Have_Error_When_Price_Is_Zero()
    {
        var product = new Product { Name = "Test", Price = 0 };
        var result = _validator.TestValidate(product);
        result.ShouldHaveValidationErrorFor(p => p.Price);
    }
}
