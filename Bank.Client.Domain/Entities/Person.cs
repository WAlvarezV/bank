using Bank.Common.Application.Enum;
using Bank.Common.Domain.Entities;

namespace Bank.Client.Domain.Entities
{
    internal class Person : BaseEntity
    {
        public string Identification { get; set; }
        public string FullName { get; set; }
        public GenderEnum Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
