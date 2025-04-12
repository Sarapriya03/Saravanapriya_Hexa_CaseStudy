using System;

namespace PayXpert.entity
{
    public class Payroll
    {
        public int PayrollID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime PayPeriodStartDate { get; set; }
        public DateTime PayPeriodEndDate { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal OvertimePay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }

        public Payroll() { }

        public Payroll(int payrollID, int employeeID, DateTime startDate, DateTime endDate,
            double basicSalary, double overtimePay, double deductions, double netSalary)
        {
            PayrollID = payrollID;
            EmployeeID = employeeID;
            PayPeriodStartDate = startDate;
            PayPeriodEndDate = endDate;
            BasicSalary = (decimal)basicSalary;
            OvertimePay = (decimal)overtimePay;
            Deductions = (decimal)deductions;
            NetSalary = (decimal)netSalary;
        }
    }
}
