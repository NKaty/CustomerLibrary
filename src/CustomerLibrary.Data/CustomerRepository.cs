using System;
using System.Data;
using System.Data.SqlClient;

namespace CustomerLibrary.Data
{
    public class CustomerRepository : BaseRepository
    {
        public int Create(Customer customer)
        {
            using var connection = GetConnection();

            return Create(customer, connection);
        }

        public int Create(Customer customer, SqlConnection connection, SqlTransaction transaction = null)
        {
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

            var command = new SqlCommand(sql, connection, transaction);

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

            return Read(customerId, connection);
        }

        public Customer Read(int customerId, SqlConnection connection, SqlTransaction transaction = null)
        {
            var sql = @"SELECT * FROM [dbo].[Customers]
	                    WHERE [CustomerID] = @CustomerID";

            var command = new SqlCommand(sql, connection, transaction);

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
                        Email = reader["Email"]?.ToString(),
                        PhoneNumber = reader["PhoneNumber"]?.ToString(),
                        TotalPurchasesAmount = (decimal) reader["TotalPurchasesAmount"]
                    };
                }
            }

            return null;
        }

        public void Update(Customer customer)
        {
            using var connection = GetConnection();

            Update(customer, connection);
        }

        public void Update(Customer customer, SqlConnection connection, SqlTransaction transaction = null)
        {
            var sql = @"UPDATE [dbo].[Customers]
	                    SET [FirstName] = @FirstName,
		                    [LastName] = @LastName,
		                    [PhoneNumber] = @PhoneNumber,
		                    [Email] = @Email,
		                    [TotalPurchasesAmount] = @TotalPurchasesAmount
	                    WHERE [CustomerID] = @CustomerID";

            var command = new SqlCommand(sql, connection, transaction);

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
            var transaction = GetTransaction(connection);

            Delete(customerId, connection, transaction);
        }

        public void Delete(int customerId, SqlConnection connection, SqlTransaction transaction)
        {
            var deleteAddressesSql = @"DELETE FROM [dbo].[Addresses]
	                                   WHERE [CustomerID] = @CustomerID";

            var deleteCustomerSql = @"DELETE FROM [dbo].[Customers]
	                                  WHERE [CustomerID] = @CustomerID";

            try
            {
                var deleteAddressesCommand = new SqlCommand(deleteAddressesSql, connection, transaction);

                var customerIdParamForAddresses = new SqlParameter("@CustomerID", SqlDbType.Int)
                {
                    Value = customerId
                };

                deleteAddressesCommand.Parameters.Add(customerIdParamForAddresses);

                var deleteCustomerCommand = new SqlCommand(deleteCustomerSql, connection, transaction);

                var customerIdParamForCustomer = new SqlParameter("@CustomerID", SqlDbType.Int)
                {
                    Value = customerId
                };

                deleteCustomerCommand.Parameters.Add(customerIdParamForCustomer);

                deleteAddressesCommand.ExecuteNonQuery();
                deleteCustomerCommand.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        public void DeleteAll()
        {
            using var connection = GetConnection();

            DeleteAll(connection);
        }

        public void DeleteAll(SqlConnection connection, SqlTransaction transaction = null)
        {
            var deleteAddressesCommand = new SqlCommand(
                @"DELETE FROM [dbo].[Addresses]", connection, transaction);

            deleteAddressesCommand.ExecuteNonQuery();

            var deleteCustomersCommand = new SqlCommand(
                @"DELETE FROM [dbo].[Customers]", connection, transaction);

            deleteCustomersCommand.ExecuteNonQuery();
        }
    }
}