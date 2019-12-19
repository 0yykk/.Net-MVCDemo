using Demo.Data.Respositories;
using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Provider.Provider
{
    public interface IAlbumProvider
    {
        List<AlbumViewModel> GetAllAlbum();
        void UpdateAlbum(AlbumViewModel albumViewModel);
    }
    public class AlbumProvider:IAlbumProvider
    {
        private readonly IAlbumRespository _albumRespository;
        public AlbumProvider(IAlbumRespository albumRespository)
        {
            _albumRespository = albumRespository;
        }
        public List<AlbumViewModel> GetAllAlbum()
        {
            var searchVm = new List<AlbumViewModel>();
             searchVm=_albumRespository.GetAllAlbum();
            return searchVm;
        }
        public void UpdateAlbum(AlbumViewModel albumViewModel)
        {
            _albumRespository.UpdateAlbum(albumViewModel);
        }
    }
    
}
