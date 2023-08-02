using MyWeb.Application.Services.Carts;

namespace MyWeb.Application.Interfaces.FacadPatterns
{
    public interface ICartsFacad
    {
        ICartService CartService { get; }
    }
}
