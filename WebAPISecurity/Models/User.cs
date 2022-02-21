namespace WebAPISecurity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [StringLength(60)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        [StringLength(50)]
        public string UserRole { get; set; }
    }
}
