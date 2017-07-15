using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spendingz.Model.Data;
using GalaSoft.MvvmLight.Messaging;
using Spendingz.Model.Messages;

namespace Spendingz.Services
{
    public class SpendingsService : ISpendings
    {
        private List<Spending> _spendings;
        private IDbStorage _dbStorage;

        public SpendingsService(IDbStorage dbStorage)
        {
            _spendings = new List<Spending>();
            _dbStorage = dbStorage;
            _spendings = _dbStorage.GetAllEntries<Spending>();
        }

        public IEnumerable<Spending> GetAllSpendings()
        {
            return new List<Spending>(_spendings);
        }

        public void SaveSpending(Spending spending)
        {
            _spendings.Add(spending);
            Messenger.Default.Send(new NewSpendingMessage { Update=true });
            Task.Run( () => { _dbStorage.CreateOrUpdateEntry(spending);  });
        }
    }
}
