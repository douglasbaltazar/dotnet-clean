using CleanArch.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;


namespace CleanArch.Domain.Tests
{
	public class ProductUnitTest1
	{
		[Fact(DisplayName = "Should create a Product")]
		public void CreateProduct_WithValidParameters_ResultObjectValidState()
		{
			Action action = () => new Product(1, "Product", "Description", 10.0m, 10, "http://github.com/douglasbaltazar.png");
			action.Should()
				.NotThrow<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}

		[Fact(DisplayName = "Should return Invalid Id Exception")]
		public void CreateProduct_WithNegativeIdValue_DomainExceptionInvalidId()
		{
			Action action = () => new Product(-1, "Product", "Description", 10.0m, 10, "http://github.com/douglasbaltazar.png");
			action.Should()
				.Throw<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}
		[Fact(DisplayName = "Should return ShortName Exception")]
		public void CreateProduct_ShortNamevalue_DomainExceptionShortName()
		{
			Action action = () => new Product(1, "Pr", "Description", 10.0m, 10, "http://github.com/douglasbaltazar.png");
			action.Should()
				.Throw<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}
		[Fact(DisplayName = "Should return Exception is name is not provided")]
		public void CreateProduct_NameIsNotProvided_DomainExceptionShortName()
		{
			Action action = () => new Product(1, "", "Description", 10.0m, 10, "http://github.com/douglasbaltazar.png");
			action.Should()
				.Throw<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}
		[Fact(DisplayName = "Should return Exception is description is not provided")]
		public void CreateProduct_DescriptionIsNotProvided_DomainExceptionShortName()
		{
			Action action = () => new Product(1, "Produto", "", 10.0m, 10, "http://github.com/douglasbaltazar.png");
			action.Should()
				.Throw<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}
		[Fact(DisplayName = "Should return Short description Exception")]
		public void CreateProduct_ShortDescriptionValue_DomainExceptionShortName()
		{
			Action action = () => new Product(1, "Produto", "Desc", 10.0m, 10, "http://github.com/douglasbaltazar.png");
			action.Should()
				.Throw<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}
		[Fact(DisplayName = "Should return Not Negative Exception if price value provided is negative")]
		public void CreateProduct_WithNegativePriceValue_DomainExceptionInvalidId()
		{
			Action action = () => new Product(1, "Product", "Description", -10.0m, 10, "http://github.com/douglasbaltazar.png");
			action.Should()
				.Throw<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}
		[Fact(DisplayName = "Should return Not Negative Exception if stock value provided is negative")]
		public void CreateProduct_WithNegativeStockValue_DomainExceptionInvalidId()
		{
			Action action = () => new Product(1, "Product", "Description", 10.0m, -10, "http://github.com/douglasbaltazar.png");
			action.Should()
				.Throw<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}
		[Fact(DisplayName = "Should return Image too long Exception if image URI provided is too long")]
		public void CreateProduct_TooLongImage_DomainExceptionInvalidId()
		{
			Action action = () => new Product(1, "Product", "Description", 10.0m, 10, "http://github.com/douglasbaltazar.pnghttp://github.com/douglasbaltazar.pnghttp://github.com/douglasbaltazar.pnghttp://github.com/douglasbaltazar.pnghttp://github.com/douglasbaltazar.pnghttp://github.com/douglasbaltazar.pnghttp://github.com/douglasbaltazar.png");
			action.Should()
				.Throw<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}
		[Fact(DisplayName = "Should not return NoNullReferenceException if image URI provided is equals null")]
		public void CreateProduct_ImageEqualsNull_DomainExceptionInvalidId()
		{
			Action action = () => new Product(1, "Product", "Description", 10.0m, 10, null);
			action.Should()
				.NotThrow<NullReferenceException>();
		}

	}
}
