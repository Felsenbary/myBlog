using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myBlog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        public BlogPost()
        {
            this.Comments = new HashSet<Comment>();
        }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string Title { get; set; }
        [Required]
        [AllowHtml]
        public string Body { get; set; }
        public string MediaURL { get; set; }
        public string Slug { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }


    }

}