using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorSample.Pages;

public class IndexModel : PageModel
{
        private readonly ILogger<IndexModel> _logger;

        public List<SelectListItem> WeekDay { get; set; }
        
        [BindProperty]
        public string? WeekDaySelected { get; set; }
        [BindProperty]
        public string? Message { get; set; }
        public void OnGet()
        {
            this.WeekDay = new List<SelectListItem>();
            this.WeekDay.Add(new SelectListItem 
                                  {
                                      Value = "Monday",Text =  "Monday"
                                  });
            this.WeekDay.Add(new SelectListItem 
                                  {
                                      Value = "Tuesday",Text =  "Tuesday"
                                  });                                  
                                  
        }

        public void OnPost()
        {
            ViewData["Message"] = $"{this.WeekDaySelected} submitted to API";
        }
}
