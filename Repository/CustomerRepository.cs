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
}