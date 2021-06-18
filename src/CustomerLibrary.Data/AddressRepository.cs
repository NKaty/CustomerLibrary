using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CustomerLibrary.Data
{
    public class AddressRepository : BaseRepository,  IDependentRepository<Address>
    {
        public int Create(Address address)
        {
            var newAddressId = 0;

            using var connection = GetConnection();

            var sql = @"INSERT INTO[dbo].[Addresses] (
                            [CustomerID],
                            [AddressLine],
                            [AddressLine2], 
                            [AddressType], 
                            [City], 
                            [PostalCode], 
                            [State], 
                            [Country]
                        ) VALUES (
                            @CustomerID,
                            @AddressLine,
                            @AddressLine2,
                            @AddressType,
                            @City,
                            @PostalCode,
                            @State,
                            @Country
                        );
                        SELECT CAST(scope_identity() AS int)";

            var command = new SqlCommand(sql, connection);

            var customerIdParam = new SqlParameter("@CustomerID", SqlDbType.Int)
            {
                Value = address.CustomerId
            };

            var addressLineParam = new SqlParameter("@AddressLine", SqlDbType.NVarChar, 100)
            {
                Value = address.AddressLine
            };

            var addressLine2Param = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100)
            {
                Value = address.AddressLine2
            };


            var addressTypeParam = new SqlParameter("@AddressType", SqlDbType.NVarChar, 10)
            {
                Value = address.AddressType
            };

            var cityParam = new SqlParameter("@City", SqlDbType.NVarChar, 50)
            {
                Value = address.City
            };

            var postalCodeParam = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 6)
            {
                Value = address.PostalCode
            };

            var stateParam = new SqlParameter("@State", SqlDbType.NVarChar, 20)
            {
                Value = address.State
            };

            var countryParam = new SqlParameter("@Country", SqlDbType.NVarChar, 15)
            {
                Value = address.Country
            };

            command.Parameters.Add(customerIdParam);
            command.Parameters.Add(addressLineParam);
            command.Parameters.Add(addressLine2Param);
            command.Parameters.Add(addressTypeParam);
            command.Parameters.Add(cityParam);
            command.Parameters.Add(postalCodeParam);
            command.Parameters.Add(stateParam);
            command.Parameters.Add(countryParam);

            var response = command.ExecuteScalar();
            if (response is not null)
            {
                newAddressId = (int) response;
            }

            address.AddressId = newAddressId;

            return newAddressId;
        }

        public Address Read(int addressId)
        {
            using var connection = GetConnection();

            var sql = @"SELECT * FROM [dbo].[Addresses]
	                    WHERE [AddressID] = @AddressID";

            var command = new SqlCommand(sql, connection);

            var addressIdParam = new SqlParameter("@AddressID", SqlDbType.Int)
            {
                Value = addressId
            };

            command.Parameters.Add(addressIdParam);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var addressType = reader["AddressType"]?.ToString();

                    if (addressType is not null)
                    {
                        return new Address
                        {
                            AddressId = (int) reader["AddressID"],
                            CustomerId = (int) reader["CustomerID"],
                            AddressLine = reader["AddressLine"]?.ToString(),
                            AddressLine2 = reader["AddressLine2"]?.ToString(),
                            AddressType = (AddressTypes) Enum.Parse(typeof(AddressTypes), addressType),
                            City = reader["City"]?.ToString(),
                            Country = reader["Country"]?.ToString(),
                            State = reader["State"]?.ToString(),
                            PostalCode = reader["PostalCode"]?.ToString()
                        };
                    }
                }
            }

            return null;
        }

        public List<Address> ReadByCustomerId(int customerId)
        {
            using var connection = GetConnection();

            var sql = @"SELECT * FROM [dbo].[Addresses]
	                    WHERE [CustomerID] = @CustomerID";

            var command = new SqlCommand(sql, connection);

            var customerIdParam = new SqlParameter("@CustomerID", SqlDbType.Int)
            {
                Value = customerId
            };

            command.Parameters.Add(customerIdParam);

            var addresses = new List<Address>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var addressType = reader["AddressType"]?.ToString();

                    if (addressType is not null)
                    {
                        addresses.Add(new Address
                        {
                            AddressId = (int) reader["AddressID"],
                            CustomerId = (int) reader["CustomerID"],
                            AddressLine = reader["AddressLine"]?.ToString(),
                            AddressLine2 = reader["AddressLine2"]?.ToString(),
                            AddressType = (AddressTypes) Enum.Parse(typeof(AddressTypes), addressType),
                            City = reader["City"]?.ToString(),
                            Country = reader["Country"]?.ToString(),
                            State = reader["State"]?.ToString(),
                            PostalCode = reader["PostalCode"]?.ToString()
                        });
                    }
                }
            }

            return addresses;
        }

        public void Update(Address address)
        {
            using var connection = GetConnection();

            var sql = @"UPDATE [dbo].[Addresses]
	                    SET [CustomerID] = @CustomerID,
		                    [AddressLine] =  @AddressLine,
		                    [AddressLine2] = @AddressLine2,
		                    [AddressType] = @AddressType,
		                    [City] = @City,
		                    [PostalCode] = @PostalCode,
		                    [State] = @State,
		                    [Country] = @Country
	                    WHERE [AddressID] = @AddressID";

            var command = new SqlCommand(sql, connection);

            var addressIdParam = new SqlParameter("@AddressID", SqlDbType.Int)
            {
                Value = address.AddressId
            };

            var customerIdParam = new SqlParameter("@CustomerID", SqlDbType.Int)
            {
                Value = address.CustomerId
            };

            var addressLineParam = new SqlParameter("@AddressLine", SqlDbType.NVarChar, 100)
            {
                Value = address.AddressLine
            };

            var addressLine2Param = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100)
            {
                Value = address.AddressLine2
            };


            var addressTypeParam = new SqlParameter("@AddressType", SqlDbType.NVarChar, 10)
            {
                Value = address.AddressType
            };

            var cityParam = new SqlParameter("@City", SqlDbType.NVarChar, 50)
            {
                Value = address.City
            };

            var postalCodeParam = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 6)
            {
                Value = address.PostalCode
            };

            var stateParam = new SqlParameter("@State", SqlDbType.NVarChar, 20)
            {
                Value = address.State
            };

            var countryParam = new SqlParameter("@Country", SqlDbType.NVarChar, 15)
            {
                Value = address.Country
            };

            command.Parameters.Add(addressIdParam);
            command.Parameters.Add(customerIdParam);
            command.Parameters.Add(addressLineParam);
            command.Parameters.Add(addressLine2Param);
            command.Parameters.Add(addressTypeParam);
            command.Parameters.Add(cityParam);
            command.Parameters.Add(postalCodeParam);
            command.Parameters.Add(stateParam);
            command.Parameters.Add(countryParam);

            command.ExecuteNonQuery();
        }

        public void Delete(int addressId)
        {
            using var connection = GetConnection();

            var sql = @"DELETE FROM [dbo].[Addresses]
	                    WHERE[AddressID] = @AddressID";

            var addressIdParam = new SqlParameter("@AddressID", SqlDbType.Int)
            {
                Value = addressId
            };

            var command = new SqlCommand(sql, connection);

            command.Parameters.Add(addressIdParam);

            command.ExecuteNonQuery();
        }

        public void DeleteAll()
        {
            using var connection = GetConnection();

            var sql = @"DELETE FROM [dbo].[Addresses]";

            var command = new SqlCommand(sql, connection);

            command.ExecuteNonQuery();
        }
    }
}