using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace UploadToBlobStorageDemo
{
    public class BlobStorageManager
    {
        private readonly CloudStorageAccount _account;
        private readonly CloudBlobClient _blobClient;


        public BlobStorageManager(string connectionStringName)
        {
            // Development Storage 
            //_account =  CloudStorageAccount.Parse(ConfigurationManager.AppSettings[DataConnectionString].ConnectionString);

            _account = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString);

            _blobClient = _account.CreateCloudBlobClient();
        }

        public void CreateContainer(string containerName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

            if (container.CreateIfNotExists())
            {
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob }
                );
            }
        }

        public void UploadFromStream(Stream memoryStream, string containerName, string blobName)
        {

            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

            CloudBlockBlob blockBob = container.GetBlockBlobReference(blobName);

            blockBob.UploadFromStream(memoryStream);
        }


        public void UploadFromText(string text, string containerName, string blobName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

            CloudBlockBlob blockBob = container.GetBlockBlobReference(blobName);

            blockBob.UploadText(text);
        }

        public FileStream DownloadToStream(string containerName, string blobName, FileStream fileStream)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            blockBlob.DownloadToStream(fileStream);

            return fileStream;
        }

        public string DownloadToText(string containerName, string blobName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            string text;

            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                text = System.Text.Encoding.Default.GetString(memoryStream.ToArray());
            }

            return text;
        }

        public List<string> ListBlobs(string containerName)
        {
            List<string> blobs = new List<string>();
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof (CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob) item;

                    blobs.Add(blob.Name);
                }
            }

            return blobs;
        }

        public string GetdSpecificUrlFromBlobStorage(string containerName, string blobName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            try
            {
                blockBlob.FetchAttributes();

                return blockBlob.Uri.OriginalString;
            }
            catch (Exception ex)
            {
                throw new Exception("Blob não existe"); 
            }
        }

        public bool DeleteSpecificBlob(string containerName, string blobName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            if (blockBlob.DeleteIfExists())
            {
                return true;
            }
            return false;

        }
    }
}
