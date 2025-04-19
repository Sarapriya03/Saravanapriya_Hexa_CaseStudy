using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using PayXpert.entity;
using PayXpert.util;
using PayXpert.exception;

namespace PayXpert.dao
{
    public class FinancialRecordService : IFinancialRecordService
    {
        private string connStr;

        public FinancialRecordService(string propFile)
        {
            connStr = DBPropertyUtil.GetConnectionString(propFile);
        }

        public void AddFinancialRecord(FinancialRecord record)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
                {
                    string query = @"INSERT INTO FinancialRecordTable (EmployeeID, RecordDate, Description, Amount, RecordType)
                                 VALUES (@EmpId, @Date, @Desc, @Amount, @Type)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmpId", record.EmployeeID);
                    cmd.Parameters.AddWithValue("@Date", record.RecordDate);
                    cmd.Parameters.AddWithValue("@Desc", record.Description);
                    cmd.Parameters.AddWithValue("@Amount", record.Amount);
                    cmd.Parameters.AddWithValue("@Type", record.RecordType);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new FinancialRecordException("Error while adding financial record: " + ex.Message);
            }
        }

        public FinancialRecord GetFinancialRecordById(int recordId)
        {
            try { 
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM FinancialRecordTable WHERE RecordID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", recordId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new FinancialRecord
                    {
                        RecordID = reader.GetInt32(0),
                        EmployeeID = reader.GetInt32(1),
                        RecordDate = reader.GetDateTime(2),
                        Description = reader.GetString(3),
                        Amount = reader.GetDecimal(4),
                        RecordType = reader.GetString(5)
                    };
                }

                throw new FinancialRecordException("Record not found.");
            }
        }
            catch (SqlException ex)
            {
                throw new FinancialRecordException("Error while fetching financial record: " + ex.Message);
            }
        }

        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            try
            {
                var list = new List<FinancialRecord>();
                using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
                {
                    string query = "SELECT * FROM FinancialRecordTable WHERE EmployeeID = @EmpId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmpId", employeeId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new FinancialRecord
                        {
                            RecordID = reader.GetInt32(0),
                            EmployeeID = reader.GetInt32(1),
                            RecordDate = reader.GetDateTime(2),
                            Description = reader.GetString(3),
                            Amount = reader.GetDecimal(4),
                            RecordType = reader.GetString(5)
                        });
                    }
                }

                return list;
            }
            catch (SqlException ex)
            {
                throw new FinancialRecordException("Error while fetching financial records: " + ex.Message);
            }
        }

        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        {
            try
            {
                var list = new List<FinancialRecord>();
                using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
                {
                    string query = "SELECT * FROM FinancialRecordTable WHERE RecordDate = @Date";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Date", recordDate);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new FinancialRecord
                        {
                            RecordID = reader.GetInt32(0),
                            EmployeeID = reader.GetInt32(1),
                            RecordDate = reader.GetDateTime(2),
                            Description = reader.GetString(3),
                            Amount = reader.GetDecimal(4),
                            RecordType = reader.GetString(5)
                        });
                    }
                }

                return list;
            }
            catch (SqlException ex)
            {
                throw new FinancialRecordException("Error while fetching financial records: " + ex.Message);
            }
        }

        internal List<FinancialRecord> GetRecordsByEmployeeId(int reportEmpId)
        {
            throw new NotImplementedException();
        }
    }
}
