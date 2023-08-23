using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
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
		private readonly IProductRepository _productRepository;
		public ProductService(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
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
			var productsEntity = await _productRepository.GetProductsAsync();
			return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
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
