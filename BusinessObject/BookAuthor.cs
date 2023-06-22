using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class BookAuthor
    {
        [ForeignKey(nameof(AuthorId))]
        public int AuthorId { get; set; }
        [JsonIgnore]
        public virtual Author? Author { get; set; }
        [ForeignKey(nameof(BookId))]
        public int BookId { get; set; }
        [JsonIgnore]
        public virtual Book? Book { get; set; }

        public int Author_order { get; set; }

        public decimal Royalty_percentage { get; set; }
    }
}
