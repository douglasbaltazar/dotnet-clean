using CleanArch.Application.Products.Commands;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Application.Products.Handlers
{
	public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
	{
		private readonly IProductRepository _productRepository;

		public ProductRemoveCommandHandler(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}
		public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
		{
			var product = await _productRepository.GetById(request.Id);

			if (product == null)
			{
				throw new ApplicationException($"Error could not be found.");
			}
			else
			{
				return await _productRepository.Remove(product);
			}
		}
	}
}
