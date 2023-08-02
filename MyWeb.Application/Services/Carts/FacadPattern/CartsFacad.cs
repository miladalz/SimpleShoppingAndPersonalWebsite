using MyWeb.Application.Interfaces.Contexts;
using MyWeb.Application.Interfaces.FacadPatterns;

namespace MyWeb.Application.Services.Carts.FacadPattern
{
    public class CartsFacad : ICartsFacad
    {
        private readonly IDataBaseContext _context;
        public CartsFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private ICartService _cartService;
        public ICartService CartService
        {
            get
            {
                return _cartService ??= new CartService(_context);
            }
        }
    }
}
