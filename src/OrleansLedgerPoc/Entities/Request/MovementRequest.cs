namespace OrleansLedgerPoc.Entities.Request
{
    public class MovementRequest
    {
        public Party? DebitParty { get; set; }
        public Party CreditParty { get; set; }
        public long Amount { get; set; }
        public string DebitPartyBankBranchAccount => $"{DebitParty?.Bank}_{DebitParty?.Branch}_{DebitParty?.Account}";
        public string CreditPartyBankBranchAccount => $"{CreditParty.Bank}_{CreditParty.Branch}_{CreditParty.Account}";
    }
}
