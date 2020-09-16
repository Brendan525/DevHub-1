using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;


namespace DevHub.Models
{
    [Table("question")]
    public class Question
    {
        [Key]
        public long id { get; set; }
        public string username { get; set; }
        public string title { get; set; }
        public string detail { get; set; }
        public DateTime posted { get; set; }
        public string category { get; set; }
        public string tags { get; set; }
        public int status { get; set; }

        public List<Answer> answers;

        public Question()
        {
            answers = new List<Answer>();
        }
    }
}
