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
    public class User
    {
        [Key , DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [EmailAddress]
        public string? Email_adress { get; set; }

        [StringLength(50)]
        public string? Password { get; set; }

        [StringLength(50)]
        public string? Source { get; set; }

        [StringLength(50)]
        public string? First_name { get; set; }

        [StringLength(50)]
        public string? Middle_name { get; set; }

        [StringLength(50)]
        public string? Last_name { get; set; }

        public DateTime Hire_date { get; set; }

        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual Role? Role { get; set; }

        public int PublisherId { get; set; }
        [JsonIgnore]
        public virtual Publisher? Publisher { get; set; }

    }
}
