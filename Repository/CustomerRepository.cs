using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Repository;

public interface ICustomerRepository
{
    int Add(CustomerDto customer);
    int Add(List<CustomerDto> customers);

    int Delete(long Id);

    int Delete(List<CustomerDto> Ids);

    int Update(CustomerDto customer);
    int Update(List<CustomerDto> customers);

    List<CustomerDto> GetCustomers();

    CustomerDto Find(long id);
    int AddByReturnId();

}

public class CustomerDto
{
    public long Id { get; set; }
    public string Name { get; set; }

    public string LastName { get; set; }
}
public class CustomerRepository : ICustomerRepository
{
    private readonly string connectionString = @"Server=DESKTOP-6P4U7CC;Initial Catalog=DapperDb; Integrated Security=True";

    public int Add(CustomerDto customer)
    {
        string sql = "INSERT INTO Customer (Name , LastName) Values (@Name , @LastName)";
        var connection = new SqlConnection(connectionString);
        // تعداد رکورد هایی که اضاف شده را برمیگرداند
        // var result = connection.Execute(sql,new {Name=customer.Name,LastName=customer.LastName});
        var result = connection.Execute(sql,customer);
        return result;
    }
    
    public int Add(List<CustomerDto> customers)
    {
        string sql = "INSERT INTO Customer (Name , LastName) Values (@Name , @LastName)";
        var connection = new SqlConnection(connectionString);
        // تعداد رکورد هایی که اضاف شده را برمیگرداند
        var result = connection.Execute(sql,customers);
        return result;
    }

    public int Delete(long Id)
    {
        string sql = "DELETE FROM Customer WHERE Id = @Id ";
        var connection = new SqlConnection(connectionString);
        var result = connection.Execute(sql,new {Id = Id});
        return result;
    }
    
    public int Delete(List<CustomerDto> Ids)
    {
        string sql = "DELETE FROM Customer WHERE Id = @Id ";
        var connection = new SqlConnection(connectionString);
        var result = connection.Execute(sql,Ids);
        return result;
    }

    public int Update(CustomerDto customer)
    {
        string sql = "UPDATE Customer SET Name = @Name , LastName = @LastName WHERE Id = @Id";
        var connection = new SqlConnection(connectionString);
        // تعداد رکورد هایی که اضاف شده را برمیگرداند
        var result = connection.Execute(sql,customer);
        return result;
    }

    public int Update(List<CustomerDto> customers)
    {
        string sql = "UPDATE Customer SET Name = @Name , LastName = @LastName WHERE Id = @Id";
        var connection = new SqlConnection(connectionString);
        // تعداد رکورد هایی که اضاف شده را برمیگرداند
        var result = connection.Execute(sql,customers);
        return result;
    }

    public List<CustomerDto> GetCustomers()
    {
        string sql = "SELECT * FROM Customer";
        var connection =new SqlConnection(connectionString);
        var result = connection.Query<CustomerDto>(sql);
        return result.ToList();
    }

    public CustomerDto Find(long id)
    {
        string sql = "SELECT * FROM Customer WHERE Id = @id";
        var connection =new SqlConnection(connectionString);
        var result = connection.Query<CustomerDto>(sql,new {Id = id}).FirstOrDefault();
        return result;
    }

    public int AddByReturnId()
    {
        string sql = "INSERT INTO Customer (Name,LastName) VALUES ( @Name , @LastName); SELECT SCOPE_IDENTITY()";
        var connection = new SqlConnection(connectionString);
        var result = connection.QuerySingle<int>(sql, new { Name = "ALi", LastName = "Shams" });
        return result;
    }

    public List<Order> GetOrders()
    {
        string sql = "SELECT TOP 10  * FROM Orders AS O INNER JOIN OrderDetails AS OD ON O.Id = OD.OrderId";
        var connection = new SqlConnection(connectionString);
        var orderDic = new Dictionary<long, Order>();

        var OrderList = connection.Query<Order, OrderDetail, Order>(sql,
                (order, orderDetail) =>
                {
                    Order entity;
                    if (!orderDic.TryGetValue(order.Id, out entity))
                    {
                        entity = order;
                        entity.OrderDetails = new List<OrderDetail>();
                        orderDic.Add(entity.Id, entity);
                    }
                    entity.OrderDetails.Add(orderDetail);
                    return entity;
                },
                splitOn: "Id")
            .Distinct()
            .ToList();
        return OrderList;
    }
    public List<Order> RunSP()
    {
        string sql = "SELECTOrders";
        var connection = new SqlConnection(connectionString);
        var result = connection.Query<Order>(sql, CommandType.StoredProcedure);
        return result.ToList();

    }

    public void QuerMultiple()
    {
        string sql = "SELECT * FROM Orders ; SELECT * FROM Invoice ;  ";
        var connection = new SqlConnection(connectionString);
        var result = connection.QueryMultiple(sql);

        var orders = result.Read<Order>().ToList();
        var invoice = result.Read<Invoice>().ToList();

    }   
    public void Queries()
    {
        string sql = "SELECT * FROM Orders ;";
        var connection = new SqlConnection(connectionString);
        //first Item in Orders
        var result = connection.QueryFirst<Order>(sql);
        
        var resultDefault = connection.QueryFirstOrDefault<Order>(sql);
        var resultSingle = connection.QuerySingle<Order>(sql);
        var resultSingleOrDefault = connection.QuerySingleOrDefault<Order>(sql);

            

    }

    public List<Order> GetInvoice()
    {
        string sql = "SELECT * FROM Orders  AS O INNER JOIN Invoice AS I ON O.Id = I.Id";
        var connection = new SqlConnection(connectionString);

        var invoice = connection.Query<Order, Invoice, Order>(
                sql, (order, invoice) =>
                {
                    order.Invoice = invoice;
                    return order;
                }, splitOn: "Id")
            .Distinct()
            .ToList();
        return invoice;
    }
    
    public class Order
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Invoice Invoice { get; set; }
    }
    public class OrderDetail
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string ProductName { get; set; }
    }
    public class Invoice
    {
        public long Id { get; set; }
        public int Price { get; set; }
    }
    
}