using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact(DisplayName = "Create Product With Valid State")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product("Product Name", "Product Description", 9.99m, 99, "Product image");
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product With Negative Id Value")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "Product image");

            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id value.");
        }

        [Fact(DisplayName = "Create Product With Name Too Short")]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product("Pr", "Product Description", 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characters");
        }

        [Fact(DisplayName = "Create Product With Long Image Name")]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "Product image tooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo looooooooooooooooooooooooooooooooooooooooooooooooooooooonnnnnggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg");

            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid image name, too long, maximum 250 characters");
        }

        [Fact(DisplayName = "Create Product With Null Image Name")]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product("Product Name", "Product Description", 9.99m, 99, null);
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product With No Null Reference Exception")]
        public void CreateProduct_WithNullImageName_NoNullReferenceException()
        {
            Action action = () => new Product("Product Name", "Product Description", 9.99m, 99, null);
            action.Should()
                .NotThrow<NullReferenceException>();
        }

        [Fact(DisplayName = "Create Product With Empty Image Name")]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product("Product Name", "Product Description", 9.99m, 99, "");
            action.Should()
                .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Product With Invalid Price Value")]
        public void CreateProduct_WithInvalidPriceValue_DomainException()
        {
            Action action = () => new Product("Product Name", "Product Description", -9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid price value");
        }

        [Theory(DisplayName = "Create Product With Invalid Stock Value")]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_DomainExceptionNegativeValue(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, value, "Product image");

            action.Should()
                .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid stock value");
        }
    }
}
