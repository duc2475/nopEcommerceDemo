using Nop.Web.Framework.Models;
using Nop.Web.Models.Customer;
using Nop.Web.Models.ShoppingCart;

namespace Nop.Web.Models.HyperBuyClientManager;

public class HyperCustomerZone   
{
    public CustomerInfoModel CustomerInfo { get; set; }

    public ShoppingCartModel ShoppingCart { get; set; }
}
