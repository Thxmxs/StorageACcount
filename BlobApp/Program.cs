using Azure;
using Azure.Data.Tables;
using BlobApp;

string connectionString = "ConnectionString";
string tableName = "Orders";

//instalar azure.data.Tables





//AÑADIR ENTIDADES A UN TABLE

void AddEntity(string OrderId,string category, int quantity)
{
    TableClient tableClient = new TableClient(connectionString,tableName);

    TableEntity tableEntity = new TableEntity(category, OrderId)
    {
        { "quantity", quantity }//add extra values
    };
    tableClient.AddEntity(tableEntity);

    Console.WriteLine("Added entity");
}

AddEntity("1", "Laptop", 200);
AddEntity("2", "PC", 200);
AddEntity("3", "Laptop", 400);
AddEntity("4", "Muebles", 100);



//OBTENER ENTIDADES

void GetEntities(string category)
{
    TableClient tableClient = new TableClient(connectionString, tableName);
    Pageable<TableEntity> results = tableClient.Query<TableEntity>(entity => entity.PartitionKey == category);

    foreach (var ent in results)
    {
        Console.WriteLine(ent.RowKey);
        Console.WriteLine("Quantity IS {0}",ent.GetInt32("quantity"));
    }
}


GetEntities("Laptop");


//BORRAR ENTIDADES


void DeleteEntity(string category,string orderID)
{
    TableClient tableClient = new TableClient(connectionString, tableName);
    tableClient.DeleteEntity(category, orderID);

    Console.WriteLine("Entity deleted");

}

DeleteEntity("Laptop", "1");


//ACTUALIZR ENTIDADES

void UpdateEntity(string category, string orderID, int quantity)
{
    // Let's first get the entity that we want to update
    TableClient tableClient = new TableClient(connectionString, tableName);
    Order order = tableClient.GetEntity<Order>(category, orderID);
    order.quantity = quantity;

    tableClient.UpdateEntity<Order>(order, ifMatch: ETag.All, TableUpdateMode.Replace);

    Console.WriteLine("Entity updated");
}
