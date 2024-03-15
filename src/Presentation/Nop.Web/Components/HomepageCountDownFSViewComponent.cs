using Microsoft.AspNetCore.Mvc;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;


namespace Nop.Web.Components;

public class HomepageCountDownFSViewComponent: NopViewComponent
{
    protected readonly ICommonModelFactory _commonModelFactory;

    public HomepageCountDownFSViewComponent(ICommonModelFactory commonModelFactory)
    {
        _commonModelFactory = commonModelFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await _commonModelFactory.PrepareCountDownModelAsync();
        if (model == null)
            return View("");
        return View(model);
    }
}
