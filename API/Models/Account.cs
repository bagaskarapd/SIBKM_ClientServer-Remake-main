using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("tb_tr_accounts")]
public class Account
{
    [Key, Column("employee_nik", TypeName = "char(5)")]
    public string EmployeeNIK { get; set; }
    [Column("password", TypeName = "char(255)")]
    public string password { get; set; }

    // Cardinality
    [JsonIgnore]
    public Employee? Employee { get; set; }
    [JsonIgnore]
    public ICollection<AccountRole>? AccountRoles { get; set; }
}
