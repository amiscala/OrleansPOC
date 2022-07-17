using Orleans;
using Orleans.Concurrency;
using OrleansLedgerPoc.Entities.Interfaces;
using OrleansLedgerPoc.Entities.Request;
using OrleansLedgerPoc.Entities.States;
using System.Linq;

namespace OrleansLedgerPoc.Entities.Grains
{
    [StatelessWorker]
    public class TransferGrain : Grain, ITransferGrain
    {
        public async Task<AccountGrainState> Deposit(IAccountGrain toAccount, MovementRequest request)
        {

            var res = await toAccount.Deposit(request);

            return res;



        }

        public async Task<AccountGrainState[]> Transfer(IAccountGrain fromAccount, IAccountGrain toAccount, MovementRequest request)
        {
            return await Task.WhenAll(fromAccount.Withdraw(request),
                                      toAccount.Deposit(request));


        }
    }
}
