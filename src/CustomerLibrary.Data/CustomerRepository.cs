using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CustomerLibrary.Data
{
    public class CustomerRepository : BaseRepository
    {
        public int Create(Customer customer)
        {
            using var connection = GetConnection();
            connection.Open();

            var newCustomerId = 0;

            var sql = @"INSERT INTO[dbo].[Customers] (
                            [FirstName],
                            [LastName],
                            [PhoneNumber],
                            [Email],
                            [TotalPurchasesAmount]
                        ) VALUES (
                            @FirstName,
                            @LastName,
                            @PhoneNumber,
                            @Email,
                            @TotalPurchasesAmount
                        );
                        SELECT CAST(scope_identity() AS int)";

            var command = new SqlCommand(sql, connection);

            var firstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50)
            {
                Value = customer.FirstName
            };

            var lastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 50)
            {
                Value = customer.LastName
            };

            var phoneNumberParam = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 15)
            {
                Value = customer.PhoneNumber
            };


            var emailParam = new SqlParameter("@Email", SqlDbType.NVarChar, 50)
            {
                Value = customer.Email
            };

            var totalPurchasesAmountParam = new SqlParameter("@TotalPurchasesAmount", SqlDbType.Money)
            {
                Value = customer.TotalPurchasesAmount
            };

            command.Parameters.Add(firstNameParam);
            command.Parameters.Add(lastNameParam);
            command.Parameters.Add(phoneNumberParam);
            command.Parameters.Add(emailParam);
            command.Parameters.Add(totalPurchasesAmountParam);

            var response = command.ExecuteScalar();
            if (response is not null)
            {
                newCustomerId = (int) response;
            }

            customer.CustomerId = newCustomerId;

            return newCustomerId;
        }

        public Customer Read(int customerId)
        {
            using var connection = GetConnection();
            connection.Open();

            var sql = @"SELECT * FROM [dbo].[Customers]
	                    WHERE [CustomerID] = @CustomerID";

            var command = new SqlCommand(sql, connection);

            var customerIdParam = new SqlParameter("@CustomerID", SqlDbType.Int)
            {
                Value = customerId
            };

            command.Parameters.Add(customerIdParam);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Customer
                    {
                        CustomerId = (int) reader["CustomerID"],
                        FirstName = reader["FirstName"]?.ToString(),
                        LastName = reader["LastName"]?.ToString(),
                        Addresses = new List<Address>(),
                        Email = reader["Email"]?.ToString(),
                        PhoneNumber = reader["PhoneNumber"]?.ToString(),
                        Notes = new List<Note>(),
                        TotalPurchasesAmount = (decimal) reader["TotalPurchasesAmount"]
                    };
                }
            }

            return null;
        }

        public void Update(Customer customer)
        {
            using var connection = GetConnection();
            connection.Open();

            var sql = @"UPDATE [dbo].[Customers]
	                    SET [FirstName] = @FirstName,
		                    [LastName] = @LastName,
		                    [PhoneNumber] = @PhoneNumber,
		                    [Email] = @Email,
		                    [TotalPurchasesAmount] = @TotalPurchasesAmount
	                    WHERE [CustomerID] = @CustomerID";

            var command = new SqlCommand(sql, connection);

            var customerIdParam = new SqlParameter("@CustomerID", SqlDbType.Int)
            {
                Value = customer.CustomerId
            };

            var firstNameParam = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50)
            {
                Value = customer.FirstName
            };

            var lastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 50)
            {
                Value = customer.LastName
            };

            var phoneNumberParam = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 15)
            {
                Value = customer.PhoneNumber
            };


            var emailParam = new SqlParameter("@Email", SqlDbType.NVarChar, 50)
            {
                Value = customer.Email
            };

            var totalPurchasesAmountParam = new SqlParameter("@TotalPurchasesAmount", SqlDbType.Money)
            {
                Value = customer.TotalPurchasesAmount
            };

            command.Parameters.Add(customerIdParam);
            command.Parameters.Add(firstNameParam);
            command.Parameters.Add(lastNameParam);
            command.Parameters.Add(phoneNumberParam);
            command.Parameters.Add(emailParam);
            command.Parameters.Add(totalPurchasesAmountParam);

            command.ExecuteNonQuery();
        }

        public void Delete(int customerId)
        {
            using var connection = GetConnection();
            connection.Open();

            var deleteAddressesSql = @"DELETE FROM [dbo].[Addresses]
	                                   WHERE [CustomerID] = @CustomerID";

            var deleteAddressesCommand = new SqlCommand(deleteAddressesSql, connection);

            var customerIdParamForAddresses = new SqlParameter("@CustomerID", SqlDbType.Int)
            {
                Value = customerId
            };

            deleteAddressesCommand.Parameters.Add(customerIdParamForAddresses);

            var deleteCustomerSql = @"DELETE FROM [dbo].[Customers]
	                                   WHERE [CustomerID] = @CustomerID;";

            var deleteCustomerCommand = new SqlCommand(deleteCustomerSql, connection);

            var customerIdParamForCustomer = new SqlParameter("@CustomerID", SqlDbType.Int)
            {
                Value = customerId
            };

            deleteCustomerCommand.Parameters.Add(customerIdParamForCustomer);

            deleteAddressesCommand.ExecuteNonQuery();
            deleteCustomerCommand.ExecuteNonQuery();
        }

        public void DeleteAll()
        {
            using var connection = GetConnection();
            connection.Open();

            var deleteAddressesCommand = new SqlCommand(
                @"DELETE FROM [dbo].[Addresses]", connection);

            deleteAddressesCommand.ExecuteNonQuery();

            var deleteCustomersCommand = new SqlCommand(
                @"DELETE FROM [dbo].[Customers]", connection);

            deleteCustomersCommand.ExecuteNonQuery();
        }
    }
}