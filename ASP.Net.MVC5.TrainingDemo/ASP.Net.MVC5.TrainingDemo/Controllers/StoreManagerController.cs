using ASP.Net.MVC5.TrainingDemo.Models;
using Demo.Core.Utilities;
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
        private readonly IGenreProvider _genreProvider;
        public List<CartListView> CartList
        {
            get
            {
                if(Session["CartList"]==null)
                {
                    return null;
                }
                else
                {
                    return Session["CartList"] as List<CartListView>;
                }
            }
            set
            {
                Session["CartList"] = value;
            }
        }
        public StoreManagerController(IAlbumProvider albumProvider,
            IGenreProvider genreProvider)
        {
            _albumProvider = albumProvider;
            _genreProvider = genreProvider;

        }
        // GET: StoreManager
        public ActionResult Store()
        {
            SetddlGenre();
            var _albumList = new List<AlbumViewModel>();
            _albumList = _albumProvider.GetAllAlbum();
            if (_albumList != null)
            {
                ViewBag.Album = _albumList;
                int pageindex = 1;
                var recordCount = _albumList.Count();
                if (Request.QueryString["page"] != null)
                    pageindex = Convert.ToInt32(Request.QueryString["page"]);
                const int PAGE_SZ = 10;

                ViewBag.Album = _albumList.OrderByDescending(alb => alb.AlbumId)
                    .Skip((pageindex - 1) * PAGE_SZ)
                    .Take(PAGE_SZ).ToList();
                ViewBag.Paper = new PagerHelper()
                {
                    PageIndex = pageindex,
                    PageSize = PAGE_SZ,
                    RecordCount = recordCount,
                };
            }
            else
            {
                ViewBag.Msg = "There are no data";
            }
            return View();
        }
        public async Task<ActionResult> Edit(int id,string type)
        {
            SetddlGenre();
            var album = new AlbumViewModel();
            album = await _albumProvider.GetAlbumById(id);
            return View(album);
        }
        public ActionResult Add()
        {
            SetddlGenre();
            return View();
        }
        public async Task<ActionResult> AlbumView(int id)
        {
            var model = new AlbumViewModel();
            model = await _albumProvider.GetAlbumById(id);
            return View(model);
        }
        public  void SetddlGenre()
        {
            var GenreList = new List<GenreViewModel>();
            if (_genreProvider.GetGenreListAsync() != null)
            {
                GenreList = _genreProvider.GetGenreListAsync();
            }
            ViewBag.GenreList= GenreList;
        }
        [HttpPost]
        public async Task<ActionResult> BuyAlbum(int id)
        { 
            var list = new CartListView();
            list.AlbumId = id;
            System.Web.HttpContext.Current.Session["CartList"] = list;

            return View();
        }
        [HttpPost]
        public ActionResult Edit(AlbumViewModel album)
        {
            _albumProvider.UpdateAlbum(album);
            return Content("<script>alert('Update Success!');history.go(-1);</script>");
        }
        [HttpPost]
        public ActionResult Add(AlbumViewModel album)
        {
            _albumProvider.UpdateAlbum(album);
            return Content("<script>alert('Add Success!');history.go(-1);</script> ");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = new DeleteMassage();
            int returnValue = _albumProvider.DeleteAlbum(id);
            if (returnValue == 0)
            {
                result.IsSuccess = true;
                result.ErrorMessage = "Delete success!";
            }

            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "The current Album has been deleted!";
            }

            return Json(result);
        }
        [HttpPost]
        public async Task<ActionResult> Store(AlbumViewModel album)
        {
            string GenreName = null; 
            var _album = new AlbumViewModel();
            var _albumList = new List<AlbumViewModel>();
            if(Request.Form.Count>0)
            {
                if (Request.Form.Get("GenreName") != "default")
                {
                    GenreName = Request.Form.Get("GenreName").ToString();
                }

            }
            else
            {
                if (Session["GenreName"] != null)
                    GenreName = Session["GenreName"].ToString();
            }

            System.Web.HttpContext.Current.Session["Title"] = album.Title;
            System.Web.HttpContext.Current.Session["GenreName"] = GenreName;
            System.Web.HttpContext.Current.Session["ArtistName"] = album.ArtistName;

            string title = album.Title;
            string ArtistName = album.ArtistName;
            if (title != null && GenreName == null && ArtistName == null)
            {
                album = await _albumProvider.GetAlbumByTittle(title);
                if (album != null)
                {
                    _albumList.Add(album);
                    ViewBag.Album = _albumList;
                    int pageindex = 1;
                    var recordCount = _albumList.Count();
                    if (Request.QueryString["page"] != null)
                        pageindex = Convert.ToInt32(Request.QueryString["page"]);
                    const int PAGE_SZ = 10;

                    ViewBag.Album = _albumList.OrderByDescending(art => art.AlbumId)
                       .Skip((pageindex - 1) * PAGE_SZ)
                       .Take(PAGE_SZ).ToList();
                    ViewBag.Pager = new PagerHelper()
                    {
                        PageIndex = pageindex,
                        PageSize = PAGE_SZ,
                        RecordCount = recordCount,
                    };
                }
                else
                {
                    ViewBag.Msg = "There are no data named " + title;
                }
            }
            else if (title == null && GenreName != null&& ArtistName == null)
            {
                _albumList = await _albumProvider.GetAlbumByGenre(GenreName);
                if (album != null)
                {
                    ViewBag.Album = _albumList;
                    int pageindex = 1;
                    var recordCount = _albumList.Count();
                    if (Request.QueryString["page"] != null)
                        pageindex = Convert.ToInt32(Request.QueryString["page"]);
                    const int PAGE_SZ = 10;

                    ViewBag.Album = _albumList.OrderByDescending(art => art.AlbumId)
                       .Skip((pageindex - 1) * PAGE_SZ)
                       .Take(PAGE_SZ).ToList();
                    ViewBag.Pager = new PagerHelper()
                    {
                        PageIndex = pageindex,
                        PageSize = PAGE_SZ,
                        RecordCount = recordCount,
                    };
                }
                else
                {
                    ViewBag.Msg = "There are no data named " + GenreName;
                }

            }
            else if (title == null && GenreName == null && ArtistName != null)
            {
                _albumList = await _albumProvider.GetAlbumByArtist(ArtistName);
                if (album != null)
                {
                    ViewBag.Album = _albumList;
                    int pageindex = 1;
                    var recordCount = _albumList.Count();
                    if (Request.QueryString["page"] != null)
                        pageindex = Convert.ToInt32(Request.QueryString["page"]);
                    const int PAGE_SZ = 10;

                    ViewBag.Album = _albumList.OrderByDescending(art => art.AlbumId)
                       .Skip((pageindex - 1) * PAGE_SZ)
                       .Take(PAGE_SZ).ToList();
                    ViewBag.Pager = new PagerHelper()
                    {
                        PageIndex = pageindex,
                        PageSize = PAGE_SZ,
                        RecordCount = recordCount,
                    };
                }
                else
                {
                    ViewBag.Msg = "There are no data named " + ArtistName;
                }

            }
            else if(title == null && GenreName == null && ArtistName == null&&album.PublicDate!=null)
            {
                _albumList = await _albumProvider.GetAlbumByDate(album.PublicDate);
                if (album != null)
                {
                    ViewBag.Album = _albumList;
                    int pageindex = 1;
                    var recordCount = _albumList.Count();
                    if (Request.QueryString["page"] != null)
                        pageindex = Convert.ToInt32(Request.QueryString["page"]);
                    const int PAGE_SZ = 10;

                    ViewBag.Album = _albumList.OrderByDescending(art => art.AlbumId)
                       .Skip((pageindex - 1) * PAGE_SZ)
                       .Take(PAGE_SZ).ToList();
                    ViewBag.Pager = new PagerHelper()
                    {
                        PageIndex = pageindex,
                        PageSize = PAGE_SZ,
                        RecordCount = recordCount,
                    };
                }
                else
                {
                    ViewBag.Msg = "There are no data named " + ArtistName;
                }
            }
            else
            {
                return Redirect("Store");
            }
            SetddlGenre();
            return View();
        }
    }
}