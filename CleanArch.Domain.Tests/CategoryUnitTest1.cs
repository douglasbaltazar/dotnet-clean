using CleanArch.Domain.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArch.Domain.Tests
{
	public class CategoryUnitTest1
	{
		[Fact(DisplayName = "Should create a Category")]
		public void CreateCategory_WithValidParameters_ResultObjectValidState()
		{
			Action action = () => new Category(1, "Category Name");
			action.Should()
				.NotThrow<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}

		[Fact(DisplayName = "Should return Invalid Id Exception")]
		public void CreateCategory_WithNegativeIdValue_DomainExceptionInvalidId()
		{
			Action action = () => new Category(-1, "Category Name");
			action.Should()
				.Throw<CleanArch.Domain.Validation.DomainExceptionValidation>().WithMessage("Invalid id");
		}

		[Fact(DisplayName = "Should return ShortName Exception")]
		public void CreateCategory_ShortNamevalue_DomainExceptionShortName()
		{
			Action action = () => new Category(1, "ca");
			action.Should()
				.Throw<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}
		[Fact(DisplayName = "Should return Required Field Exception")]
		public void CreateCategory_MissingName_DomainExceptionRequiredField()
		{
			Action action = () => new Category(1, "");
			action.Should()
				.Throw<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}
		[Fact(DisplayName = "Should return Invalid name Exception")]
		public void CreateCategory_NameEqualNull_DomainExceptionRequiredField()
		{
			Action action = () => new Category(1, null);
			action.Should()
				.Throw<CleanArch.Domain.Validation.DomainExceptionValidation>();
		}
	}
}
