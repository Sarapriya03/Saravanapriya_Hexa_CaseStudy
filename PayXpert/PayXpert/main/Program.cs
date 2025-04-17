// See https://aka.ms/new-console-template for more information
using PayXpert.dao;
using PayXpert.entity;
using PayXpert.util;
using PayXpert.exception;

namespace PayXpert.main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connStr = DBPropertyUtil.GetConnectionString("db.properties");
            var employeeService = new EmployeeService(connStr);
            var payrollService = new PayrollService(connStr);
            var taxService = new TaxService(connStr);
            var financialService = new FinancialRecordService(connStr);
            var reportGenerator = new ReportGenerator(employeeService);

            while (true)
            {
                Console.WriteLine("\n*****Choose an operation*****");
                Console.WriteLine("1. View All Employees");
                Console.WriteLine("2. Add New Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Generate Reports");
                Console.WriteLine("6. Exit");

                Console.Write("Enter your choice (1-6): ");
                bool validChoice = int.TryParse(Console.ReadLine(), out int choice);

                if (!validChoice || choice < 1 || choice > 6)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            var allEmployees = employeeService.GetAllEmployees();
                            foreach (var e in allEmployees)
                            {
                                Console.WriteLine($"{e.EmployeeID}: {e.FirstName} {e.LastName} - {e.Position}");
                            }
                            break;

                        case 2:
                            var newEmp = GetEmployeeDetails();
                            ValidationService validationService = new ValidationService();

                            if (!validationService.IsValidEmail(newEmp.Email))
                            {
                                Console.WriteLine("Invalid email format. Please try again.");
                                break; 
                            }

                            employeeService.AddEmployee(newEmp);
                            Console.WriteLine("Employee added successfully.");
                            break;


                        case 3:
                            Console.Write("Enter Employee ID to update: ");
                            int updateId = Convert.ToInt32(Console.ReadLine());

                            var updatedEmp = GetEmployeeDetails();
                            ValidationService validation = new ValidationService();

                            if (!validation.IsValidEmail(updatedEmp.Email)) 
                            {
                                Console.WriteLine("Invalid email format. Please try again.");
                                break;
                            }

                            updatedEmp.EmployeeID = updateId;
                            employeeService.UpdateEmployee(updatedEmp);
                            Console.WriteLine("Employee updated successfully.");
                            break;


                        case 4:
                            Console.Write("Enter Employee ID to delete: ");
                            int deleteId = Convert.ToInt32(Console.ReadLine());
                            employeeService.RemoveEmployee(deleteId);
                            Console.WriteLine("Employee deleted successfully.");
                            break;

                        case 5:
                            Console.Write("Enter Employee ID for reports: ");
                            int reportId = Convert.ToInt32(Console.ReadLine());

                            var payrolls = payrollService.GetPayrollsForEmployee(reportId);
                            reportGenerator.GeneratePayrollReport(payrolls);

                            var taxes = taxService.GetTaxesForEmployee(reportId);
                            reportGenerator.GenerateTaxReport(taxes);

                            var records = financialService.GetFinancialRecordsForEmployee(reportId);
                            reportGenerator.GenerateFinancialSummary(records);
                            break;

                        case 6:
                            Console.WriteLine("Exiting...");
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static Employee GetEmployeeDetails()
        {
            var emp = new Employee();

            Console.Write("First Name: ");
            emp.FirstName = Console.ReadLine() ?? "";

            Console.Write("Last Name: ");
            emp.LastName = Console.ReadLine() ?? "";

            Console.Write("Date of Birth (yyyy-MM-dd): ");
            emp.DateOfBirth = DateTime.Parse(Console.ReadLine() ?? "");

            Console.Write("Gender: ");
            emp.Gender = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            emp.Email = Console.ReadLine() ?? "";

            Console.Write("Phone Number: ");
            emp.PhoneNumber = Console.ReadLine() ?? "";

            Console.Write("Address: ");
            emp.Address = Console.ReadLine() ?? "";

            Console.Write("Position: ");
            emp.Position = Console.ReadLine() ?? "";

            Console.Write("Joining Date (yyyy-MM-dd): ");
            emp.JoiningDate = DateTime.Parse(Console.ReadLine() ?? "");

            Console.Write("Termination Date (yyyy-MM-dd or leave blank): ");
            string? termDateInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(termDateInput))
                emp.TerminationDate = null;
            else
                emp.TerminationDate = DateTime.Parse(termDateInput);

            return emp;
        }
    }
}
