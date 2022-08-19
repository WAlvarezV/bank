using System.Runtime.CompilerServices;

// 1. Api
[assembly: InternalsVisibleTo("Bank.Account.Api")]
// 2. Aplication
[assembly: InternalsVisibleTo("Bank.Account.Application")]
// 3. Infraestructure
[assembly: InternalsVisibleTo("Bank.Account.Persistence")]
// 4. Test
[assembly: InternalsVisibleTo("Bank.Account.Test")]


