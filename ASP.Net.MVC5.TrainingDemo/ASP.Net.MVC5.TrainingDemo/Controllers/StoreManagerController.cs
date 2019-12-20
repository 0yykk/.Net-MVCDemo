using Demo.Domain;
using Demo.Provider.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASP.Net.MVC5.TrainingDemo.Controllers
{
    public class StoreManagerController : Controller
    {
        private readonly IAlbumProvider _albumProvider;
        //private readonly ISelectListProvider _selectListProvider;
        public StoreManagerController(IAlbumProvider albumProvider)
        {
            _albumProvider = albumProvider;
            

        }
        // GET: StoreManager
        public ActionResult Store()
        {
            
            var albumList = new List<AlbumViewModel>();
            albumList = _albumProvider.GetAllAlbum();
            if(albumList!=null&&albumList.Count>0)
            {
                ViewBag.Album = albumList;
            }
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult AlbumView()
        {
            return View();
        }
        public async void  SetddlGenre()
        {
            var GenreList = new List<GenreViewModel>();

        }
        [HttpPost]
        public async Task<ActionResult> Store(AlbumViewModel album)
        {
            string GenreName = null;
            string ArtistName = null;
            var _album = new AlbumViewModel();
            var _albumList = new List<AlbumViewModel>();
            if(Request.Form.Count>0)
            {
                if (Request.Form.Get("GenreName") != "default")
                {
                    GenreName = Request.Form.Get("GenreName").ToString();
                }
                if (Request.Form.Get("Artist") != "default")
                {
                    ArtistName = Request.Form.Get("GenreName").ToString();
                }
            }
            else
            {
                if (Session["GenreName"] != null)
                    GenreName = Session["GenreName"].ToString();
                if (Session["ArtistName"] != null)
                    ArtistName = Session["ArtistName"].ToString();
            }

            System.Web.HttpContext.Current.Session["Title"] = album.Title;
            System.Web.HttpContext.Current.Session["GenreName"] = GenreName;
            System.Web.HttpContext.Current.Session["ArtistName"] = ArtistName;

            return View();
        }


         
    }
}