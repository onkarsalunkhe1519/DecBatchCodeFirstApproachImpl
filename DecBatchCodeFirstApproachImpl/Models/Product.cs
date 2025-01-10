using System.ComponentModel.DataAnnotations;

namespace DecBatchCodeFirstApproachImpl.Models
{
    public class Product
    {
        [Key]
        public int Pid { get; set; }
        public string PName { get; set; }
        public string Pcat { get; set; }
        public string Pimg { get; set; }
        public double Price { get; set; }
          
    }
}
