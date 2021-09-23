using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnA_Website.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}