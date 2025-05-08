using CustomerRegistrationForm.Interface;
using CustomerRegistrationForm.Model;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CustomerRegistrationForm.Data
    {
    public class CustomerService : IService
        {
        private readonly string _connectionString;

        public CustomerService ( string connectionString )
            {
            _connectionString = connectionString;
            }

        public async Task<List<Customer>> GetAllAsync ( )
            {
            var customers = new List<Customer> ( );
            try
                {
                using ( var connection = new SqlConnection ( _connectionString ) )
                    {
                    await connection.OpenAsync ( );
                    using ( var command = new SqlCommand ( "SELECT * FROM Customers" , connection ) )
                        {
                        using ( var reader = await command.ExecuteReaderAsync ( ) )
                            {
                            while ( await reader.ReadAsync ( ) )
                                {
                                customers.Add ( new Customer
                                    {
                                    Id = reader.GetInt32 ( reader.GetOrdinal ( "Id" ) ) ,
                                    FirstName = reader.GetString ( reader.GetOrdinal ( "FirstName" ) ) ,
                                    LastName = reader.GetString ( reader.GetOrdinal ( "LastName" ) ) ,
                                    Email = reader.GetString ( reader.GetOrdinal ( "Email" ) ) ,
                                    Phone = reader.GetString ( reader.GetOrdinal ( "Phone" ) ) ,
                                    DateOfBirth = reader.GetDateTime ( reader.GetOrdinal ( "DateOfBirth" ) ) ,
                                    Address = reader.GetString ( reader.GetOrdinal ( "Address" ) ) ,
                                    EmployerName = reader.GetString ( reader.GetOrdinal ( "EmployerName" ) ) ,
                                    JobTitle = reader.GetString ( reader.GetOrdinal ( "JobTitle" ) ) ,
                                    Salary = reader.GetDecimal ( reader.GetOrdinal ( "Salary" ) ) ,
                                    EmploymentStartDate = reader.GetDateTime ( reader.GetOrdinal ( "EmploymentStartDate" ) )
                                    } );
                                }
                            }
                        }
                    }
                }
            catch ( SqlException ex )
                {
                throw new Exception ( "An error occurred while retrieving customers." , ex );
                }

            return customers;
            }

        public async Task<Customer> GetByIdAsync ( int id )
            {
            try
                {
                using ( var connection = new SqlConnection ( _connectionString ) )
                    {
                    await connection.OpenAsync ( );
                    using ( var command = new SqlCommand ( "SELECT * FROM Customers WHERE Id = @Id" , connection ) )
                        {
                        command.Parameters.AddWithValue ( "@Id" , id );
                        using ( var reader = await command.ExecuteReaderAsync ( ) )
                            {
                            if ( await reader.ReadAsync ( ) )
                                {
                                return new Customer
                                    {
                                    Id = reader.GetInt32 ( reader.GetOrdinal ( "Id" ) ) ,
                                    FirstName = reader.GetString ( reader.GetOrdinal ( "FirstName" ) ) ,
                                    LastName = reader.GetString ( reader.GetOrdinal ( "LastName" ) ) ,
                                    Email = reader.GetString ( reader.GetOrdinal ( "Email" ) ) ,
                                    Phone = reader.GetString ( reader.GetOrdinal ( "Phone" ) ) ,
                                    DateOfBirth = reader.GetDateTime ( reader.GetOrdinal ( "DateOfBirth" ) ) ,
                                    Address = reader.GetString ( reader.GetOrdinal ( "Address" ) ) ,
                                    EmployerName = reader.GetString ( reader.GetOrdinal ( "EmployerName" ) ) ,
                                    JobTitle = reader.GetString ( reader.GetOrdinal ( "JobTitle" ) ) ,
                                    Salary = reader.GetDecimal ( reader.GetOrdinal ( "Salary" ) ) ,
                                    EmploymentStartDate = reader.GetDateTime ( reader.GetOrdinal ( "EmploymentStartDate" ) )
                                    };
                                }
                            return null;
                            }
                        }
                    }
                }
            catch ( SqlException ex )
                {
                throw new Exception ( "An error occurred while retrieving the customer." , ex );
                }
            }

        public async Task AddAsync ( Customer customer )
            {
            try
                {
                using ( var connection = new SqlConnection ( _connectionString ) )
                    {
                    await connection.OpenAsync ( );
                    using ( var command = new SqlCommand (
                        "INSERT INTO Customers (FirstName, LastName, Email, Phone, DateOfBirth, Address, EmployerName, JobTitle, Salary, EmploymentStartDate) " +
                        "VALUES (@FirstName, @LastName, @Email, @Phone, @DateOfBirth, @Address, @EmployerName, @JobTitle, @Salary, @EmploymentStartDate); " +
                        "SELECT SCOPE_IDENTITY();" , connection ) )
                        {
                        command.Parameters.AddWithValue ( "@FirstName" , customer.FirstName );
                        command.Parameters.AddWithValue ( "@LastName" , customer.LastName );
                        command.Parameters.AddWithValue ( "@Email" , customer.Email );
                        command.Parameters.AddWithValue ( "@Phone" , customer.Phone );
                        command.Parameters.AddWithValue ( "@DateOfBirth" , customer.DateOfBirth );
                        command.Parameters.AddWithValue ( "@Address" , customer.Address );
                        command.Parameters.AddWithValue ( "@EmployerName" , customer.EmployerName );
                        command.Parameters.AddWithValue ( "@JobTitle" , customer.JobTitle );
                        command.Parameters.AddWithValue ( "@Salary" , customer.Salary );
                        command.Parameters.AddWithValue ( "@EmploymentStartDate" , customer.EmploymentStartDate );

                        customer.Id = Convert.ToInt32 ( await command.ExecuteScalarAsync ( ) );
                        }
                    }
                }
            catch ( SqlException ex )
                {
                throw new Exception ( "An error occurred while adding the customer." , ex );
                }
            }

        public async Task<Customer> UpdateAsync ( Customer customer )
            {
            try
                {
                using ( var connection = new SqlConnection ( _connectionString ) )
                    {
                    await connection.OpenAsync ( );
                    using ( var command = new SqlCommand (
                        @"UPDATE Customers SET 
                        FirstName = @FirstName, 
                        LastName = @LastName, 
                        Email = @Email, 
                        Phone = @Phone,
                        DateOfBirth = @DateOfBirth, 
                        Address = @Address, 
                        EmployerName = @EmployerName, 
                        JobTitle = @JobTitle,
                        Salary = @Salary, 
                        EmploymentStartDate = @EmploymentStartDate 
                        WHERE Id = @Id" , connection ) )
                        {
                        command.Parameters.AddWithValue ( "@FirstName" , customer.FirstName );
                        command.Parameters.AddWithValue ( "@LastName" , customer.LastName );
                        command.Parameters.AddWithValue ( "@Email" , customer.Email );
                        command.Parameters.AddWithValue ( "@Phone" , customer.Phone );
                        command.Parameters.AddWithValue ( "@DateOfBirth" , customer.DateOfBirth );
                        command.Parameters.AddWithValue ( "@Address" , customer.Address );
                        command.Parameters.AddWithValue ( "@EmployerName" , customer.EmployerName );
                        command.Parameters.AddWithValue ( "@JobTitle" , customer.JobTitle );
                        command.Parameters.AddWithValue ( "@Salary" , customer.Salary );
                        command.Parameters.AddWithValue ( "@EmploymentStartDate" , customer.EmploymentStartDate );
                        command.Parameters.AddWithValue ( "@Id" , customer.Id );

                        await command.ExecuteNonQueryAsync ( );
                        }
                    }
                }
            catch ( SqlException ex )
                {
                throw new Exception ( "An error occurred while updating the customer." , ex );
                }
            return customer;
            }

        public async Task DeleteAsync ( int id )
            {
            try
                {
                using ( var connection = new SqlConnection ( _connectionString ) )
                    {
                    await connection.OpenAsync ( );
                    using ( var command = new SqlCommand ( "DELETE FROM Customers WHERE Id = @Id" , connection ) )
                        {
                        command.Parameters.AddWithValue ( "@Id" , id );
                        await command.ExecuteNonQueryAsync ( );
                        }
                    }
                }
            catch ( SqlException ex )
                {
                throw new Exception ( "An error occurred while deleting the customer." , ex );
                }
            }
        }
    }
