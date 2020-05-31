namespace JagraTaskManager.Server.Models
{
    public class TicketTag
    {
        public Ticket Ticket { get; set; }
        public string TicketId { get; set; }
        public Tag Tag { get; set; }
        public string TagId { get; set; }
    }
}