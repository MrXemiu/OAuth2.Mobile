using System.Threading.Tasks;

namespace StudioDonder.OAuth2.Mobile.Tests.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    using Xamarin.Auth;

    internal class InMemoryAccountStore : AccountStore
    {
        private readonly Dictionary<string, IList<Account>> servicesWithAccounts;

        public InMemoryAccountStore()
        {
            servicesWithAccounts = new Dictionary<string, IList<Account>>();
        }

        public override IEnumerable<Account> FindAccountsForService(string serviceId)
        {
            if (servicesWithAccounts.ContainsKey(serviceId))
            {
                return servicesWithAccounts[serviceId];
            }

            return new List<Account>();
        }

        public override void Save(Account account, string serviceId)
        {
            if (!servicesWithAccounts.ContainsKey(serviceId))
            {
                servicesWithAccounts[serviceId] = new List<Account>();
            }

            var existingAccount = servicesWithAccounts[serviceId].FirstOrDefault(a => a.Username == account.Username);

            if (existingAccount != null)
            {
                servicesWithAccounts[serviceId].Remove(existingAccount);
            }

            servicesWithAccounts[serviceId].Add(account);
        }

        public override async Task<List<Account>> FindAccountsForServiceAsync(string serviceId)
        {
            
            var task = Task.Run(() => FindAccountsForService(serviceId).ToList());
            return await task;
        }

        public override async Task SaveAsync(Account account, string serviceId)
        {
            var task = Task.Run(() => Save(account, serviceId));
            await task;
        }

        public override async Task DeleteAsync(Account account, string serviceId)
        {
            var task = Task.Run(() => Delete(account, serviceId));
            await task;
        }

        public override void Delete(Account account, string serviceId)
        {
            servicesWithAccounts.Remove(serviceId);
        }
    }
}