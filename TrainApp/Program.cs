using ConsoleTables;
using WholesaleEntities.Models;
using WholesaleEntities.DataBaseControllers;

void PrintTable<T>(IQueryable<T> dbSet) where T : class
{
    ConsoleTable table = new ConsoleTable();
    table.AddColumn(typeof(T).GetProperties().Select(x => x.Name).ToList());

    foreach(var data in dbSet)
    {
        table.AddRow(data.GetType().GetProperties().Select(x => x.GetValue(data)).ToArray());
    }
    table.Write();
}

using (WholesaleContext db = new WholesaleContext())
{
    // получаем объекты из бд и выводим на консоль
    var customers = db.Customers;
    Console.WriteLine("1.FullCustomersTable (Press Enter)");
    Console.ReadLine();

    PrintTable(customers);
    Console.WriteLine("2.CustomersTable with realeases more than 1(Press Enter)");
    Console.ReadLine();
    PrintTable(customers.Where(x => x.ReleaseReports.Count > 1));

    Console.WriteLine("3.Receip averange value(Press Enter)");
    Console.ReadLine();
    Console.WriteLine(db.ReceiptReports.Average(x => x.Volume));

    Console.WriteLine("4. (Press Enter)");
    Console.ReadLine();
    PrintTable(db.ReceiptReports.Join(db.Storages, x => x.StorageId, u => u.StorageId,
                        (x,u) => new
                        {
                            StorageName = u.Name,
                            Volume = x.Volume,
                            ProductName = x.Product.Name
                        }));

    Console.WriteLine("5. (Press Enter)");
    Console.ReadLine();
    PrintTable(db.ReceiptReports.Where(x => x.Volume > 80).Join(db.Storages, x => x.StorageId, u => u.StorageId,
                        (x, u) => new
                        {
                            StorageName = u.Name,
                            Volume = x.Volume,
                            ProductName = x.Product.Name
                        }));

    //6.....
    var newProvider = new Provaider()
    {
        Name = "newProw",
        Address = "add",
        TelephoneNumber = "+321312"
    };

    db.Provaiders.Add(newProvider);

    //7......
    var product = new Product()
    {
        Name = "newdsda",
        StorageConditions = "sdadasdasd",
        Package = "dasdas",
        StorageLife = new DateTime(2024, 4, 21)
    };

    product.Manufacturer = new Manufacturer()
    {
        Name = "fdsafas"
    };
    product.Type = new ProductType()
    {
        Name = "fdsfsdf",
        Description = "dksalkdkas",
        Feature = "sdkadjas"
    };

    db.Products.Add(product);

    //8........
    var manufacturer = db.Manufacturers.FirstOrDefault();
    db.Manufacturers.Remove(manufacturer);

    //9....
    var receipReport = db.ReceiptReports.FirstOrDefault();
    db.ReceiptReports.Remove(receipReport);
    //10...

    var customer = db.Customers.Where(x => x.ReleaseReports.Count > 1).FirstOrDefault();
    customer.Name = "Winner";
    db.SaveChanges();


}