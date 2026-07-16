using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoodlesEgypt.Data;
using WebApplication1.Data.Entities;

namespace WebApplication1.Controllers
{
    public class IndexModel : PageModel
    {
        private readonly NoodlesEgypt.Data.AppDbContext _context;

        public IndexModel(NoodlesEgypt.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }
    }
}
