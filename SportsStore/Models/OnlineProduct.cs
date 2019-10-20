namespace SportsStore.Models
{
    public class OnlineProduct : Product
    {
        public string ThumbNail { get; set; }

        public OnlineProduct(string name, decimal price, string thumbNail, string description = null) : base(name, price, description)
        {
            ThumbNail = thumbNail;
        }
    }

}