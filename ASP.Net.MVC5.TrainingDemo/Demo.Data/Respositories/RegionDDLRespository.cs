using Demo.Data.Contexts;
using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Respositories
{
    public interface IRegionDDLRespository
    {
        Task<List<CountryViewModel>> GetCountryList();
        List<StateViewModel> GetStateList(string countrycode);
        List<CityViewModel> GetCityList(string statecode);
    }
    public class RegionDDLRespository:IRegionDDLRespository
    {
        private readonly IMusicStoreContext _db;
        private readonly DbContext _dbContext;
        public RegionDDLRespository(IMusicStoreContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }
        public async Task<List<CountryViewModel>> GetCountryList()
        {
            var countryList = new List<CountryViewModel>();
            var storeProduceName = "[dbo].[GetCountryList]";
            countryList = await _dbContext.Database.SqlQuery<CountryViewModel>(
                $"{storeProduceName}"
                ).ToListAsync();
            return (countryList != null) ? countryList : new List<CountryViewModel>();
        }
        public List<StateViewModel>GetStateList(string countrycode)
        {
            var stateList = new List<StateViewModel>();
            var storeProduceName = "[dbo].[GetStateList]";
            stateList =  _dbContext.Database.SqlQuery<StateViewModel>(
                $"{storeProduceName}@countrycode",
                new SqlParameter("@countrycode",countrycode)
                ).ToList();
            return (stateList != null) ? stateList : new List<StateViewModel>();
        }
        public List<CityViewModel>GetCityList(string statecode)
        {
            var cityList = new List<CityViewModel>();
            var storeProduceName = "[dbo].[GetCityList]";
            cityList = _dbContext.Database.SqlQuery<CityViewModel>(
                $"{storeProduceName}@statecode",
                new SqlParameter("@statecode",statecode)
                ).ToList();
            return (cityList != null) ? cityList : new List<CityViewModel>();
        }
    }
}
