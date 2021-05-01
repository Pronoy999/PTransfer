using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.RequestModels {
    public class UploadRequestPartsCreate {
        [Required]
        public int RequestId { get; set; }
        [Required]
        public string PartValue { get; set; }
        [Required]
        public int PartNumber { get; set; }
    }
}
