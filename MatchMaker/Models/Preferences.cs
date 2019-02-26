namespace MatchMaker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Preferences
    {
        [Key]
        public int preference_id { get; set; }

        public int person_id { get; set; }

        [Required]
        [StringLength(50)]
        public string fieldofinterest1 { get; set; }

        [Required]
        [StringLength(50)]
        public string fieldofinterest2 { get; set; }

        [Required]
        [StringLength(50)]
        public string fieldofinterest3 { get; set; }

        [Required]
        [StringLength(50)]
        public string position1 { get; set; }

        [Required]
        [StringLength(50)]
        public string position2 { get; set; }

        [Required]
        [StringLength(50)]
        public string position3 { get; set; }

        [Required]
        [StringLength(50)]
        public string technology1 { get; set; }

        [Required]
        [StringLength(50)]
        public string technology2 { get; set; }

        [Required]
        [StringLength(50)]
        public string technology3 { get; set; }

        public virtual People People { get; set; }
    }
}
