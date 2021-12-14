using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace StoredProcedureSqlAdo
{
    class Handler
    {
        private SqlConnection dbcon;
        public Handler()
        {
            string connectionString =
            "Data Source=LAPTOP-MOP66LEC\\SQLEXPRESS;Initial Catalog=TelerikAcademy99;"
            + "Integrated Security=true";
            dbcon = new SqlConnection(connectionString);
        }
        public void AddCustomer()
        {
            dbcon.Open();
            Console.WriteLine("Begin with adding a unique CustomerID (5 characters):" +
            "\n-----------------------------");
            string customerID = Console.ReadLine().ToUpper();
            Console.WriteLine("Add a Contact name (first- and lastname):" +
                "\n-----------------------------");
            string contactName = Console.ReadLine();
            Console.WriteLine("Add an address:" +
                "\n-----------------------------");
            string address = Console.ReadLine();
            Console.WriteLine("Add a phonenumber:" +
                "\n-----------------------------");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Add a Company name:" +
                "\n-----------------------------");
            string companyName = Console.ReadLine();
            Console.WriteLine("Add a contact title (if any):" +
                "\n-----------------------------");
            string contactTitle = Console.ReadLine();
            Console.WriteLine("Add which city the customer is located in:" +
                "\n-----------------------------");
            string city = Console.ReadLine();
            Console.WriteLine("Add which region the customer is located in:" +
                "\n-----------------------------");
            string region = Console.ReadLine();
            Console.WriteLine("Add a postalcode:" +
                "\n-----------------------------");
            string postalCode = Console.ReadLine();
            Console.WriteLine("Add which country they are located in:" +
                "\n-----------------------------");
            string country = Console.ReadLine();
            Console.WriteLine("Enter a fax number if any:" +
                "\n-----------------------------");
            string fax = Console.ReadLine();

           SqlCommand cmd = new SqlCommand()
           {
               CommandText = "spAddCustomer",
               Connection = dbcon,
               CommandType = CommandType.StoredProcedure
           };

            cmd.Parameters.AddWithValue("@CustomerId", customerID);
            cmd.Parameters.AddWithValue("@ContactName", contactName);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@Phone", phoneNumber);
            cmd.Parameters.AddWithValue("@CompanyName", companyName);
            cmd.Parameters.AddWithValue("@ContactTitle", contactTitle);
            cmd.Parameters.AddWithValue("@City", city);
            cmd.Parameters.AddWithValue("@Region", region);
            cmd.Parameters.AddWithValue("@PostalCode", postalCode);
            cmd.Parameters.AddWithValue("@Country", country);
            cmd.Parameters.AddWithValue("@Fax", fax);

            int returnValue = cmd.ExecuteNonQuery();

            Console.WriteLine("Customer is added!");
            dbcon.Close();

        }
        public void DeleteCustomer()
        {
            dbcon.Open();
            Console.WriteLine("Write the name or CustomerID you want to delete:");
            string userInput = Console.ReadLine();

            SqlCommand cmd = new SqlCommand()
            {
                CommandText = "spDeleteCustomer",
                Connection = dbcon,
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CustomerId", userInput);
            cmd.Parameters.AddWithValue("@ContactName", userInput);

            int returnValue = cmd.ExecuteNonQuery();
            Console.WriteLine("Customer is deleted!");
            dbcon.Close();
        }
        public void UpdateEmployee()
        {
            dbcon.Open();
            Console.WriteLine("Write the EmployeeID of the employee you want to change:");
            int EmployeeID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the new address:");
            string userAddress = Console.ReadLine();

            SqlCommand cmd = new SqlCommand()
            {
                CommandText = "spUpdateEmployee",
                Connection = dbcon,
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            cmd.Parameters.AddWithValue("@AddressText", userAddress);

            int returnValue = cmd.ExecuteNonQuery();
            Console.WriteLine("Address is updated!");
            dbcon.Close();
        }
        public void ShowCountrySales()
        {
            dbcon.Open();
            Console.WriteLine("Write which country you want to see the sales for:");
            string userCountry = Console.ReadLine();

            SqlCommand cmd = new SqlCommand()
            {
                CommandText = "spShowCountrySales",
                Connection = dbcon,
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@ShipCountry", userCountry);

            SqlDataReader reader = cmd.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    decimal Sales = (decimal)reader["Sales"];
                    string Country = (string)reader["Country"];
                    string Seller = (string)reader["Seller"];
                    Console.WriteLine("{0}, {1}, {2}", Sales, Country, Seller);
                }
            }
            int returnValue = cmd.ExecuteNonQuery();
            dbcon.Close();

        }
        public void AddNewOrder()
        {
            dbcon.Open();
            
            SqlCommand cmd = new SqlCommand()
            {
                CommandText = "spAddNewOrder",
                Connection = dbcon,
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CustomerID", "FRIWE");
            cmd.Parameters.AddWithValue("@EmployeeID", "1");
            cmd.Parameters.AddWithValue("@OrderDate", "2020-12-20");
            cmd.Parameters.AddWithValue("@RequiredDate", "2020-12-24");
            cmd.Parameters.AddWithValue("@ShippedDate", "2020-12-21");
            cmd.Parameters.AddWithValue("@ShipVia", "2");
            cmd.Parameters.AddWithValue("@Freight", "33");
            cmd.Parameters.AddWithValue("@ShipName", "Frillz");
            cmd.Parameters.AddWithValue("@ShipAddress", "Kattgatan 20");
            cmd.Parameters.AddWithValue("@ShipCity", "Kattrineholm");
            cmd.Parameters.AddWithValue("@ShipRegion", "KT");
            cmd.Parameters.AddWithValue("@ShipPostalCode", "66699");
            cmd.Parameters.AddWithValue("@ShipCountry", "Kattlandia");
            cmd.Parameters.AddWithValue("@ProductID", "1");
            cmd.Parameters.AddWithValue("@UnitPrice", "33");
            cmd.Parameters.AddWithValue("@Quantity", "1");
            cmd.Parameters.AddWithValue("@Discount", "0");

            int returnValue = cmd.ExecuteNonQuery();
            Console.WriteLine("Order is confirmed!");
            dbcon.Close();
        }
        public void DeleteOrderAndCustomer()
        {
            Console.WriteLine("Write the Customers ID that you want to delete:");
            string delCusID = Console.ReadLine();
            dbcon.Open();

            SqlCommand cmd = new SqlCommand()
            {
                CommandText = "spDeleteOrderAndCustomer",
                Connection = dbcon,
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@CustomerID", delCusID);

            int returnValue = cmd.ExecuteNonQuery();
            Console.WriteLine("The customer and its order is deleted!");
            dbcon.Close();
        }
    }
}
