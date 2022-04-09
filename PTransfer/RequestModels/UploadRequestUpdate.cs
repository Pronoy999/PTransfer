using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.RequestModels {
    public class UploadRequestUpdate {
        [Required]       
        public int RequestId { get; set; }     
        public int RequestStatus { get; set; }
    }
}
