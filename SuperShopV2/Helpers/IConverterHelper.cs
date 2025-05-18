using SuperShopV2.Data.Entities;
using SuperShopV2.Models;
using System;

namespace SuperShopV2.Helpers
{
    public interface IConverterHelper
    {
        Product ToProduct(ProductViewModel model, Guid imageId, bool isNew);

        ProductViewModel ToProductViewModel(Product product);
    }
}
