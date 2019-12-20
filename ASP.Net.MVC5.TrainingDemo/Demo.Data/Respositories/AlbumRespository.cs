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
            
            var storeProcedureName = "dbo.AllAllbum";
            AList = _dbContext.Database.SqlQuery<AlbumViewModel>(
                $"{storeProcedureName}"
                ).ToList();
           
            return (AList==null)?null:AList;
        }
        public void UpdateAlbum(AlbumViewModel albumViewModel)
        {
            var db = new MusicStoreContext();
            var exitAlbum = _db.Album.FirstOrDefault(s => s.AlbumId == albumViewModel.AlbumId);
            if (exitAlbum == null)
            {
                var AlbumData = new Album()
                {
                    AlbumId = Convert.ToInt32(albumViewModel.AlbumId),
                    Title = albumViewModel.Title,
                    ArtistName = albumViewModel.ArtistName,
                    GenreName = albumViewModel.GenreName,
                    Price = albumViewModel.Price
                };
                _db.Album.Add(AlbumData);
                _dbContext.SaveChanges();
            }
            else
            {
                db.Set<Album>().Attach(exitAlbum);
                db.Entry(exitAlbum).State = EntityState.Modified;

                exitAlbum.AlbumId = Convert.ToInt32(albumViewModel.AlbumId);
                exitAlbum.Title = albumViewModel.Title;
                exitAlbum.ArtistName = albumViewModel.ArtistName;
                exitAlbum.GenreName = albumViewModel.GenreName;
                exitAlbum.Price = albumViewModel.Price;


                db.SaveChanges();
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
    }
}
