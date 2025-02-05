using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Abstracts
{

    public interface IGenericFields
    {
        [Key]
        public int Id { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }

    public class GenericFields : IGenericFields
    {
        [Key]
        public int Id { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}