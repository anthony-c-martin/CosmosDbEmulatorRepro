using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;

namespace CosmosDbEmulatorRepro
{
    public static class DocumentClientExtensions
    {
        public static Task CreateDatabaseIfNotExists(this DocumentClient documentClient, string databaseId)
        {
            return documentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseId });
        }

        public static Task CreateCollectionIfNotExists(this DocumentClient documentClient, string databaseId, string collectionId, string partitionKey = "/partitionKey", int timeToLive = -1)
        {
            var documentCollection = new DocumentCollection
            {
                Id = collectionId,
                DefaultTimeToLive = timeToLive
            };

            if (!string.IsNullOrEmpty(partitionKey))
            {
                documentCollection.PartitionKey.Paths.Add(partitionKey);
            }

            return documentClient
                .CreateDocumentCollectionIfNotExistsAsync(
                    databaseUri: UriFactory.CreateDatabaseUri(databaseId),
                    documentCollection: documentCollection);
        }
    }
}
