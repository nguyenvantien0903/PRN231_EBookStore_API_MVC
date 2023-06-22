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
    public class Publisher
    {
        [Key , DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublisherId { get; set; }

        [StringLength(50)]
        public string? Publisher_name { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? Country { get; set; }

        [StringLength(50)]
        public string? State { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<User>? Users { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Book>? Books { get; set; }
    }
}
