using System.Collections.Generic;
using System.Threading.Tasks;
using eShop.Data.EF;
using eShop.Data.Entities;
using System;
using eShop.Utilities.Exceptions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eShop.ViewModels.Catalog.Products.Manage;
using eShop.ViewModels.Catalog.Products;
using eShop.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using eShop.Application.Common;

namespace eShop.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly eShopDbContext _context;
        private readonly IStorageService _storageService;

        public ManageProductService(eShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImages(int productId, List<IFormFile> files)
        {
            var productImages = _context.ProductImages.Where(x => x.ProductId == productId).ToList();
            foreach (var image in files)
            {
                var prodImage = new ProductImage()
                {
                    Caption = "",
                    DateCreated = DateTime.Now,
                    FileSize = image.Length,
                    ImagePath = await this.SaveFile(image),
                    IsDefault = false,
                    SortOrder = 1
                };
                productImages.Add(prodImage);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.UtcNow,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoTitle = request.SeoTitle,
                        SeoAlias = request.SeoAlias,
                        LanguageId = request.LanguageId
                    }
                },
            };
            //Save image
            if (request.ThumbnaiImage is not null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnai image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnaiImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnaiImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product : {productId}");
            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductCategoryMaps on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keywork))
                query = query.Where(x => x.pt.Name.Contains(request.Keywork));

            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }
            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            var result = new List<ProductImageViewModel>();
            var productImages = _context.ProductImages.Where(x => x.ProductId == productId);
            foreach (var image in productImages)
            {
                var prodImageView = new ProductImageViewModel()
                {
                    Id = image.Id,
                    FilePath = image.ImagePath,
                    IsDefault = image.IsDefault,
                    FileSize = image.FileSize,
                };
                result.Add(prodImageView);
            }
            return Task.FromResult(result);
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image is null) throw new EShopException($"Cannot find image: {imageId}");
            await _storageService.DeleteFileAsync(image.ImagePath);
            _context.ProductImages.Remove(image);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            if (product == null || productTranslation == null) throw new EShopException($"Cannot find a product : {request.Id}");
            productTranslation.Name = request.Name;
            productTranslation.SeoAlias = request.SeoAlias;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;
            productTranslation.Description = request.Description;
            productTranslation.Details = request.Details;
            // Save image
            if (request.ThumbnaiImage is not null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
                if (thumbnailImage is not null)
                {
                    thumbnailImage.FileSize = request.ThumbnaiImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnaiImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, string caption, bool isDefault)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image is null) throw new EShopException($"Cannot find image: {imageId}");
            image.Caption = caption;
            image.IsDefault = isDefault;
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product : {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EShopException($"Cannot find a product : {productId}");
            product.Stock += addQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}