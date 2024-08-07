using Azure.Storage.Blobs;

namespace ABC_Retail.Services;

public class BlobStorageService
{
    private readonly BlobContainerClient _blobContainerClient;

    public BlobStorageService(string connectionString)
    {
        var blobServiceClient = new BlobServiceClient(connectionString);
        //create a blob storage if it doesn't exist
        _blobContainerClient = blobServiceClient.GetBlobContainerClient("productimages");
        _blobContainerClient.CreateIfNotExists();
    }

    //upload file to blob storage container
    public async Task<string> UploadFileAsync(string fileName, Stream fileStream)
    {
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, true);
        return blobClient.Uri.ToString(); //return URL of uploaded file
    }
}