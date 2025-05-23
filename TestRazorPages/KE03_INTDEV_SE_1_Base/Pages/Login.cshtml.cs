using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Interfaces;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;

        public LoginModel(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var customer = _customerRepository.GetAllCustomers()
                .FirstOrDefault(c => c.Name == Username);

            if (customer == null)
            {
                ErrorMessage = "Gebruiker niet gevonden";
                return Page();
            }

            if (Password != Username)
            {
                ErrorMessage = "Ongeldig wachtwoord";
                return Page();
            }

            var claims = new[] { new Claim(ClaimTypes.Name, customer.Name) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToPage("/Index");
        }
    }
}
