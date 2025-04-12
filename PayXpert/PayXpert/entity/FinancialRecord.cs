using System;

namespace PayXpert.entity
{
    public class FinancialRecord
    {
        public int RecordID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime RecordDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string RecordType { get; set; } // e.g. Income, Expense

        public FinancialRecord() { }

        public FinancialRecord(int recordID, int employeeID, DateTime recordDate, string description, double amount, string recordType)
        {
            RecordID = recordID;
            EmployeeID = employeeID;
            RecordDate = recordDate;
            Description = description;
            Amount = (decimal)amount;
            RecordType = recordType;
        }
    }
}
