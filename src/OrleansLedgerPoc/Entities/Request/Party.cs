namespace OrleansLedgerPoc.Entities.Request
{
    [Serializable]
    public class Party
    {
        public int Bank { get; set; }
        public int Branch { get; set; }
        public int Account { get; set; }
    }
}
