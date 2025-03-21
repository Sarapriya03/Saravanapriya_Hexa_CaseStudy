CREATE TABLE Employee
(EmployeeID int PRIMARY KEY,
FirstName varchar(max),
LastName varchar(max),
DateOfBirth datetime,
Gender varchar(20),
Email varchar(max),
PhoneNumber bigint,
Address varchar(50),
Position varchar(50),
JoiningDate datetime,
TerminationDate datetime);

CREATE TABLE Payroll (
    PayrollID INT PRIMARY KEY,
	EmployeeID int NOT NULL,
    PayPeriodStartDate DATE NOT NULL,
    PayPeriodEndDate DATE NOT NULL,
    BasicSalary DECIMAL(10, 2) NOT NULL,
    OvertimePay DECIMAL(10, 2) DEFAULT 0,
    Deductions DECIMAL(10, 2) DEFAULT 0,
    NetSalary AS (BasicSalary + OvertimePay - Deductions) PERSISTED, 
CONSTRAINT FK_Employee_Payroll FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

CREATE TABLE TaxTable(
    TaxID INT PRIMARY KEY,  
    EmployeeID INT NOT NULL, 
	TaxYear INT NOT NULL,  
    TaxableIncome DECIMAL(10, 2) NOT NULL,  
    TaxAmount DECIMAL(10, 2) NOT NULL,  
CONSTRAINT FK_Employee_Tax FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

CREATE TABLE FinancialRecordTable (
    RecordID INT PRIMARY KEY, 
    EmployeeID INT NOT NULL,    
    RecordDate DATE NOT NULL,   
    Description VARCHAR(255),   
    Amount DECIMAL(10, 2) NOT NULL,  
    RecordType VARCHAR(50) CHECK (RecordType IN ('Income', 'Expense', 'Tax Payment')),  
CONSTRAINT FK_Employee_FinancialRecord FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

ALTER TABLE Employee
ALTER COLUMN DateOfBirth DATE;

ALTER TABLE Employee
ALTER COLUMN JoiningDate DATE;

ALTER TABLE Employee
ALTER COLUMN TerminationDate DATE;