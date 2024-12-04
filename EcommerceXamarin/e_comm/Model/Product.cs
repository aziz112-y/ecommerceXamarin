using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace e_comm
{
    public class Product
    {

      
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public string CategoryId { get; set; }

    }
}