using myBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace myBlog.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page,string query )
        {
            var result = db.Posts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
                result = result.Where(p => p.Body.Contains(query))
               .Union(db.Posts.Where(p => p.Title.Contains(query)))
               .Union(db.Posts.Where(p => p.Comments.Any(c => c.Body.Contains(query))))
               .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.FirstName.Contains(query))))
               .Union(db.Posts.Where(p => p.Comments.Any(c => c.Author.DisplayName.Contains(query))));
            }
            int pageSize = 3; // #of posts per page
            int pageNumber = (page ?? 1); // default page is page 1 

            var qpost = result.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize);

            return View(qpost);
        }

       public ActionResult Create()
        {
            return View();
        }
    }
}