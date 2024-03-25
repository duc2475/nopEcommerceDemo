using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Shipping;
using Nop.Core.Infrastructure;
using Nop.Services.Attributes;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Html;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Web.Factories;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Web.Models.Customer;
using Nop.Web.Models.HyperBuyClientManager;
using Nop.Web.Models.ShoppingCart;

namespace Nop.Web.Controllers;
public class HyperBuyClientManagerController : BasePublicController
{
/*    protected readonly IAttributeService<CheckoutAttribute, CheckoutAttributeValue> _checkoutAttributeService;
    protected readonly IWorkContext _workContext;
    protected readonly ICustomerService _customerService;
    protected readonly ICustomerModelFactory _customerModelFactory;
    protected readonly IPermissionService _permissionService;
    protected readonly IStoreContext _storeContext;
    protected readonly IShoppingCartService _shoppingCartService;
    protected readonly IShoppingCartModelFactory _shoppingCartModelFactory;
    protected readonly IProductService _productService;
    private static readonly char[] _separator = [','];

    public HyperBuyClientManagerController(
        IAttributeService<CheckoutAttribute, CheckoutAttributeValue> checkoutAttributeService,
        IWorkContext workContext,
        ICustomerService customerService, 
        ICustomerModelFactory customerModelFactory,
        IPermissionService permissionService,
        IShoppingCartModelFactory shoppingCartModelFactory,
        IShoppingCartService shoppingCartService,
        IStoreContext storeContext,
        IProductService productService)
    {
        _checkoutAttributeService = checkoutAttributeService;
        _workContext = workContext;
        _customerService = customerService;
        _customerModelFactory = customerModelFactory;
        _permissionService = permissionService;
        _shoppingCartModelFactory = shoppingCartModelFactory;
        _storeContext = storeContext;
        _shoppingCartService = shoppingCartService;
        _shoppingCartModelFactory = shoppingCartModelFactory;
        _productService = productService;
    }

*/
    #region Fields

    protected readonly CaptchaSettings _captchaSettings;
    protected readonly CustomerSettings _customerSettings;
    protected readonly IAttributeParser<CheckoutAttribute, CheckoutAttributeValue> _checkoutAttributeParser;
    protected readonly IAttributeService<CheckoutAttribute, CheckoutAttributeValue> _checkoutAttributeService;
    protected readonly ICurrencyService _currencyService;
    protected readonly ICustomerActivityService _customerActivityService;
    protected readonly ICustomerService _customerService;
    protected readonly IDiscountService _discountService;
    protected readonly IDownloadService _downloadService;
    protected readonly IGenericAttributeService _genericAttributeService;
    protected readonly IGiftCardService _giftCardService;
    protected readonly IHtmlFormatter _htmlFormatter;
    protected readonly ILocalizationService _localizationService;
    protected readonly INopFileProvider _fileProvider;
    protected readonly INopUrlHelper _nopUrlHelper;
    protected readonly INotificationService _notificationService;
    protected readonly IPermissionService _permissionService;
    protected readonly IPictureService _pictureService;
    protected readonly IPriceFormatter _priceFormatter;
    protected readonly IProductAttributeParser _productAttributeParser;
    protected readonly IProductAttributeService _productAttributeService;
    protected readonly IProductService _productService;
    protected readonly IShippingService _shippingService;
    protected readonly IShoppingCartModelFactory _shoppingCartModelFactory;
    protected readonly IShoppingCartService _shoppingCartService;
    protected readonly IStaticCacheManager _staticCacheManager;
    protected readonly IStoreContext _storeContext;
    protected readonly IStoreMappingService _storeMappingService;
    protected readonly ITaxService _taxService;
    protected readonly IUrlRecordService _urlRecordService;
    protected readonly IWebHelper _webHelper;
    protected readonly IWorkContext _workContext;
    protected readonly IWorkflowMessageService _workflowMessageService;
    protected readonly ICustomerModelFactory _customerModelFactory;
    protected readonly MediaSettings _mediaSettings;
    protected readonly OrderSettings _orderSettings;
    protected readonly ShoppingCartSettings _shoppingCartSettings;
    protected readonly ShippingSettings _shippingSettings;
    private static readonly char[] _separator = [','];

    #endregion

    #region Ctor

    public HyperBuyClientManagerController(CaptchaSettings captchaSettings,
        CustomerSettings customerSettings,
        IAttributeParser<CheckoutAttribute, CheckoutAttributeValue> checkoutAttributeParser,
        IAttributeService<CheckoutAttribute, CheckoutAttributeValue> checkoutAttributeService,
        ICurrencyService currencyService,
        ICustomerActivityService customerActivityService,
        ICustomerService customerService,
        IDiscountService discountService,
        IDownloadService downloadService,
        ICustomerModelFactory customerModelFactory,
        IGenericAttributeService genericAttributeService,
        IGiftCardService giftCardService,
        IHtmlFormatter htmlFormatter,
        ILocalizationService localizationService,
        INopFileProvider fileProvider,
        INopUrlHelper nopUrlHelper,
        INotificationService notificationService,
        IPermissionService permissionService,
        IPictureService pictureService,
        IPriceFormatter priceFormatter,
        IProductAttributeParser productAttributeParser,
        IProductAttributeService productAttributeService,
        IProductService productService,
        IShippingService shippingService,
        IShoppingCartModelFactory shoppingCartModelFactory,
        IShoppingCartService shoppingCartService,
        IStaticCacheManager staticCacheManager,
        IStoreContext storeContext,
        IStoreMappingService storeMappingService,
        ITaxService taxService,
        IUrlRecordService urlRecordService,
        IWebHelper webHelper,
        IWorkContext workContext,
        IWorkflowMessageService workflowMessageService,
        MediaSettings mediaSettings,
        OrderSettings orderSettings,
        ShoppingCartSettings shoppingCartSettings,
        ShippingSettings shippingSettings)
    {
        _captchaSettings = captchaSettings;
        _customerSettings = customerSettings;
        _checkoutAttributeParser = checkoutAttributeParser;
        _checkoutAttributeService = checkoutAttributeService;
        _currencyService = currencyService;
        _customerActivityService = customerActivityService;
        _customerService = customerService;
        _customerModelFactory = customerModelFactory;
        _discountService = discountService;
        _downloadService = downloadService;
        _genericAttributeService = genericAttributeService;
        _giftCardService = giftCardService;
        _htmlFormatter = htmlFormatter;
        _localizationService = localizationService;
        _fileProvider = fileProvider;
        _nopUrlHelper = nopUrlHelper;
        _notificationService = notificationService;
        _permissionService = permissionService;
        _pictureService = pictureService;
        _priceFormatter = priceFormatter;
        _productAttributeParser = productAttributeParser;
        _productAttributeService = productAttributeService;
        _productService = productService;
        _shippingService = shippingService;
        _shoppingCartModelFactory = shoppingCartModelFactory;
        _shoppingCartService = shoppingCartService;
        _staticCacheManager = staticCacheManager;
        _storeContext = storeContext;
        _storeMappingService = storeMappingService;
        _taxService = taxService;
        _urlRecordService = urlRecordService;
        _webHelper = webHelper;
        _workContext = workContext;
        _workflowMessageService = workflowMessageService;
        _mediaSettings = mediaSettings;
        _orderSettings = orderSettings;
        _shoppingCartSettings = shoppingCartSettings;
        _shippingSettings = shippingSettings;
    }

    #endregion




    public async Task<CustomerInfoModel> CustomerInfoManager()
    {
        var customer = await _workContext.GetCurrentCustomerAsync();
        if (!await _customerService.IsRegisteredAsync(customer))
            return new CustomerInfoModel();
        var customerInfo = new CustomerInfoModel();
        customerInfo = await _customerModelFactory.PrepareCustomerInfoModelAsync(customerInfo, customer, false);

        return customerInfo;
    }
    public async Task<ShoppingCartModel> CustomerCartManager()
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var cart = await _shoppingCartService.GetShoppingCartAsync(await _workContext.GetCurrentCustomerAsync(), ShoppingCartType.ShoppingCart, store.Id);
        var shoppingCart = new ShoppingCartModel();
        shoppingCart = await _shoppingCartModelFactory.PrepareShoppingCartModelAsync(shoppingCart, cart);

        return shoppingCart;
    }

    protected virtual async Task ParseAndSaveCheckoutAttributesAsync(IList<ShoppingCartItem> cart, IFormCollection form)
    {
        ArgumentNullException.ThrowIfNull(cart);

        ArgumentNullException.ThrowIfNull(form);

        var attributesXml = string.Empty;
        var excludeShippableAttributes = !await _shoppingCartService.ShoppingCartRequiresShippingAsync(cart);
        var store = await _storeContext.GetCurrentStoreAsync();
        var checkoutAttributes = await _checkoutAttributeService.GetAllAttributesAsync(_staticCacheManager, _storeMappingService, store.Id, excludeShippableAttributes);
        foreach (var attribute in checkoutAttributes)
        {
            var controlId = $"checkout_attribute_{attribute.Id}";
            switch (attribute.AttributeControlType)
            {
                case AttributeControlType.DropdownList:
                case AttributeControlType.RadioList:
                case AttributeControlType.ColorSquares:
                case AttributeControlType.ImageSquares:
                    {
                        var ctrlAttributes = form[controlId];
                        if (!StringValues.IsNullOrEmpty(ctrlAttributes))
                        {
                            var selectedAttributeId = int.Parse(ctrlAttributes);
                            if (selectedAttributeId > 0)
                                attributesXml = _checkoutAttributeParser.AddAttribute(attributesXml,
                                    attribute, selectedAttributeId.ToString());
                        }
                    }

                    break;
                case AttributeControlType.Checkboxes:
                    {
                        var cblAttributes = form[controlId];
                        if (!StringValues.IsNullOrEmpty(cblAttributes))
                        {
                            foreach (var item in cblAttributes.ToString().Split(_separator, StringSplitOptions.RemoveEmptyEntries))
                            {
                                var selectedAttributeId = int.Parse(item);
                                if (selectedAttributeId > 0)
                                    attributesXml = _checkoutAttributeParser.AddAttribute(attributesXml,
                                        attribute, selectedAttributeId.ToString());
                            }
                        }
                    }

                    break;
                case AttributeControlType.ReadonlyCheckboxes:
                    {
                        //load read-only (already server-side selected) values
                        var attributeValues = await _checkoutAttributeService.GetAttributeValuesAsync(attribute.Id);
                        foreach (var selectedAttributeId in attributeValues
                                     .Where(v => v.IsPreSelected)
                                     .Select(v => v.Id)
                                     .ToList())
                        {
                            attributesXml = _checkoutAttributeParser.AddAttribute(attributesXml,
                                attribute, selectedAttributeId.ToString());
                        }
                    }

                    break;
                case AttributeControlType.TextBox:
                case AttributeControlType.MultilineTextbox:
                    {
                        var ctrlAttributes = form[controlId];
                        if (!StringValues.IsNullOrEmpty(ctrlAttributes))
                        {
                            var enteredText = ctrlAttributes.ToString().Trim();
                            attributesXml = _checkoutAttributeParser.AddAttribute(attributesXml,
                                attribute, enteredText);
                        }
                    }

                    break;
                case AttributeControlType.Datepicker:
                    {
                        var date = form[controlId + "_day"];
                        var month = form[controlId + "_month"];
                        var year = form[controlId + "_year"];
                        DateTime? selectedDate = null;
                        try
                        {
                            selectedDate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(date));
                        }
                        catch
                        {
                            // ignored
                        }

                        if (selectedDate.HasValue)
                            attributesXml = _checkoutAttributeParser.AddAttribute(attributesXml,
                                attribute, selectedDate.Value.ToString("D"));
                    }

                    break;
                case AttributeControlType.FileUpload:
                    {
                        _ = Guid.TryParse(form[controlId], out var downloadGuid);
                        var download = await _downloadService.GetDownloadByGuidAsync(downloadGuid);
                        if (download != null)
                        {
                            attributesXml = _checkoutAttributeParser.AddAttribute(attributesXml,
                                attribute, download.DownloadGuid.ToString());
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        //validate conditional attributes (if specified)
        foreach (var attribute in checkoutAttributes)
        {
            var conditionMet = await _checkoutAttributeParser.IsConditionMetAsync(attribute.ConditionAttributeXml, attributesXml);
            if (conditionMet.HasValue && !conditionMet.Value)
                attributesXml = _checkoutAttributeParser.RemoveAttribute(attributesXml, attribute.Id);
        }

        //save checkout attributes
        await _genericAttributeService.SaveAttributeAsync(await _workContext.GetCurrentCustomerAsync(), NopCustomerDefaults.CheckoutAttributes, attributesXml, store.Id);
    }


    public virtual async Task<IActionResult> HyperBuyCart()
    {
        HyperCustomerZone hyperCustomerZone = new HyperCustomerZone();
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.EnableShoppingCart))
            return RedirectToRoute("Homepage");
        hyperCustomerZone.CustomerInfo = await CustomerInfoManager();
        hyperCustomerZone.ShoppingCart = await CustomerCartManager();
        return View(hyperCustomerZone);
    }
    [HttpPost, ActionName("HyperBuyCart")]
    [FormValueRequired("updatecart")]
    public virtual async Task<IActionResult> UpdateCart(IFormCollection form)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.EnableShoppingCart))
            return RedirectToRoute("Homepage");

        var customer = await _workContext.GetCurrentCustomerAsync();
        var store = await _storeContext.GetCurrentStoreAsync();
        var cart = await _shoppingCartService.GetShoppingCartAsync(customer, ShoppingCartType.ShoppingCart, store.Id);

        //get identifiers of items to remove
        var itemIdsToRemove = form["removefromcart"]
            .SelectMany(value => value.Split(_separator, StringSplitOptions.RemoveEmptyEntries))
            .Select(idString => int.TryParse(idString, out var id) ? id : 0)
            .Distinct().ToList();

        var products = (await _productService.GetProductsByIdsAsync(cart.Select(item => item.ProductId).Distinct().ToArray()))
            .ToDictionary(item => item.Id, item => item);

        //get order items with changed quantity
        var itemsWithNewQuantity = cart.Select(item => new
        {
            //try to get a new quantity for the item, set 0 for items to remove
            NewQuantity = itemIdsToRemove.Contains(item.Id) ? 0 : int.TryParse(form[$"itemquantity{item.Id}"], out var quantity) ? quantity : item.Quantity,
            Item = item,
            Product = products.TryGetValue(item.ProductId, out var value) ? value : null
        }).Where(item => item.NewQuantity != item.Item.Quantity);

        //order cart items
        //first should be items with a reduced quantity and that require other products; or items with an increased quantity and are required for other products
        var orderedCart = await itemsWithNewQuantity
            .OrderByDescendingAwait(async cartItem =>
                (cartItem.NewQuantity < cartItem.Item.Quantity &&
                 (cartItem.Product?.RequireOtherProducts ?? false)) ||
                (cartItem.NewQuantity > cartItem.Item.Quantity && cartItem.Product != null && (await _shoppingCartService
                    .GetProductsRequiringProductAsync(cart, cartItem.Product)).Any()))
            .ToListAsync();

        //try to update cart items with new quantities and get warnings
        var warnings = await orderedCart.SelectAwait(async cartItem => new
        {
            ItemId = cartItem.Item.Id,
            Warnings = await _shoppingCartService.UpdateShoppingCartItemAsync(customer,
                cartItem.Item.Id, cartItem.Item.AttributesXml, cartItem.Item.CustomerEnteredPrice,
                cartItem.Item.RentalStartDateUtc, cartItem.Item.RentalEndDateUtc, cartItem.NewQuantity, true)
        }).ToListAsync();

        //updated cart
        cart = await _shoppingCartService.GetShoppingCartAsync(customer, ShoppingCartType.ShoppingCart, store.Id);

        //parse and save checkout attributes
        await ParseAndSaveCheckoutAttributesAsync(cart, form);

        //prepare model
        var model = new ShoppingCartModel();
        model = await _shoppingCartModelFactory.PrepareShoppingCartModelAsync(model, cart);

        //update current warnings
        foreach (var warningItem in warnings.Where(warningItem => warningItem.Warnings.Any()))
        {
            //find shopping cart item model to display appropriate warnings
            var itemModel = model.Items.FirstOrDefault(item => item.Id == warningItem.ItemId);
            if (itemModel != null)
                itemModel.Warnings = warningItem.Warnings.Concat(itemModel.Warnings).Distinct().ToList();
        }
        HyperCustomerZone hyperCustomerZone = new HyperCustomerZone();
        hyperCustomerZone.CustomerInfo = await CustomerInfoManager();
        hyperCustomerZone.ShoppingCart = model;

        return View(hyperCustomerZone);
    }
}
