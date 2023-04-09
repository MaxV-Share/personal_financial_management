
using MongoDB.Bson.Serialization.Attributes;
using PersonalFinancialManagement.Services.Excels.Configurations;

namespace PersonalFinancialManagement.Services.Excels.Entities;

[BsonCollection("MisaRawDataEntries")]
public class MisaRawDataEntry : MongoEntity
{
    [BsonElement("walletName")]
    public string? WalletName { get; set; }
    [BsonElement("excelName")]
    public string? ExcelName { get; set; }
    [BsonElement("transactions")]
    public List<Transaction>? Transactions { get; set; }
}
