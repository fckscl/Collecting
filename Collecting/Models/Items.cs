using System.ComponentModel.DataAnnotations.Schema;

namespace Collecting.Models
{
    public class Items
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CollectionId { get; set; }
    }
}
