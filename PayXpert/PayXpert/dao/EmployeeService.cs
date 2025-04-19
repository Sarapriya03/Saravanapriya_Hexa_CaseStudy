using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using PayXpert.entity;
using PayXpert.util;
using PayXpert.exception;

namespace PayXpert.dao
{
    public class EmployeeService : IEmployeeService
    {
        private string connStr;

        public EmployeeService(string propFile)
        {
            connStr = DBPropertyUtil.GetConnectionString(propFile);
        }

        public void AddEmployee(Employee emp)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
                {
                    string query = @"INSERT INTO Employee 
                                (FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate, TerminationDate) 
                                VALUES 
                                (@FirstName, @LastName, @DOB, @Gender, @Email, @Phone, @Address, @Position, @JoiningDate, @TerminationDate)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                    cmd.Parameters.AddWithValue("@DOB", emp.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@Phone", emp.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", emp.Address);
                    cmd.Parameters.AddWithValue("@Position", emp.Position);
                    cmd.Parameters.AddWithValue("@JoiningDate", emp.JoiningDate);
                    cmd.Parameters.AddWithValue("@TerminationDate", (object?)emp.TerminationDate ?? DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Employee already exists.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the employee.", ex);
            }
        }

        public void RemoveEmployee(int employeeId)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
                {
                    string query = "DELETE FROM Employee WHERE EmployeeID = @EmployeeID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                        throw new EmployeeNotFoundException("Employee not found.");
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while deleting employee.", ex);
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException("Employee not found.");
            }
        }

        public void UpdateEmployee(Employee emp)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
                {
                    string query = @"UPDATE Employee SET 
                                 FirstName = @FirstName,
                                 LastName = @LastName,
                                 DateOfBirth = @DOB,
                                 Gender = @Gender,
                                 Email = @Email,
                                 PhoneNumber = @Phone,
                                 Address = @Address,
                                 Position = @Position,
                                 JoiningDate = @JoiningDate,
                                 TerminationDate = @TerminationDate
                                 WHERE EmployeeID = @EmployeeID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                    cmd.Parameters.AddWithValue("@DOB", emp.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@Phone", emp.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", emp.Address);
                    cmd.Parameters.AddWithValue("@Position", emp.Position);
                    cmd.Parameters.AddWithValue("@JoiningDate", emp.JoiningDate);
                    cmd.Parameters.AddWithValue("@TerminationDate", (object?)emp.TerminationDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EmployeeID", emp.EmployeeID);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                        throw new EmployeeNotFoundException("Employee not found for update.");
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while updating employee.", ex);
            }
            catch (Exception ex)
            {
                throw new EmployeeNotFoundException("Employee not found for update.");
            }
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
                {
                    string query = "SELECT * FROM Employee WHERE EmployeeID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Employee
                        {
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Gender = reader["Gender"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Address = reader["Address"].ToString(),
                            Position = reader["Position"].ToString(),
                            JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                            TerminationDate = reader["TerminationDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["TerminationDate"])
                        };
                    }

                    throw new EmployeeNotFoundException("Employee not found.");
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while fetching employee.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the employee.", ex);
            }
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> list = new List<Employee>();
            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
                {
                    string query = "SELECT * FROM Employee";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new Employee
                        {
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Gender = reader["Gender"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Address = reader["Address"].ToString(),
                            Position = reader["Position"].ToString(),
                            JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                            TerminationDate = reader["TerminationDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["TerminationDate"])
                        });
                    }
                }

                return list;
            }
            catch (SqlException ex)
            {
                throw new DatabaseConnectionException("Error while fetching employees.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the employees.", ex);
            }
        }
    }
}
