using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Concurrency;
using Orleans.Transactions.Abstractions;
using OrleansLedgerPoc.Entities.Interfaces;
using OrleansLedgerPoc.Entities.Request;
using OrleansLedgerPoc.Entities.States;

namespace OrleansLedgerPoc.Entities.Grains
{
    [Reentrant]
    public class AccountGrain : Grain, IAccountGrain
    {
        private readonly ITransactionalState<AccountGrainState> _state;

        public AccountGrain([TransactionalState("AccountState", "AccountStore")] ITransactionalState<AccountGrainState> state)
        {
            _state = state;
        }

        public Task<AccountGrainState> Deposit(MovementRequest request)
        {
              return _state.PerformUpdate(
                state =>
                {
                    state.Balance += request.Amount;
                    state.LastMovement = new Movement
                    {
                        Amount = request.Amount,
                        CreditParty = request.CreditParty,
                        DebitParty = request.DebitParty
                    };
                    return state;
                }
            );
        }

        public Task<long> GetBalance()
        {
            return _state.PerformRead(x => x.Balance);
        }

        public Task<AccountGrainState> Withdraw(MovementRequest request)
        {
            return _state.PerformUpdate(state =>
            {
                if (state.Balance + state.Limit < request.Amount)
                {
                    throw new Exception("Insufficent Funds");
                }
                state.Balance -= request.Amount;
                state.LastMovement = new Movement
                {
                    Amount = request.Amount,
                    CreditParty = request.CreditParty,
                    DebitParty = request.DebitParty
                };
                return state;
            });
            
        }
    }
}
