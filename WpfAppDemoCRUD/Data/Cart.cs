using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppDemoCRUD.Data
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Unit { get; set; }
    }
}
