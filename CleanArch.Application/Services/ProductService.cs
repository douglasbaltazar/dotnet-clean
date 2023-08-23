using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Products.Queries;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
	public class ProductService : IProductService
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;
		public ProductService(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		public async Task Add(ProductDTO productDto)
		{
			var productEntity = _mapper.Map<Product>(productDto);
			await _productRepository.Create(productEntity);
		}

		public async Task<ProductDTO> GetById(int? id)
		{
			var productEntity = await _productRepository.GetById(id);
			return _mapper.Map<ProductDTO>(productEntity);
		}

		public async Task<ProductDTO> GetProductCategory(int? id)
		{
			var productEntity = await _productRepository.GetProductCategoryAsync(id);
			return _mapper.Map<ProductDTO>(productEntity);
		}

		public async Task<IEnumerable<ProductDTO>> GetProducts()
		{
			var productsQuery = new GetProductsQuery();

			if(productsQuery == null)
			{
				throw new ApplicationHandleException($"Entity could not be loaded.");
			}
			var result = await _mediator.Send(productsQuery);
			// var productsEntity = await _productRepository.GetProductsAsync();
			return _mapper.Map<IEnumerable<ProductDTO>>(result);
		}

		public async Task Remove(int? id)
		{
			var productEntity = await _productRepository.GetById(id);
			await _productRepository.Remove(productEntity);
		}

		public async Task Update(ProductDTO productDto)
		{
			var productEntity = _mapper.Map<Product>(productDto);
			await _productRepository.Update(productEntity);
		}
	}
}
