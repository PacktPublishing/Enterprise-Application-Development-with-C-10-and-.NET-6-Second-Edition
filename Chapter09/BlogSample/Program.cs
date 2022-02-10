﻿// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

string connectionString = "CONNECTION_STRING";
string containerName = "fileuploadsample";
string blobFileName = "sample.png";
// Upload file to blob            
BlobContainerClient containerClient = new 
BlobContainerClient(connectionString, containerName);
await containerClient.CreateIfNotExistsAsync(PublicAccessType.None);//Making blob private.
BlobClient blobClient = new BlobClient(connectionString, containerName, blobFileName);
using FileStream fileStream = File.OpenRead(blobFileName);
await blobClient.UploadAsync(fileStream, true);
fileStream.Close();
Console.WriteLine(blobClient.Uri.ToString());
BlobSasBuilder sasBuilder = new BlobSasBuilder()
{
    BlobContainerName = containerClient.Name,
    Resource = "b", // c for container
    BlobName = blobClient.Name
};
sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);
if (blobClient.CanGenerateSasUri)
{
    Uri blobSasUri = blobClient.GenerateSasUri(sasBuilder);
    Console.WriteLine(blobSasUri.ToString());
}
Console.ReadLine();
