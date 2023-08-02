using MyWeb.Domain.Entities.Commons;
using MyWeb.Domain.Entities.Orders;

namespace MyWeb.Domain.Entities.Users
{
    public class User : BaseEntity 
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UserInRole > UserInRoles { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
