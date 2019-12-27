using Demo.Data.Respositories;
using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Provider.Provider
{
    public interface IddlProvider
    {
        Task<List<CountryViewModel>> GetCountryList();
       List<StateViewModel> GetStateList(string countrycode);
        List<CityViewModel> GetCityList(string statecode);
    }
    public class ddlProvider:IddlProvider
    {
        private readonly IRegionDDLRespository _regionDDLRespository;
        public ddlProvider(IRegionDDLRespository regionDDLRespository)
        {
            _regionDDLRespository = regionDDLRespository;
        }
        public async Task<List<CountryViewModel>> GetCountryList()
        {
            var _countryList = new List<CountryViewModel>();
            _countryList=await _regionDDLRespository.GetCountryList();
            return _countryList;
        }
        public List<StateViewModel> GetStateList(string countrycode)
        {
            var _stateList = new List<StateViewModel>();
            _stateList =  _regionDDLRespository.GetStateList(countrycode);
            return _stateList;
        }
        public List<CityViewModel> GetCityList(string statecode)
        {
            var _cityList = new List<CityViewModel>();
            _cityList =  _regionDDLRespository.GetCityList(statecode);
            return _cityList;
        }
    }
}
