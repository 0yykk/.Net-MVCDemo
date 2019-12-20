using Demo.Data.Contexts;
using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Respositories
{
    public interface IGenreRespository
    {
        Task<List<GenreViewModel>> GetGenreListAsync();
    }
    public class GenreRespository:IGenreRespository
    {
        private readonly IMusicStoreContext _db;
        private readonly DbContext _dbContext;

        public GenreRespository(IMusicStoreContext db)
        {
            _db = db;
            _dbContext = _db.GetDbContext();
        }

        public async Task<List<GenreViewModel>> GetGenreListAsync()
        {
            var storeProcedureName = "[dbo].[GetGenreList]";
            var result = new List<GenreViewModel>();
            try
            {
                result = await _dbContext.Database.SqlQuery<GenreViewModel>(
                $"{storeProcedureName}"
                ).ToListAsync();
                return result == null ? null : result;
            }
            catch(Exception ex)
            {
                return new List<GenreViewModel>();
            }

        }
    }
}
