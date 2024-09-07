using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DutResLeague.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string PlayerName { get; set; }

        [Required]
        [StringLength(50)]
        public string StudentNumber { get; set; }

        [Required]
        public int ClubId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        // Navigation Property
        [ForeignKey("ClubId")]
        public virtual Club Club { get; set; }


    }
}