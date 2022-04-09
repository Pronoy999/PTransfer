using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTransfer.Models {
    public class UploadRequest {
        public int RequestId { get; set; }
        public int TotalParts { get; set; }
        public Users FileOwner { get; set; }
        [DataType(DataType.Text)]
        public string FileHash { get; set; }
        [DataType(DataType.Text)]
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public List<UploadRequestParts> Parts { get; set; }

        public UploadRequest(int requestId, int totalParts, Users fileOwner, string fileHash, string fileName) {
            RequestId = requestId;
            TotalParts = totalParts;
            FileOwner = fileOwner;
            FileHash = fileHash;
            FileName = fileName;
        }
        /// <summary>
        /// Method to get the Concatenated Parts in Order.
        /// </summary>
        /// <returns>All the parts merged as a string</returns>
        public string GetRequestParts() {
            this.Parts = this.Parts.OrderBy(o => o.PartNumber).ToList();
            StringBuilder stringBuilder = new StringBuilder();
            for(int i = 0; i < this.Parts.Count; i++) {
                stringBuilder.Append(this.Parts[i].PartValue);
            }
            return stringBuilder.ToString();
        }
    }
}
