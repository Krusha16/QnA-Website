using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnA_Website.Models
{
    public class AnswerVote
    {
        public int Id { get; set; }
        public VoteType VoteType { get; set; }
        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}