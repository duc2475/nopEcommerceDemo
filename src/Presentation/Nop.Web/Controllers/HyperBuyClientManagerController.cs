using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Customers;
using Nop.Web.Factories;
using Nop.Web.Models.Customer;
using Nop.Web.Models.HyperBuyClientManager;

namespace Nop.Web.Controllers;
public class HyperBuyClientManagerController : BasePublicController
{
    protected readonly IWorkContext _workContext;
    protected readonly ICustomerService _customerService;
    protected readonly ICustomerModelFactory _customerModelFactory;
    public HyperBuyClientManagerController(IWorkContext workContext,
        ICustomerService customerService, 
        ICustomerModelFactory customerModelFactory) {
        _workContext = workContext;
        _customerService = customerService;
        _customerModelFactory = customerModelFactory;
    }
    public virtual async Task<IActionResult> CustomerManager()
    {
        HyperCustomerZone hyperCustomerZone = new HyperCustomerZone();
        var customer = await _workContext.GetCurrentCustomerAsync();
        if (!await _customerService.IsRegisteredAsync(customer))
            return Challenge();
        var model = new CustomerInfoModel();
        model = await _customerModelFactory.PrepareCustomerInfoModelAsync(model, customer, false);
        hyperCustomerZone.CustomerInfo = model;
        return View(hyperCustomerZone);
    }
}
