using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class BestellingenModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public BestellingenModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<Order> Orders { get; private set; } = new List<Order>();

        public async Task OnGetAsync()
        {
            var username = User.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                Orders = await _orderRepository.GetOrdersByCustomerNameAsync(username);
            }
            else
            {
                Orders = Enumerable.Empty<Order>();
            }
        }
    }
}
