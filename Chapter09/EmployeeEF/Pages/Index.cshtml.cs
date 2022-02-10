﻿using System.Collections.Generic;
using EmployeeEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmployeeEF.Pages;

public class IndexModel : PageModel
{
        private readonly EmployeeContext context;
        public IndexModel(EmployeeContext context)
        {
            this.context = context;
        }
         public IList<Employee> Employees { get; set; }
        public async Task OnGetAsync()
        {
            this.Employees = await this.context.
            Employees.Include(x => x.Address).
            AsNoTracking().ToListAsync();
        }

}
