using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QnA_Website.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Questions = new HashSet<Question>();
            this.Answers = new HashSet<Answer>();
            this.QuestionVotes = new HashSet<QuestionVote>();
            this.AnswerVotes = new HashSet<AnswerVote>();
            this.CommentOnQuestions = new HashSet<CommentOnQuestion>();
            this.CommentOnAnswers = new HashSet<CommentOnAnswer>();
        }
        public int Reputation { get; set; }
        public int GoldenBadges { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuestionVote> QuestionVotes { get; set; }
        public virtual ICollection<AnswerVote> AnswerVotes { get; set; }
        public virtual ICollection<CommentOnQuestion> CommentOnQuestions { get; set; }
        public virtual ICollection<CommentOnAnswer> CommentOnAnswers { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<CommentOnQuestion> CommentOnQuestions { get; set; }
        public virtual DbSet<CommentOnAnswer> CommentOnAnswers { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<QuestionVote> QuestionVotes { get; set; }
        public virtual DbSet<AnswerVote> AnswerVotes { get; set; }
        public ApplicationDbContext()
            : base("name=QnADbConnectionString", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}