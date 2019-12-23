using Demo.Data.Respositories;
using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Provider.Provider
{
    public interface IGenreProvider
    {
        List<GenreViewModel>GetGenreListAsync();
    }
    public class GenreProvider:IGenreProvider
    {
        private readonly IGenreRespository _genreRespository;
        public GenreProvider(IGenreRespository genreRespository)
        {
            _genreRespository = genreRespository;
        }
        public List<GenreViewModel>GetGenreListAsync()
        {
            try
            {
               var genreList=new List<GenreViewModel>();
                genreList = _genreRespository.GetGenreListAsync();
                return genreList;
            }
            catch(Exception ex)
            {
                return new List<GenreViewModel>();
            }
            
        }
    }
}
