namespace ShopParser.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AllGoods
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AllGoods()
        {
            ChangeDynamics = new HashSet<ChangeDynamics>();
            GoodsDescription = new HashSet<GoodsDescription>();
        }

        public int Id { get; set; }

        public int ShopId { get; set; }

        public double Price { get; set; }

        [StringLength(50)]
        public string ImagePath { get; set; }

        [Required]
        [StringLength(500)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(50)]
        public string IdOnSite { get; set; }

        [StringLength(500)]
        public string ReferenceToTheOriginal { get; set; }

        public int CurrencyId { get; set; }

        public int CategoriesId { get; set; }

        public virtual Categories Categories { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Shops Shops { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChangeDynamics> ChangeDynamics { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsDescription> GoodsDescription { get; set; }
    }
}
