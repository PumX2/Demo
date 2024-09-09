using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace ShopWebApp.Pages.Patients
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationContext _context;

		public IndexModel(ApplicationContext context)
		{
			_context = context;
		}

		public IList<Patient> Patient { get; set; }

		[BindProperty(SupportsGet = true)]
		public string SearchString { get; set; }

		public async Task OnGetAsync()
		{
			IQueryable<Patient> patientsQuery = _context.Patients;

			// Sắp xếp tăng dần theo tên
			patientsQuery = patientsQuery.OrderBy(p => p.Name);

            // Sắp xếp giảm dần theo tên
            //patientsQuery = patientsQuery.OrderByDescending(p => p.Name);

            if (!string.IsNullOrEmpty(SearchString))
			{
				// Lọc theo tên nếu có chuỗi tìm kiếm
				patientsQuery = patientsQuery.Where(p => p.Name.Contains(SearchString));
			}

			Patient = await patientsQuery.ToListAsync();
		}
	}
}
