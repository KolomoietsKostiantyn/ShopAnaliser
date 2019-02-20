namespace ShopParser.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoriesPath")]
    public partial class CategoriesPath
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoriesId { get; set; }

        [StringLength(500)]
        public string PathToCategory { get; set; }

        public virtual Categories Categories { get; set; }
    }
}
