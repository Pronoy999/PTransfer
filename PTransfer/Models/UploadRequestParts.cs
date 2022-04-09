using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.Models {
    public class UploadRequestParts {       
        public int PartNumber { get; set; }
        public string PartValue { get; set; }

        public UploadRequestParts( int partNumber, string partValue) {           
            PartNumber = partNumber;
            PartValue = partValue;
        }
    }
}
