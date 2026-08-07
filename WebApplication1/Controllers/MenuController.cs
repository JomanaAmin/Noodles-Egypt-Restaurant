using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Repositories;
using WebApplication1.Models.ProductDTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Controllers
{
    public class MenuController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment environment;
        private IProductRepository productRepository;
        private IBaseRepository<Category> categoryRepository;
        private IBaseRepository<ProductVariant> productVariantRepository;
        public MenuController(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            this.environment = environment;
            this.unitOfWork = unitOfWork;
            productRepository = unitOfWork.Products;
            categoryRepository = unitOfWork.Categories;
            productVariantRepository = unitOfWork.ProductVariants;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productRepository.GetAllProductsAsync();
            return View(products);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var categories=await categoryRepository.GetAllAsync();
            if (categories == null)
            {
                throw new Exception("Categories is null");
            }
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            //gives it 1 already made variant (Empty)
            CreateProductDTO dto = new CreateProductDTO();
            dto.Variants.Add(new CreateProductVariantDTO());

            return View(dto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO dto)
        {

            if (!ModelState.IsValid)
            {
                var categories = await categoryRepository.GetAllAsync();
                if (categories == null)
                {
                    throw new Exception("Categories is null");
                }
                ViewBag.Categories = new SelectList(
                    categories,
                    "Id",
                    "Name",
                    dto.CategoryId);   // preserve the selected category

                return View(dto);
            }
            //save image
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            newFileName += Path.GetExtension(dto.ImageFile.FileName);
            string filePathName=environment.WebRootPath + "/products/" + newFileName;
            using (var stream = System.IO.File.Create(filePathName))
            {
                dto.ImageFile.CopyTo(stream);
            }
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageFileName = newFileName,
                CategoryId = dto.CategoryId,
                Variants = dto.Variants.Select(v => new ProductVariant
                {
                    Name = v.Name,
                    Description = v.Description,
                    Price = v.Price
                }).ToList()
            };

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult NewVariant(int id)
        {
            ViewData.TemplateInfo.HtmlFieldPrefix = $"Variants[{id}]";
            return PartialView(
                "_VariantForm",
                new CreateProductVariantDTO()
            );
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            ProductDTO? product= await productRepository.GetProductByIdAsync(id);
            if (product == null) 
            {
                return RedirectToAction("Index");
            }
            UpdateProductDTO dto = new UpdateProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageFileName = product.ImageFileName,
                CategoryId = product.CategoryId,
                Variants = product.Variants.Select(v => new ProductVariantDTO
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    Price = v.Price
                }).ToList()
            };
            ViewBag.Categories = new SelectList(await categoryRepository.GetAllAsync(), "Id", "Name");
            return View(dto);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductDTO dto) 
        {
            Product? product = await productRepository.GetByIdAsync(dto.Id);
            if (product == null) 
            {
                return RedirectToAction("Index");
            }
            //if (!ModelState.IsValid) 
            //{
            //    ViewBag.Categories = new SelectList(await categoryRepository.GetAllAsync(), "Id", "Name");
            //    dto.ImageFileName = product.ImageFileName;
            //    return View(dto);
            //}
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    Console.WriteLine($"Key: {state.Key}");

                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }

                ViewBag.Categories = new SelectList(
                    await categoryRepository.GetAllAsync(),
                    "Id",
                    "Name",
                    dto.CategoryId);

                dto.ImageFileName = product.ImageFileName;

                return View(dto);
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.CategoryId= dto.CategoryId;


            //image
            string newFileName = product.ImageFileName;
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                newFileName+= Path.GetExtension(dto.ImageFile.FileName);
                string pathName = environment.WebRootPath + "/products/" + newFileName;
                using (var stream=System.IO.File.Create(pathName) ) 
                {
                    dto.ImageFile.CopyTo(stream);
                }
            }
            product.ImageFileName = newFileName;
            //remanining variants
            var submittedIds = dto.Variants
            .Where(v => v.Id != 0)
            .Select(v => v.Id)
            .ToHashSet();
            //old variants-remaining variants
            var variantsToDelete = product.Variants
                .Where(v => !submittedIds.Contains(v.Id))
                .ToList();

            foreach (var variant in variantsToDelete)
            {
               await productVariantRepository.DeleteAsync(variant);
            }
            foreach (var variant in dto.Variants) 
            {
                ProductVariant? v = await productVariantRepository.GetByIdAsync(variant.Id);
                if (v == null)
                {
                    await productVariantRepository.AddAsync(
                        new ProductVariant 
                        {
                            Name = variant.Name,
                            Description = variant.Description,
                            Price = variant.Price,
                            ProductId = product.Id
                        });
                }
                else 
                {
                    v.Name = variant.Name;
                    v.Description = variant.Description;
                    v.Price = variant.Price;
                    productVariantRepository.Update(v);

                }
                await unitOfWork.SaveChangesAsync();
            }
            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            ProductDTO? product = await productRepository.GetProductByIdAsync(id);
            if (product == null) 
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }
        public async Task<IActionResult> Delete (int id)
        {
            Product? product = await productRepository.GetByIdAsync(id);
            if (product == null) 
            {
                return RedirectToAction("Index");
            }
            //Delete variants
            foreach (var variant in product.Variants) 
            {
                await productVariantRepository.DeleteAsync(variant);
            }
            //delete image
            string filePathName = environment.WebRootPath + "/products/" + product.ImageFileName;
            System.IO.File.Delete(filePathName);

            //delete product
            await productRepository.DeleteAsync(product);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
            //return View();
        }
        public async Task<IActionResult> GetByCategory(int id) 
        {
            var products = productRepository.GetAllAsQueryable();
            List<ProductDTO> filteredProducts = await products.Select(p=>new ProductDTO
            {
                Id=p.Id,
                Name=p.Name,
                Description=p.Description,
                ImageFileName=p.ImageFileName,
                CategoryId=p.CategoryId,
                CategoryName=p.Category.Name,
                Variants=p.Variants.Select(v=>new ProductVariantDTO
                {
                    Id=v.Id,
                    Name=v.Name,
                    Description=v.Description,
                    Price=v.Price
                }).ToList()

            }            
            ).Where(p => p.CategoryId == id).ToListAsync();
            return View("Index", filteredProducts);
        }

    }
}
