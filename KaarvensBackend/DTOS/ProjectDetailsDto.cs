using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KaarvensBackend.DTOS
{
    public class ProjectDetailsDto
    {
        
        public int Id { get; set; }

       
        public string JobNo { get; set; }

       
        public string ProjectName { get; set; }

       
        public string ArchitectName { get; set; }

       
        public string SiteLocation { get; set; }

        public List<DrawingDetails> DrawingDetails { get; set; }
    }
}
