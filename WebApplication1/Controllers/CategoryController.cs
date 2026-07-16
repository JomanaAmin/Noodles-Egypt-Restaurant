using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Repositories;
using WebApplication1.Models.CategoryDTO;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment environment;
        private IBaseRepository<Category> categoryRepository;
        public CategoryController(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            this.unitOfWork = unitOfWork;
            categoryRepository = unitOfWork.Categories;
            this.environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await categoryRepository.GetAllAsync();
            return View(categories);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost] // Ensure you add the HttpPost attribute
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCategoryDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            newFileName += Path.GetExtension(dto.ImageFile.FileName);
            string filePathName = environment.WebRootPath + "/categories/" + newFileName;
            using (var stream = System.IO.File.Create(filePathName)) 
            {
                dto.ImageFile.CopyTo(stream);
            }
            var category = new Category { Name = dto.Name, Description = dto.Description, ImageFileName=newFileName };

            // Await these database operations
            await categoryRepository.AddAsync(category);
            await unitOfWork.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var category=await categoryRepository.GetByIdAsync(id);
            if (category == null) 
            {
                return RedirectToAction("Index");
            }
            CreateCategoryDTO dto= new CreateCategoryDTO
            {
                Name = category.Name,
                Description = category.Description
            };
            ViewData["CategoryId"] = category.Id;
            ViewData["ImageFileName"]= category.ImageFileName;
            return View(dto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, CreateCategoryDTO dto)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = category.Id;
                ViewData["ImageFileName"] = category.ImageFileName;
                return View(dto); 
            }

            string newFileName = category.ImageFileName;
            if (dto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                newFileName += Path.GetExtension(dto.ImageFile.FileName);
                string filePathName = environment.WebRootPath + "/categories/" + newFileName;
                using (var stream = System.IO.File.Create(filePathName))
                {
                    dto.ImageFile.CopyTo(stream);
                }
            }
            category.Name = dto.Name;
            category.Description = dto.Description;
            category.ImageFileName = newFileName;

            // Await these database operations
            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            
            string filePathName = environment.WebRootPath + "/categories/" + category.ImageFileName;
            System.IO.File.Delete(filePathName);

            await categoryRepository.DeleteAsync(category);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id) 
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            //ViewData["ImageFileName"] = category.ImageFileName;
            return View(category);
        }
    }
}
