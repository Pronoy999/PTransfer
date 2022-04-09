using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using dotenv.net;

namespace PTransfer.Utilities {
    public class S3Helper {
        private string BucketName = DotEnv.Read()[Constants.BUCKET_NAME_KEY];
        private RegionEndpoint BucketRegion = RegionEndpoint.APSoutheast1;
        private DateTime InitiatedTime;
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public S3Helper(string fileName, string filePath) {
            FileName = fileName;
            FilePath = filePath;
        }
        /// <summary>
        /// Method to upload the file to the S3 Bucket. 
        /// </summary>
        /// <returns>Task</returns>
        public async Task Upload() {
            await UploadFileToS3();
        }
        private async Task UploadFileToS3() {
            var s3Client = new AmazonS3Client(BucketRegion);
            var fileTransferUtility = new TransferUtility(s3Client);
            try {
                InitiatedTime = DateTime.Now;
                await fileTransferUtility.UploadAsync(FilePath, BucketName, FileName);
                Logger.logInfo(typeof(S3Helper).Name, "File Key: " + FileName + " is Uploaded to S3 Bucket.");
            } catch (Exception e) {
                fileTransferUtility.AbortMultipartUploads(BucketName,InitiatedTime);
                Logger.logError(typeof(S3Helper).Name, "File Name: " + FileName + " is NOT UPLOADED.");
                Logger.logError(typeof(S3Helper).Name, e.ToString());
             }
        }
    }
}
