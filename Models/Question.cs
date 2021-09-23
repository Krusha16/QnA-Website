using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QnA_Website.Models
{
    public class Question
    {
        public Question()
        {
            this.Tags = new HashSet<Tag>();
            this.Answers = new HashSet<Answer>();
            this.QuestionVotes = new HashSet<QuestionVote>();
            this.CommentOnQuestions = new HashSet<CommentOnQuestion>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Title can't be more than 50 letter.")]
        [MinLength(5, ErrorMessage = "Title can't be less than 5 letter.")]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<QuestionVote> QuestionVotes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<CommentOnQuestion> CommentOnQuestions { get; set; }
    }
}