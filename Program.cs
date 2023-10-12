using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace CosmosDbEmulatorRepro
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var enableDirectConnectionMode = true;
            var enableTcpProtocol = false;

            var serviceEndpoint = new Uri("https://localhost:8081/");
            var connectionPolicy = new ConnectionPolicy
            {
                ConnectionMode = enableDirectConnectionMode ? ConnectionMode.Direct : ConnectionMode.Gateway,
                ConnectionProtocol = (enableDirectConnectionMode && enableTcpProtocol) ? Protocol.Tcp : Protocol.Https
            };

            var authKey = new SecureString();
            "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==".ToCharArray().ToList().ForEach(authKey.AppendChar);

            var documentClient = new DocumentClient(serviceEndpoint, authKey, connectionPolicy);

            await CreateCollection(documentClient, "RbacStore", "RbacPolicies", "/PartitionKey");
        }

        private static async Task CreateCollection(
            DocumentClient documentClient,
            string databaseId,
            string collectionId,
            string partitionKey = "/partitionKey",
            int timeToLive = 900)
        {
            await documentClient.CreateDatabaseIfNotExists(databaseId);
            await documentClient.CreateCollectionIfNotExists(databaseId, collectionId, partitionKey, timeToLive);
        }
    }
}
