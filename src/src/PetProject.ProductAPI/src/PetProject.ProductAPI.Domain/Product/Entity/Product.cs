﻿using PetProject.ProductAPI.Domain.Product.ValueObject;

namespace PetProject.ProductAPI.Domain.Product.Entity;
public class Product : EntityBase<Guid>
{
    public Product(string name, string category, double price)
    {
        Id = default;
        Name = new Name(name);
        Category = new Category(category);
        Price = new Price(price);
    }

    public Name Name { get; private set; }
    public Category Category { get; private set; }
    public Price Price { get; private set; }
}
