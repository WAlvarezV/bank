using System.Runtime.CompilerServices;

// 1. Api
[assembly: InternalsVisibleTo("Bank.Account.Api")]
[assembly: InternalsVisibleTo("Bank.Client.Api")]
[assembly: InternalsVisibleTo("Bank.Transaction.Api")]
// 2. Aplication
[assembly: InternalsVisibleTo("Bank.Account.Application")]
[assembly: InternalsVisibleTo("Bank.Account.Domain")]
[assembly: InternalsVisibleTo("Bank.Client.Application")]
[assembly: InternalsVisibleTo("Bank.Client.Domain")]
[assembly: InternalsVisibleTo("Bank.Transaction.Application")]
[assembly: InternalsVisibleTo("Bank.Transaction.Domain")]
// 3. Infraestructure
[assembly: InternalsVisibleTo("Bank.Account.Persistence")]
[assembly: InternalsVisibleTo("Bank.Client.Persistence")]
[assembly: InternalsVisibleTo("Bank.Transaction.Persistence")]
// 4. Test
[assembly: InternalsVisibleTo("Bank.Account.Test")]
[assembly: InternalsVisibleTo("Bank.Client.Test")]
[assembly: InternalsVisibleTo("Bank.Transaction.Test")]

[assembly: InternalsVisibleTo("Bank.Common.Test")]


