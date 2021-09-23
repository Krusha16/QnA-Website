using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QnA_Website.Models
{
    public class Answer
    {
        public Answer()
        {
            this.CommentOnAnswers = new HashSet<CommentOnAnswer>();
            this.AnswerVotes = new HashSet<AnswerVote>();
        }
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public bool IsAccepted { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<AnswerVote> AnswerVotes { get; set; }
        public virtual ICollection<CommentOnAnswer> CommentOnAnswers { get; set; }
    }
}