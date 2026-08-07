using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Repositories;
using WebApplication1.Models;
using WebApplication1.Models.CategoryDTO;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        private IBaseRepository<Category> categoryRepository;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
            categoryRepository = unitOfWork.Categories;
        }

        public async Task<IActionResult> Index()
        {
            List<CategoryViewModel> categories = await categoryRepository.GetAllAsQueryable()
                .Select(category=> new CategoryViewModel 
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ImageFileName = category.ImageFileName
                }).ToListAsync();
            return View(categories);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
