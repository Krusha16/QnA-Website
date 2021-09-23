using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QnA_Website.Models
{
    public class CommentOnQuestion
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Question Question { get; set; }
    }
}