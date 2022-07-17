using OrleansLedgerPoc.Entities.Request;

namespace OrleansLedgerPoc.Entities.States
{
    [Serializable]
    public class AccountGrainState
    {
        public long Balance { get; set; }
        public long Limit { get; set; }

        public Movement LastMovement { get; set; }
    }

    [Serializable]
    public class Movement
    {
        public Party DebitParty { get; set; }
        public Party CreditParty { get; set; }
        public long Amount { get; set; }
        public string MovementTimestamp => DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFFzzz");
    }
}
