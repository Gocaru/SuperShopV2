using Microsoft.AspNetCore.Mvc.Rendering;
using SuperShopV2.Data.Entities;
using SuperShopV2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShopV2.Data
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        IQueryable GetCountriesWithCities();

        Task<Country> GetCountryWithCitiesAsync(int id);

        Task<City> GetCityAsync(int id);

        Task AddCityAsync(CityViewModel model);

        Task<int> UpdateCityAsync(City city);

        Task<int> DeleteCityAsync(City city);

        IEnumerable<SelectListItem> GetComboCountries();    //Para preencher a combobox dos countries

        IEnumerable<SelectListItem> GetComboCities(int countryId);  //Para preencher a combobox das cities

        Task<Country> GetCountryAsync(City city);       //Para devolver um Country, em função de uma city

    }
}
