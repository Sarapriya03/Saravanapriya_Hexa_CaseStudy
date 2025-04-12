namespace PayXpert.entity
{
    public class Tax
    {
        public int TaxID { get; set; }
        public int EmployeeID { get; set; }
        public int TaxYear { get; set; }
        public decimal TaxableIncome { get; set; }
        public decimal TaxAmount { get; set; }

        public Tax() { }

        public Tax(int taxID, int employeeID, int taxYear, double taxableIncome, double taxAmount)
        {
            TaxID = taxID;
            EmployeeID = employeeID;
            TaxYear = taxYear;
            TaxableIncome = (decimal)taxableIncome;
            TaxAmount = (decimal)taxAmount;
        }
    }
}
