using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [StringLength(50)]
        public string? Title { get; set; }  

        [StringLength(50)]
        public string? Type { get; set; }

        public decimal? Price { get; set; }

        public decimal? Advance { get; set; }

        public decimal? Royalty { get; set; }

        public decimal? Ytd_sales { get; set;}

        [StringLength(50)]
        public string? Notes { get; set; }

        public DateTime? Published_date { get; set; }

        public int PublisherId { get; set; }
        [JsonIgnore]
        public virtual Publisher? Publisher { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
