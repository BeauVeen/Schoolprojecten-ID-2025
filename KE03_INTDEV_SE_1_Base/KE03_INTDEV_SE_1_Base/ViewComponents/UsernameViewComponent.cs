using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Interfaces;
using System.Linq;

namespace KE03_INTDEV_SE_1_Base.ViewComponents
{
    public class UsernameViewComponent : ViewComponent
    {
        private readonly ICustomerRepository _customerRepository;
        
        public UsernameViewComponent(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IViewComponentResult Invoke()
        {
            var username = HttpContext.User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                return Content("");
            }

            var customer = _customerRepository.GetAllCustomers()
                .FirstOrDefault(c => c.Name == username);

            if (customer == null)
            {
                return Content("");
            }

            return View("Default", customer.Name);
        }
    }
}
