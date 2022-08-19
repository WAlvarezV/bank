using Bank.Common.Domain.Entities;

namespace Bank.Client.Domain.Entities
{
    internal class Client : BaseEntity
    {
        public string ClientId { get; private set; }
        public string Password { get; private set; }
        public bool State { get; private set; }
        public int PersonId { get; private set; }
        public virtual Person Person { get; private set; }

        public void SetClientId(string clientId) => ClientId = clientId;
        public void SetPassword(string password) => Password = password;
        public void SetState(bool state) => State = state;
        public void SetPersonId(int personId) => PersonId = personId;
        public void SetPerson(Person person) => Person = person;
    }
}
