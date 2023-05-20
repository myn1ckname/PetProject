﻿using MongoDB.Driver;
using PetProject.ProductAPI.MongoDb.Contexts;
using PetProject.ProductAPI.Domain.Exceptions;
using PetProject.ProductAPI.Domain.Product.Entity;
using PetProject.ProductAPI.Domain.Interfaces.Repositories;

namespace PetProject.ProductAPI.MongoDb.Repositories;
public class ProductRepository : IProductRepository<Guid>
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(IDbContext context)
    {
        _collection = context.Collection<Product>();


        int y = 9;
    }

    public async Task<Product> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Product>
            .Filter
            .Eq(x => x.Id, id);

        try
        {
            return await _collection.Find(filter).FirstAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new EntityNotFoundException("the product not found", ex);
        }
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.AsQueryable().ToListAsync(cancellationToken);
    }

    public async Task<Guid> InsertOneAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(product, cancellationToken: cancellationToken);
        return product.Id;
    }

    public async Task UpdateOneAsync(Product product, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Product>.Filter.Where(x => x.Id == product.Id);
        var update = Builders<Product>
            .Update
            .Set(x => x.Name, product.Name)
            .Set(x => x.Category, product.Category)
            .Set(x => x.Price, product.Price);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task DeleteOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Product>.Filter.Where(x => x.Id == id);
        await _collection.DeleteOneAsync(filter, cancellationToken: cancellationToken);
    }
}
