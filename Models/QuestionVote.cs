using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QnA_Website.Models
{
    public enum VoteType
    {
        None,
        UpVote,
        DownVote
    }

    public class QuestionVote
    {
        public int Id { get; set; }
        public VoteType VoteType { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}