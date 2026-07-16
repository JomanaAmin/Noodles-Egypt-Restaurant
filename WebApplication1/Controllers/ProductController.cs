using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.Repositories;
using WebApplication1.Models.ProductDTO;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private IProductRepository productRepository;
        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            productRepository = unitOfWork.Products;
        }
        public IActionResult Index()
        {
            var products = productRepository.GetAllProductsAsync();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
        // This is called by the AJAX fetch request
        //public IActionResult GetVariantRow(int index)
        //{
        //    ViewData["index"] = index;
        //    return PartialView("_ProductVariantRow", new CreateProductVariantDTO());
        //}
    }
}
