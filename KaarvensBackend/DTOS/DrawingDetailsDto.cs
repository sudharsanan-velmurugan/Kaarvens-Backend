using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KaarvensBackend.DTOS
{
    public class DrawingDetailsDto
    {


        public int DrawingId { get; set; }

        public string DrawingName { get; set; }

        public string DrawingStatus { get; set; }

        public string Revision { get; set; }

        public int ProjectDetailsId { get; set; }
    }
}
