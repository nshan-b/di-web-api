using System.ComponentModel.DataAnnotations;

namespace di_web_api.Models {
    public class DocumentStatus {
        [Key]
        public int StatusId { get; set; }
        public int DocumentId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
