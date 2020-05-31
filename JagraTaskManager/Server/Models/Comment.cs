using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Models
{
    public class Comment
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public Ticket Ticket { get; set; }
        public string TicketId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public ICollection<CommentReply> Replies { get; set; }
        public CommentReply ReplyTo { get; set; }
        public DateTime Created { get; set; }
        public Comment()
        {
            Created = DateTime.UtcNow;
        }
    }
}
