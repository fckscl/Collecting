namespace Collecting.Models
{
    public class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }
    }
}
