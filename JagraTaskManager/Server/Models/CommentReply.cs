using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Models
{
    public class CommentReply
    {
        public Comment Reply { get; set; }
        public string ReplyId { get; set; }
        public Comment ReplyTo { get; set; }
        public string ReplyToId { get; set; }
    }
}
