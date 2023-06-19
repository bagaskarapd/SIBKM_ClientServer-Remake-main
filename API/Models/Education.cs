using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("tb_tr_educations")]
public class Education
{
    [Key, Column("id", TypeName = "int")]
    public int Id { get; set; }
    [Column("major", TypeName = "varchar(100)")]
    public string Major { get; set; }
    [Column("degree", TypeName = "varchar(10)")]
    public string Degree { get; set; }
    [Column("gpa", TypeName = "varchar(5)")]
    public string Gpa { get; set; }
    [Column("university_id")]
    public int? UniversityId { get; set; }

    // Cardinality
    [JsonIgnore]
    public Universitie? Universitie { get; set; }
    [JsonIgnore]
    public Profiling? Profiling { get; set; }
}