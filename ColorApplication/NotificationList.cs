namespace ColorApplication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NotificationList")]
    public partial class NotificationList
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        [StringLength(50)]
        public string Text { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }
    }
}
