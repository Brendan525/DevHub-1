using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace DevHub.Models
{
    [Table("answer")]
    public class Answer
    {
        [Key]
        public long id { get; set; }
        public string username { get; set; }
        public string detail { get; set; }
        public long question_id { get; set; }
        public DateTime posted { get; set; }
        public long upvote { get; set; }
    }
}
