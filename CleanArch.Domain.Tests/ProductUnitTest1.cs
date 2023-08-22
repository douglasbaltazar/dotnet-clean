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
	}
}
