using OfficeOpenXml;
using PersonalFinancialManagement.Services.Excels.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinancialManagement.Services.Excels.Repositories;

namespace PersonalFinancialManagement.Services.Excels
{
    public class Example : IExample
    {
        private readonly IMisaRawDataRepository _repository;

        public Example(IMisaRawDataRepository repository)
        {
            _repository = repository;
        }
        public async Task ReadXLS(string filePath)
        {
            var existingFile = new FileInfo("C:\\Users\\thevi\\Downloads\\Misa_Test.xlsx");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(existingFile);
            //get the first worksheet in the workbook
            var rawDatas = new List<MisaRawDataEntry>();
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            Parallel.ForEach(package.Workbook.Worksheets, worksheet =>
            {
                rawDatas.Add(ReadOneSheet(worksheet));
            });
            sw.Stop();
            _repository.CreateAsync(rawDatas);
        }

        public MisaRawDataEntry ReadOneSheet(ExcelWorksheet worksheet)
        {
            var colCount = worksheet.Dimension.End.Column;  //get Column Count
            var rowCount = worksheet.Dimension.End.Row;     //get row count
            var misaRawData = new MisaRawDataEntry
            {
                WalletName = worksheet.Name,
                Transactions = new List<Transaction>()
            };
            Parallel.For(12, rowCount, row =>
            {
                if (worksheet.Cells[row, 1].Value == null || string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Value.ToString()))
                    return;
                var strDateTime = worksheet.Cells[row, 2].Value + " " + worksheet.Cells[row, 3].Value;

                var money = Convert.ToDecimal(worksheet.Cells[row, 4].Value) - Convert.ToDecimal(worksheet.Cells[row, 5].Value);
                var transaction = new Transaction
                {
                    No = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                    TransactionDate = DateTime.ParseExact(strDateTime, "dd/MM/yyyy HH:mm",
                        System.Globalization.CultureInfo.InvariantCulture),
                    Money = money,
                    RemainingBalance = Convert.ToDecimal(worksheet.Cells[row, 6].Value),
                    ParentCategory = worksheet.Cells[row, 7].Value + "",
                    Category = worksheet.Cells[row, 8].Value + "",
                    SpendMoneyFor = worksheet.Cells[row, 9].Value + "",
                    Description = worksheet.Cells[row, 10].Value + "",

                };
                misaRawData.Transactions.Add(transaction);
            });
            return misaRawData;
        }

    }
}
