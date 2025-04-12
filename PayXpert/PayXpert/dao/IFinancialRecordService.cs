using System;
using System.Collections.Generic;
using PayXpert.entity;

namespace PayXpert.dao
{
    public interface IFinancialRecordService
    {
        void AddFinancialRecord(FinancialRecord record);
        FinancialRecord GetFinancialRecordById(int recordId);
        List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId);
        List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate);
    }
}
