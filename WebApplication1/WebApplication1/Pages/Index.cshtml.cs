using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataReader _dataReader;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            _dataReader = new DataReader("MOCK_DATA.json");

        }

        [BindProperty]
        public IList<ICustomer> Customers { get; set; }

        public string SearchPolicyNumber { get; set; }
        public string SearchMemberCardNumber { get; set; }


        public async Task OnGetAsync(string searchPolicyNumber, string searchMemberCardNumber)
        {
            SearchPolicyNumber = searchPolicyNumber;
            SearchMemberCardNumber = searchMemberCardNumber;
            Customers = await Task.Run(() => _dataReader.GetAllCustomers().ToList());

            if (!ModelState.IsValid)
            {
                return;
            }

            if (!String.IsNullOrEmpty(searchPolicyNumber))
            {
                Customers = await Task.Run(() => Customers.Where(w => w.PolicyNumber.Contains(searchPolicyNumber)).ToList());
            }


            if (!String.IsNullOrEmpty(searchMemberCardNumber))
            {
                Customers = await Task.Run(() => Customers.Where(w => w.MemberCardNumber.Contains(searchMemberCardNumber)).ToList());
            }

        }
    }
}
