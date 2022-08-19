using System.ComponentModel.DataAnnotations;

namespace Bank.Common.Domain.Entities
{
    internal class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
