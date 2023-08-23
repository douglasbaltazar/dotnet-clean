﻿using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _productContext;
		public ProductRepository(ApplicationDbContext context)
		{
			_productContext = context;
		}

		public async Task<Product> Create(Product product)
		{
			_productContext.Add(product);
			await _productContext.SaveChangesAsync();
			return product;
		}

		public async Task<Product> GetById(int? id)
		{
			return await _productContext.Products.Include(c => c.Category).SingleOrDefaultAsync(p => p.Id == id);
		}

		public async Task<Product> GetProductCategoryAsync(int? id)
		{
			return await _productContext.Products.Include(c => c.Category).SingleOrDefaultAsync(p => p.Id == id);
		}

		public async Task<IEnumerable<Product>> GetProductsAsync()
		{
			return await _productContext.Products.ToListAsync();
		}

		public async Task<Product> Remove(Product product)
		{
			_productContext.Remove(product);
			await _productContext.SaveChangesAsync();
			return product;
		}

		public async Task<Product> Update(Product product)
		{
			_productContext.Update(product);
			await _productContext.SaveChangesAsync();
			return product;
		}
	}
}
