namespace OrleansLedgerPoc.Entities.Request
{
    public class DepoistRequest
    {
        public Party CreditParty { get; set; }
        public string CreditPartyBankBranchAccount => $"{CreditParty.Bank}_{CreditParty.Branch}_{CreditParty.Account}";
        public long Amount { get; set; }
    }
}
