using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CandyCaneLane1._1.Data;

namespace CandyCaneLane1._1.Pages.Candy
{
    public class IndexModel : PageModel
    {
        private readonly CandyCaneLane1._1.Data.ApplicationDbContext _context;

        [BindProperty]
        public int opencount { get; set; }

        [BindProperty]
        public int closedcount { get; set; }

        [BindProperty(SupportsGet = true)]
        public string searchInput { get; set; } = " ";

        [BindProperty(SupportsGet = true)]
        public System.DateTime searchstartDate { get; set; } = new System.DateTime(1890, 01, 01);
        [BindProperty(SupportsGet = true)]
        public System.DateTime searchendDate { get; set; } = System.DateTime.Now;

        public IndexModel(CandyCaneLane1._1.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Candies> Candies { get;set; }

        public async Task OnGetAsync()
        {
            var Candy = from m in _context.Candies select m;

            if (User.IsInRole("manager"))
            {
                Candy = Candy.Where(s => s.CandyName.Contains(searchInput));
            }else if (User.IsInRole("Employee"))
            {
                Candy = Candy.Where(s => s.CandyName.Contains(searchInput));
            } else if (User.IsInRole("Customer"))
            {
                Candy = Candy.Where(s => s.CandyName.Contains(searchInput));
            }else
            {
                Candy = Candy.Where(s => s.CandyName.Contains(searchInput));
            }


            // Candies = await Candy.ToListAsync();

            var startDate = searchstartDate;
            var endDate = searchendDate;
            Candies = await Candy.Where(m => m.DeliveryDate > startDate && m.DeliveryDate <= endDate)
                .ToListAsync();
            foreach (var item in Candies)
            {
                if (item.ApprPos == false)
                {
                    opencount += 1;
                }
                else if (item.ApprPos == true)
                {
                    closedcount += 1;
                }else
                {

                }
            }
                System.Diagnostics.Debug.WriteLine("opencount: ");
                System.Diagnostics.Debug.WriteLine(opencount);
                System.Diagnostics.Debug.WriteLine("closedcount: ");
                System.Diagnostics.Debug.WriteLine(closedcount);
            
        }
    }
}
