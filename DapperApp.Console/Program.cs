//Print Result AddQuery in Console

using System.Threading.Channels;
using Repository;

var repository = new CustomerRepository();

// Console.WriteLine(repository.AddByReturnId());
var orders = repository.GetInvoice();
//repository.QuerMultiple();
//repository.Queries();
Console.WriteLine();
// var orders = repository.RunSP();


    // var orders = repository.GetOrders();
    //
    // foreach (var item in orders)
    // {
    //     Console.WriteLine($"Order :{ item.Id }   On Date {item.Date}");
    //     foreach (var detail in item.OrderDetails)
    //     {
    //         Console.WriteLine($"Id :{ detail.Id }     {detail.ProductName}");
    //
    //     }
    // }