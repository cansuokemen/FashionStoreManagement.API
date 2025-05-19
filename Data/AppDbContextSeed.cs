using FashionStoreManagement.API.Entities;

namespace FashionStoreManagement.API.Data
{
    public static class AppDbContextSeed
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Elbise" },
                    new Category { Name = "Ceket" },
                    new Category { Name = "Gömlek" }
                );
            }

            if (!context.Brands.Any())
            {
                context.Brands.AddRange(
                    new Brand { Name = "Zara" },
                    new Brand { Name = "Mango" }
                );
            }

            if (!context.Sizes.Any())
            {
                context.Sizes.AddRange(
                    new Size { Name = "S" },
                    new Size { Name = "M" },
                    new Size { Name = "L" }
                );
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    FullName = "Test Kullanıcı",
                    Email = "test@example.com",
                    Password = "123456"
                });
            }

            // önce diğer verileri kaydet!
            context.SaveChanges();

            // şimdi kategori ve marka alınabilir
            if (!context.Products.Any())
            {
                var category = context.Categories.FirstOrDefault();
                var brand = context.Brands.FirstOrDefault();

                if (category == null || brand == null) return;

                var product = new Product
                {
                    Name = "Siyah Elbise",
                    Description = "Şık siyah gece elbisesi",
                    Price = 499.99m,
                    CategoryId = category.Id,
                    BrandId = brand.Id
                };

                context.Products.Add(product);
                context.SaveChanges(); // ürünü kaydet

                var sizeS = context.Sizes.First(s => s.Name == "S").Id;
                var sizeM = context.Sizes.First(s => s.Name == "M").Id;

                context.ProductSizes.AddRange(
                    new ProductSize { ProductId = product.Id, SizeId = sizeS, StockQuantity = 10 },
                    new ProductSize { ProductId = product.Id, SizeId = sizeM, StockQuantity = 15 }
                );

                context.SaveChanges(); // stokları da kaydet
            }
        }

    }
}
