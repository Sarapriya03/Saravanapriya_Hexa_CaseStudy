using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using PayXpert.entity;
using PayXpert.util;
using PayXpert.exception;

namespace PayXpert.dao
{
    public class TaxService : ITaxService
    {
        private string connStr;

        public TaxService(string propFile)
        {
            connStr = DBPropertyUtil.GetConnectionString(propFile);
        }

        public Tax CalculateTax(int employeeId, int taxYear)
        {
            double taxableIncome = 50000;
            double taxAmount = taxableIncome * 0.2;

            Tax tax = new Tax
            {
                EmployeeID = employeeId,
                TaxYear = taxYear,
                TaxableIncome = (decimal)taxableIncome,
                TaxAmount = (decimal)taxAmount
            };

            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = @"INSERT INTO TaxTable (EmployeeID, TaxYear, TaxableIncome, TaxAmount) 
                                 VALUES (@EmpId, @Year, @Income, @Amount);
                                 SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmpId", tax.EmployeeID);
                cmd.Parameters.AddWithValue("@Year", tax.TaxYear);
                cmd.Parameters.AddWithValue("@Income", tax.TaxableIncome);
                cmd.Parameters.AddWithValue("@Amount", tax.TaxAmount);

                conn.Open();
                tax.TaxID = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return tax;
        }

        public Tax GetTaxById(int taxId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM TaxTable WHERE TaxID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", taxId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Tax
                    {
                        TaxID = reader.GetInt32(0),
                        EmployeeID = reader.GetInt32(1),
                        TaxYear = reader.GetInt32(2),
                        TaxableIncome = reader.GetDecimal(3),
                        TaxAmount = reader.GetDecimal(4)
                    };
                }

                throw new TaxCalculationException("Tax record not found.");
            }
        }

        public List<Tax> GetTaxesForEmployee(int employeeId)
        {
            var list = new List<Tax>();
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM TaxTable WHERE EmployeeID = @EmpId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmpId", employeeId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Tax
                    {
                        TaxID = reader.GetInt32(0),
                        EmployeeID = reader.GetInt32(1),
                        TaxYear = reader.GetInt32(2),
                        TaxableIncome = reader.GetDecimal(3),
                        TaxAmount = reader.GetDecimal(4)
                    });
                }
            }

            return list;
        }

        public List<Tax> GetTaxesForYear(int taxYear)
        {
            var list = new List<Tax>();
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM TaxTable WHERE TaxYear = @Year";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Year", taxYear);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Tax
                    {
                        TaxID = reader.GetInt32(0),
                        EmployeeID = reader.GetInt32(1),
                        TaxYear = reader.GetInt32(2),
                        TaxableIncome = reader.GetDecimal(3),
                        TaxAmount = reader.GetDecimal(4)
                    });
                }
            }

            return list;
        }

        internal List<Tax> GetTaxesByEmployeeId(int reportEmpId)
        {
            throw new NotImplementedException();
        }
    }
}
