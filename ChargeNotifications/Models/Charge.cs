namespace ChargeNotifications.Models
{
    public class Charge
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Description { get; set; }
        public string ChargeDescription { get; set; }
        public int CostPence { get; set; }
        public int CostTotal { get; set; }
        public DateTime ChargeDate { get; set; }

    }
}
