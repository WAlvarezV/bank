using System.Runtime.CompilerServices;

// 1. Api
[assembly: InternalsVisibleTo("Bank.Client.Api")]
// 2. Aplication
[assembly: InternalsVisibleTo("Bank.Client.Application")]
// 4. Test
[assembly: InternalsVisibleTo("Bank.Client.Test")]


