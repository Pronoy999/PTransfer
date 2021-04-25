using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.RequestModels {
    public class UploadRequestCreate {       
        public int RequestId { get; set; }
        [Required]
        public int TotalParts { get; set; }
        [Required]
        public string FileHash { get; set; }
        [Required]
        public string FileName { get; set; }        
    }
}
