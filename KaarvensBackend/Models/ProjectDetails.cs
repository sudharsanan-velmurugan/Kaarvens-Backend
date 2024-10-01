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

    [Required]
    public string JobNo { get; set; } = string.Empty;

    [Required]
    public string ProjectName { get; set; } = string.Empty;
   
    [Required]
    public string ArchitectName { get; set; } = string.Empty;
   
    [Required]
    public string SiteLocation { get; set; } = string.Empty;

    // Navigation Property - One Project can have many Drawings
    public List<DrawingDetails> DrawingDetails { get; set; }
}

// Updated DrawingDetails class
public class DrawingDetails
{
    [Key]
    public int DrawingId { get; set; }

    [Required]
    public string DrawingName { get; set; } = string.Empty;

    [Required]
    public string DrawingStatus { get; set; } = string.Empty;

    [Required]
    public string Revision { get; set; } = string.Empty;

    // Foreign Key relationship to ProjectDetails
    public int ProjectDetailsId { get; set; }

    // Prevent ProjectDetails from being required in API calls
    [JsonIgnore]
    public ProjectDetails? ProjectDetails { get; set; }
}
