using Demo.Data.Contexts;
using Demo.Data.DatabaseInitializer;
using Demo.Data.Model;
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
    public interface IAlbumRespository
    {
        List<AlbumViewModel> GetAllAlbum();
        void UpdateAlbum(AlbumViewModel albumViewModel);
        Task<AlbumViewModel> GetAlbumByTittle(string title);
        Task<List<AlbumViewModel>> GetAlbumByGenre(string genreName);
        Task<List<AlbumViewModel>> GetAlbumByArtist(string artist);
        Task<AlbumViewModel> GetAlbumById(int id);
        Task<List<AlbumViewModel>> GetAlbumByDate(DateTime date);
        int DeleteAlbum(int id);
    }
    public class AlbumRespository:IAlbumRespository
    {
        private readonly IMusicStoreContext _db;
        private readonly DbContext _dbContext;
        

        public AlbumRespository(IMusicStoreContext db)
        {
            _db = db;
            _dbContext = db.GetDbContext();
            
        }
        public List<AlbumViewModel> GetAllAlbum()
        {
            var AList = new List<AlbumViewModel>();
            
            var storeProcedureName = "dbo.AllAlbum";
            AList = _dbContext.Database.SqlQuery<AlbumViewModel>(
                $"{storeProcedureName}"
                ).ToList();
           
            return (AList==null)?null:AList;
        }
        public async Task<List<AlbumViewModel>> GetAlbumByDate(DateTime date)
        {
            var Alist = new List<AlbumViewModel>();
            var storeProcedureName = "[dbo].[SelectAlbumByDate]";
            Alist = await _dbContext.Database.SqlQuery<AlbumViewModel>(
                $"{storeProcedureName} @date",
                new SqlParameter("@date", date)
                ).ToListAsync();
            return (Alist == null) ? null : Alist;
        }
        public void UpdateAlbum(AlbumViewModel albumViewModel)
        {
            
            var exitAlbum = _db.Album.FirstOrDefault(s => s.AlbumId == albumViewModel.AlbumId);
            if (exitAlbum == null)
            {
                var storeProcedureName = "[dbo].[Add_Album]";
                var Result = _dbContext.Database.SqlQuery<AlbumViewModel>(
                    $"{storeProcedureName} @Title,@ArtistName,@GenreName,@Price,@PublicDate",
                    new SqlParameter("@Title", albumViewModel.Title),
                    new SqlParameter("@ArtistName",albumViewModel.ArtistName),
                    new SqlParameter("@GenreName",albumViewModel.GenreName),
                    new SqlParameter("@Price",albumViewModel.Price),
                    new SqlParameter("@PublicDate",albumViewModel.PublicDate)
                    ).SingleOrDefault();
            }
            else
            {
                var storeProcedureName = "[dbo].[Update_Album]";
                var Result = _dbContext.Database.SqlQuery<AlbumViewModel>(
                    $"{storeProcedureName}@AlbumId,@Title,@ArtistName,@GenreName,@Price,@PublicDate",
                    new SqlParameter("@AlbumId",albumViewModel.AlbumId),
                    new SqlParameter("@Title",albumViewModel.Title),
                    new SqlParameter("@ArtistName",albumViewModel.ArtistName),
                    new SqlParameter("@GenreName",albumViewModel.GenreName),
                    new SqlParameter("@Price",albumViewModel.Price),
                    new SqlParameter("@PublicDate",albumViewModel.PublicDate)
                    ).SingleOrDefault();
            }
            
        }
        public async Task<AlbumViewModel> GetAlbumByTittle(string title)
        {
            var album = new AlbumViewModel();
            var storeProcedureName = "[dbo].[SelectAlbumByTitle]";
            try
            {
                album = await _dbContext.Database.SqlQuery<AlbumViewModel>(
                    $"{storeProcedureName} @title",
                    new SqlParameter("@title", title)
                ).FirstOrDefaultAsync();
                return album == null ? null : album;
            }
            catch(Exception ex)
            {
                return new AlbumViewModel();
            }

        }
        public async Task<List<AlbumViewModel>>GetAlbumByGenre(string genreName)
        {
            var Result = new List<AlbumViewModel>();
            var storeProcedureName = "[dbo].[SelectAlbumListByGenre]";
            try
            {
                Result = await _dbContext.Database.SqlQuery<AlbumViewModel>(
                    $"{storeProcedureName} @genreName",
                    new SqlParameter("@genreName", genreName)
                    ).ToListAsync();
                return Result == null ? null : Result;
            }
            catch(Exception ex)
            {
                return new List<AlbumViewModel>();
            }
        }
        public async Task<List<AlbumViewModel>> GetAlbumByArtist(string artist)
        {
            var result = new List<AlbumViewModel>();
            var storeProceduceName = "[dbo].[SelectAlbumListByArtist]";
            try
            {
                result = await _dbContext.Database.SqlQuery<AlbumViewModel>(
                    $"{storeProceduceName} @artist",
                    new SqlParameter("@artist", artist)
                    ).ToListAsync();
                return result == null ? null : result;
            }
            catch(Exception ex)
            {
                return new List<AlbumViewModel>();
            }
        }
        public async Task<AlbumViewModel> GetAlbumById(int id)
        {
            var album = new AlbumViewModel();
            var db = new MusicStoreContext();
            var exitAlbum =await _db.Album.FirstOrDefaultAsync(s => s.AlbumId == id);
            if (exitAlbum != null)
            {
                album.AlbumId = exitAlbum.AlbumId;
                album.ArtistName = exitAlbum.ArtistName;
                album.Title = exitAlbum.Title;
                album.GenreName = exitAlbum.GenreName;
                album.Price = exitAlbum.Price;
                album.PublicDate = exitAlbum.PublicDate;
            }
            return album;
        }
        public int DeleteAlbum(int id)
        {
            int returnValue = 0;
            var model = _db.Album.FirstOrDefault(s => s.AlbumId == id);
            if (model != null)
            {
                _db.Album.Remove(model);
                _dbContext.SaveChanges();
                return returnValue;
            }
            else
                return (returnValue = 1);
        }
    }
}
