using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Author
    {
        [Key , DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }

        [StringLength(50)]
        public string? Last_name { get; set; }

        [StringLength(50)]
        public string? First_name { get; set; }

        [StringLength(50)]
        [Phone]
        public string? Phone { get;set; }

        [StringLength(50)]
        [EmailAddress]
        public string? Email_address { get; set; }

        [StringLength(50)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(50)]
        public string? Zip { get; set;}
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<BookAuthor>? BookAuthors { get; set;}
    }
}
