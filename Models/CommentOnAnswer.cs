using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QnA_Website.Models
{
    public class CommentOnAnswer
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public string Content { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Answer Answer { get; set; }
    }
}