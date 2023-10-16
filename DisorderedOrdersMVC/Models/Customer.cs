namespace DisorderedOrdersMVC.Models
{
    public class Customer : Identifiers
    {
        public string Email { get; set; }
        public bool IsPreferred { get; set; }
    }
}
