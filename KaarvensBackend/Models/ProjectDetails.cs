// Updated ProjectDetails class
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components.Web;

public class ProjectDetails
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(300), Column(TypeName = "nvarchar(300)")]
    public string JobNo { get; set; }

    [Required, MaxLength(300), Column(TypeName = "nvarchar(300)")]
    public string ProjectName { get; set; }

    [Required, MaxLength(300), Column(TypeName = "nvarchar(300)")]
    public string ArchitectName { get; set; }

    [MaxLength(300), Column(TypeName = "nvarchar(300)")]
    public string SiteLocation { get; set; }

    // Navigation Property - One Project can have many Drawings
    public List<DrawingDetails> DrawingDetails { get; set; }
}

// Updated DrawingDetails class
public class DrawingDetails
{
    [Key]
    public int DrawingId { get; set; }

    [MaxLength(300), Column(TypeName = "nvarchar(300)")]
    public string DrawingName { get; set; }

    [MaxLength(100), Column(TypeName = "nvarchar(100)")]
    public string DrawingStatus { get; set; }

    [MaxLength(10), Column(TypeName = "nvarchar(10)")]
    public string Revision { get; set; }

    // Foreign Key relationship to ProjectDetails
    public int ProjectDetailsId { get; set; }

    // Prevent ProjectDetails from being required in API calls
    [JsonIgnore]
    public ProjectDetails? ProjectDetails { get; set; }
}
