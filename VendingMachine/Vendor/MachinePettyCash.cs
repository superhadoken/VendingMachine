namespace VendingMachine.Vendor
{
    public class MachinePettyCash
    {
        internal MachinePettyCash(int amountInPence)
        {
            AmountInPence = amountInPence;
        }

        public int AmountInPence { get; private set; }
    }
}
