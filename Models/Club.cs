using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DutResLeague.Models
{
    public class Club
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string ClubName { get; set; }

        [Required]
        public int CoachId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Navigation Property
        [ForeignKey("CoachId")]
        public virtual User Coach { get; set; }

        // Navigation Property
        public virtual ICollection<Player> Players { get; set; }
    }
}
