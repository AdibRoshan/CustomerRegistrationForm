using CustomerRegistrationForm.Model;
using System.Data.SqlClient;

namespace CustomerRegistrationForm.Data
    {
    public class CustomerRepository
        {
        private readonly string _connectionString;

        public CustomerRepository ( string connectionString )
            {
            _connectionString = connectionString;
            }

        public List<Customer> GetAll ( )
            {
            var customers = new List<Customer> ( );
            try
                {
                using ( var connection = new SqlConnection ( _connectionString ) )
                    {
                    connection.Open ( );
                    using ( var command = new SqlCommand ( "SELECT * FROM Customers" , connection ) )
                        {
                        using ( var reader = command.ExecuteReader ( ) )
                            {
                            while ( reader.Read ( ) )
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

        public Customer GetById ( int id )
            {
            try
                {
                using ( var connection = new SqlConnection ( _connectionString ) )
                    {
                    connection.Open ( );
                    using ( var command = new SqlCommand ( "SELECT * FROM Customers WHERE Id = @Id" , connection ) )
                        {
                        command.Parameters.AddWithValue ( "@Id" , id );
                        using ( var reader = command.ExecuteReader ( ) )
                            {
                            if ( reader.Read ( ) )
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

        public void Add ( Customer customer )
            {
            try
                {
                using ( var connection = new SqlConnection ( _connectionString ) )
                    {
                    connection.Open ( );
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

                        customer.Id = Convert.ToInt32 ( command.ExecuteScalar ( ) );
                        }
                    }
                }
            catch ( SqlException ex )
                {
                throw new Exception ( "An error occurred while adding the customer." , ex );
                }
            }

        public Customer Update ( Customer customer )
            {
            try
                {
                using ( var connection = new SqlConnection ( _connectionString ) )
                    {
                    connection.Open ( );
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

                        command.ExecuteNonQuery ( );
                        }
                    }
                }
            catch ( SqlException ex )
                {
                throw new Exception ( "An error occurred while updating the customer." , ex );
                }
            return customer;
            }

        public void Delete ( int id )
            {
            try
                {
                using ( var connection = new SqlConnection ( _connectionString ) )
                    {
                    connection.Open ( );
                    using ( var command = new SqlCommand ( "DELETE FROM Customers WHERE Id = @Id" , connection ) )
                        {
                        command.Parameters.AddWithValue ( "@Id" , id );
                        command.ExecuteNonQuery ( );
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
