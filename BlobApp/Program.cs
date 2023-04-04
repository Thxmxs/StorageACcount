using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;



//CREAR UN CONTAINER


string connectionString = "DefaultEndpointsProtocol=https;AccountName=appstore20237;AccountKey=Lsk9U/W+AyCq/x2TEqnJ1A81O3/vnGtSKlfIKQmbvA4quEU2xXilwGWdiGCciEa2CZftHPsiJIs+AStsLrAQQ==;EndpointSuffix=core.windows.net";
string containerName = "scripts";

BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

await blobServiceClient.CreateBlobContainerAsync(containerName, Azure.Storage.Blobs.Models.PublicAccessType.Blob);

Console.WriteLine("Container created");

 


//SUBIR UN BLOB A UN CONTAINER


string connectionString = "DefaultEndpointsProtocol=https;AccountName=appstore20237;AccountKey=Lsk9U/W+AyCqq/x2TEqnJ1A81O3/vnGtSKlfIKQmbvA4quEU2xXilwGWdiGCciEa2CZftHPsiJIs+AStsLrAQQ==;EndpointSuffix=core.windows.net";
string containerName = "scripts";
string blobName = "avatar22.png";
string filePath = "C:\\tmp\\avatar22.png";

BlobContainerClient blobServiceClient = new BlobContainerClient(connectionString,containerName);

BlobClient blobClient = blobServiceClient.GetBlobClient(blobName);
await blobClient.UploadAsync(filePath,true);

blobServiceClient.GetBlobClient(blobName);

Console.WriteLine("Blob Uploaded");




//LISTAR BLOBS DE UN CONTAINER


string connectionString = "DefaultEndpointsProtocol=https;AccountName=appstore20237;AccountKey=Lsk9U/W+AyCqq/x2TEqnJ1A81O3/vnGtSKlfIKQmbvA4quEU2xXilwGWdiGCciEa2CZftHPsiJIs+AStsLrAQQ==;EndpointSuffix=core.windows.net";
string containerName = "scripts";
BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);

await foreach (var blobItem in blobContainerClient.GetBlobsAsync())
{
    Console.WriteLine("The BLOB Name is {0}", blobItem.Name);
    Console.WriteLine("Blob Properties is {0}",blobItem.Properties);
}



//DESCARGAR UN BLOB


string connectionString = "DefaultEndpointsProtocol=https;AccountName=appstore20237;AccountKey=Lsk9U/W+AyCqq/x2TEqnJ1A81O3/vnGtSKlfIKQmbvA4quEU2xXilwGWdiGCciEa2CZftHPsiJIs+AStsLrAQQ==;EndpointSuffix=core.windows.net";
string containerName = "scripts";
string blobName = "avatar22.png";
string filePath = "C:\\tmp\\avatar22.png";

BlobClient blobClient = new BlobClient(connectionString, containerName, blobName);

await blobClient.DownloadToAsync(filePath);

Console.WriteLine("The blob is downloaded");





//ADD METADATA TO A BLOB


string connectionString = "DefaultEndpointsProtocol=https;AccountName=appstore20237;AccountKey=Lsk9U/W+AyCqq/x2TEqnJ1A81O3/vnGtSKlfIKQmbvA4quEU2xXilwGWdiGCciEa2CZftHPsiJIs+AStsLrAQQ==;EndpointSuffix=core.windows.net";
string containerName = "scripts";

await SetBlobMetaData();

async Task SetBlobMetaData(){

    string blobName = "avatar22.png";

    BlobClient blobClient = new BlobClient(connectionString, containerName, blobName);

    IDictionary<string,string> metaData = new Dictionary<string,string>();

    metaData.Add("Type", "Image");
    metaData.Add("Application", "BlobApp");

    await blobClient.SetMetadataAsync(metaData);

    Console.WriteLine("Metadata added");

}

//OBTENER METADATA DE UN ARCHIVO


await getMetadata();

async Task getMetadata()
{
    string blobName = "avatar22.png";
    BlobClient blobClient = new BlobClient(connectionString, containerName, blobName);

    BlobProperties blobProperties = await blobClient.GetPropertiesAsync();

    foreach (var item in blobProperties.Metadata)
    {
        Console.WriteLine("The key is {0} and the value is {1}", item.Key, item.Value);
    }

}



//BLOB LEASE sirve para que solo una persona pueda modificar un blob a la ves

await AcquireLease();

async Task AcquireLease()
{
    string connectionString = "DefaultEndpointsProtocol=https;AccountName=appstore20237;AccountKey=Lsk9U/W+AyCqq/x2TEqnJ1A81O3/vnGtSKlfIKQmbvA4quEU2xXilwGWdiGCciEa2CZftHPsiJIs+AStsLrAQQ==;EndpointSuffix=core.windows.net";
    string containerName = "scripts";
    string blobName = "avatar22.png";

    BlobClient blobClient = new BlobClient(connectionString, containerName,blobName);

    BlobLeaseClient blobLeaseClient = blobClient.GetBlobLeaseClient();

    TimeSpan leaseTiem = new TimeSpan(0, 0, 1, 00);

    Response<BlobLease> response = await blobLeaseClient.AcquireAsync(leaseTiem);

    Console.WriteLine("Leae id is {0}", response.Value.LeaseId);
}
*/