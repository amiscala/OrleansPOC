using Orleans;
using Orleans.Concurrency;
using OrleansLedgerPoc.Entities.Request;
using OrleansLedgerPoc.Entities.States;

namespace OrleansLedgerPoc.Entities.Interfaces
{
    public interface IAccountGrain : IGrainWithStringKey
    {
        [Transaction(TransactionOption.Join)]
        Task<AccountGrainState> Withdraw(MovementRequest request);

        [Transaction(TransactionOption.Join)]
        Task<AccountGrainState> Deposit(MovementRequest request);

        [Transaction(TransactionOption.CreateOrJoin)]
        Task<long> GetBalance();
    }
}
