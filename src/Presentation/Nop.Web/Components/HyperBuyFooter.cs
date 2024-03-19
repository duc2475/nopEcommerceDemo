using Microsoft.AspNetCore.Mvc;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;

namespace Nop.Web.Components;

public class HyperBuyFooter : NopViewComponent
{
    protected readonly ICommonModelFactory _commonModelFactory;

    public HyperBuyFooter(ICommonModelFactory commonModelFactory)
    {
        _commonModelFactory = commonModelFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await _commonModelFactory.PrepareFooterModelAsync();
        return View(model);
    }
}