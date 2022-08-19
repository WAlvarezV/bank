using System.Runtime.CompilerServices;

// 1. Api
[assembly: InternalsVisibleTo("Bank.Transaction.Api")]
// 2. Aplication
[assembly: InternalsVisibleTo("Bank.Transaction.Application")]
// 4. Test
[assembly: InternalsVisibleTo("Bank.Transaction.Test")]


