using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using PayXpert.entity;
using PayXpert.util;
using PayXpert.exception;

namespace PayXpert.dao
{
    public class PayrollService : IPayrollService
    {
        private string connStr;

        public PayrollService(string propFile)
        {
            connStr = DBPropertyUtil.GetConnectionString(propFile);
        }

        public Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate)
        {
            double basicSalary = 30000; 
            double overtimePay = 2000;  
            double deductions = 1500;
            double netSalary = basicSalary + overtimePay - deductions;

            Payroll payroll = new Payroll
            {
                EmployeeID = employeeId,
                PayPeriodStartDate = startDate,
                PayPeriodEndDate = endDate,
                BasicSalary = (decimal)basicSalary,
                OvertimePay = (decimal)overtimePay,
                Deductions = (decimal)deductions,
                NetSalary = (decimal)netSalary
            };

            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = @"INSERT INTO Payroll (EmployeeID, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OvertimePay, Deductions, NetSalary)
                                 VALUES (@EmpId, @Start, @End, @Basic, @Overtime, @Deductions, @Net);
                                 SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmpId", payroll.EmployeeID);
                cmd.Parameters.AddWithValue("@Start", payroll.PayPeriodStartDate);
                cmd.Parameters.AddWithValue("@End", payroll.PayPeriodEndDate);
                cmd.Parameters.AddWithValue("@Basic", payroll.BasicSalary);
                cmd.Parameters.AddWithValue("@Overtime", payroll.OvertimePay);
                cmd.Parameters.AddWithValue("@Deductions", payroll.Deductions);
                cmd.Parameters.AddWithValue("@Net", payroll.NetSalary);

                conn.Open();
                payroll.PayrollID = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return payroll;
        }

        public Payroll GetPayrollById(int payrollId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM Payroll WHERE PayrollID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", payrollId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Payroll
                    {
                        PayrollID = reader.GetInt32(0),
                        EmployeeID = reader.GetInt32(1),
                        PayPeriodStartDate = reader.GetDateTime(2),
                        PayPeriodEndDate = reader.GetDateTime(3),
                        BasicSalary = reader.GetDecimal(4),
                        OvertimePay = reader.GetDecimal(5),
                        Deductions = reader.GetDecimal(6),
                        NetSalary = reader.GetDecimal(7)
                    };
                }

                throw new PayrollGenerationException("Payroll not found.");
            }
        }

        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            var list = new List<Payroll>();
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM Payroll WHERE EmployeeID = @EmpId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmpId", employeeId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Payroll
                    {
                        PayrollID = reader.GetInt32(0),
                        EmployeeID = reader.GetInt32(1),
                        PayPeriodStartDate = reader.GetDateTime(2),
                        PayPeriodEndDate = reader.GetDateTime(3),
                        BasicSalary = reader.GetDecimal(4),
                        OvertimePay = reader.GetDecimal(5),
                        Deductions = reader.GetDecimal(6),
                        NetSalary = reader.GetDecimal(7)
                    });
                }
            }

            return list;
        }

        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            var list = new List<Payroll>();
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM Payroll WHERE PayPeriodStartDate >= @Start AND PayPeriodEndDate <= @End";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Start", startDate);
                cmd.Parameters.AddWithValue("@End", endDate);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Payroll
                    {
                        PayrollID = reader.GetInt32(0),
                        EmployeeID = reader.GetInt32(1),
                        PayPeriodStartDate = reader.GetDateTime(2),
                        PayPeriodEndDate = reader.GetDateTime(3),
                        BasicSalary = reader.GetDecimal(4),
                        OvertimePay = reader.GetDecimal(5),
                        Deductions = reader.GetDecimal(6),
                        NetSalary = reader.GetDecimal(7)
                    });
                }
            }

            return list;
        }

        internal List<Payroll> GetPayrollsByEmployeeId(int reportEmpId)
        {
            throw new NotImplementedException();
        }
    }
}
