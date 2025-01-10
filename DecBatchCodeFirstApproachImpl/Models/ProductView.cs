namespace DecBatchCodeFirstApproachImpl.Models
{
    public class ProductView
    {
        public int Pid { get; set; }
        public string PName { get; set; }
        public string Pcat { get; set; }
        public IFormFile Pimg { get; set; }
        public double Price { get; set; }
    }
}
