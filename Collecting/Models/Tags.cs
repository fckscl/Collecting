namespace Collecting.Models
{
    public class Tags
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class ItemTags
    {
        public int ID { get; set; }
        public Tags tags { get; set; } = new Tags();
        public int tagsID { get; set; }

    }
}
