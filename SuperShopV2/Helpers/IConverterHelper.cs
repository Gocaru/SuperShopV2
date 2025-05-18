using SuperShopV2.Data.Entities;
using SuperShopV2.Models;

namespace SuperShopV2.Helpers
{
    public interface IConverterHelper
    {
        Product ToProduct(ProductViewModel model, string path, bool isNew);

        ProductViewModel ToProductViewModel(Product product);
    }
}
