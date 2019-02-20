namespace ShopParser.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GoodsDescription")]
    public partial class GoodsDescription
    {
        public int Id { get; set; }

        public int AllGoodsId { get; set; }

        public string Specification { get; set; }

        public virtual AllGoods AllGoods { get; set; }
    }
}
