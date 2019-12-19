using Demo.Data.Contexts;
using Demo.Data.Model;
using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Respositories
{
    public interface IAlbumRespository
    {
        List<AlbumViewModel> GetAllAlbum();
        void UpdateAlbum(AlbumViewModel albumViewModel);
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
            return AList;
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
    }
}
