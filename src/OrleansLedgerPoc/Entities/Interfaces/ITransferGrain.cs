using Orleans;
using Orleans.Concurrency;
using OrleansLedgerPoc.Entities.Request;
using OrleansLedgerPoc.Entities.States;

namespace OrleansLedgerPoc.Entities.Interfaces
{
    
    public interface ITransferGrain : IGrainWithIntegerKey
    {
        [Transaction(TransactionOption.Create)]
        Task<AccountGrainState[]> Transfer(
        IAccountGrain fromAccount,
        IAccountGrain toAccount,
        MovementRequest request);
        [Transaction(TransactionOption.Create)]
        Task<AccountGrainState> Deposit(
        IAccountGrain toAccount,
        MovementRequest request);
    }
}
