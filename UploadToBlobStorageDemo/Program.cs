using System;
using System.Collections;
using System.ComponentModel;
using System.IO;

namespace UploadToBlobStorageDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            BlobStorageManager blobStorage = new BlobStorageManager("StorageConnectionString");

            string text = "test";
            string containerName = "testcontainer001";
            string blobFromText = "testblob001.txt";
            string blobFromFileStream = "testfilestream.txt"; 

            blobStorage.CreateContainer(containerName);

            blobStorage.UploadFromText(text, containerName, blobFromText);

            using (FileStream stream = new FileStream("arquivo.txt", FileMode.Open))
            {
                blobStorage.UploadFromStream(stream, containerName, blobFromFileStream);
            }

            var readContentFromText =  blobStorage.DownloadToText(containerName, blobFromText);
            var readContentFromFileStream = blobStorage.DownloadToText(containerName, blobFromFileStream); 
            Console.WriteLine("Conteúdo lido através do blob de text:{0}\r\nConteúdo lido através do blob de FileStream: {1}", readContentFromText, readContentFromFileStream);

            bool isDeleted = blobStorage.DeleteSpecificBlob(containerName, blobFromText);

            var blobs = blobStorage.ListBlobs(containerName);

            Console.WriteLine("O blob teste foi deletado? {0}", isDeleted);

            foreach (var item in blobs)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }
}
