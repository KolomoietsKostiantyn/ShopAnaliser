namespace ShopParser.BD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ChangeDynamics
    {
        public int Id { get; set; }

        public int AllGoodsId { get; set; }

        public double OldPrice { get; set; }

        public double NewPrice { get; set; }

        public DateTime UpdateTime { get; set; }

        public virtual AllGoods AllGoods { get; set; }
    }
}
