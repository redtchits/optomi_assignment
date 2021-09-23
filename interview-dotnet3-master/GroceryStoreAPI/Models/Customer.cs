
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace GroceryStoreAPI.Models
{
    public class Customer
    {       
        public Response GetCustomers() 
        {
            return new Response {Success = true, Data =  GetRecordsFromFile()};          
        }
       
        public Response GetCustomers(int id)
        {
            CustomerList result = new CustomerList
            {
                Customers = GetRecordsFromFile().Customers.Where(x => x.Id == id).ToList()
            };

            return new Response { Success = true, Data = result };          
        }

        public Response AddCustomer(Person v) 
        {
            CustomerList records = GetRecordsFromFile();

            if (records.Customers.Any(x => x.Id == v.Id)) 
            {
                return new Response { Success = false, Message = "Provided id already exists." };              
            }

            records.Customers.Add(v);

            string json = JsonConvert.SerializeObject(records);
            
            System.IO.File.WriteAllText(@"database.json", json);

            return new Response { Success = true };
        }

        public Response UpdateCustomer(Person v)
        {
            CustomerList records = GetRecordsFromFile();

            if (!records.Customers.Exists(x => x.Id == v.Id))
            {
                return new Response { Success = false, Message = "No match found for provided id." };                
            }

            var recordToUpdate = records.Customers.FirstOrDefault(x => x.Id == v.Id);

            records.Customers.Remove(recordToUpdate);

            recordToUpdate.Name = v.Name;

            records.Customers.Add(recordToUpdate);

            string json = JsonConvert.SerializeObject(records);

            System.IO.File.WriteAllText(@"database.json", json);

            return new Response { Success = true };
        }

        public Response DeleteCustomer(int id)
        {
            CustomerList records = GetRecordsFromFile();
            
            if (!records.Customers.Exists(x => x.Id == id))
            {
                return new Response { Success = false, Message = "No match found for provided id." };                
            }

            var recordToDelete = records.Customers.FirstOrDefault(x => x.Id == id);
            records.Customers.Remove(recordToDelete);

            string json = JsonConvert.SerializeObject(records);

            System.IO.File.WriteAllText(@"database.json", json);

            return new Response { Success = true };
        }

        private CustomerList GetRecordsFromFile() 
        {
            CustomerList result = new CustomerList();
            
            using (StreamReader r = new StreamReader("database.json"))
            {
                string json = r.ReadToEnd();
                result = JsonConvert.DeserializeObject<CustomerList>(json);
            }

            return result;
        }
    }

    public class CustomerList
    {
        public List<Person> Customers {get; set;}
    }

    public class Person 
    {
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Invalid id provided.")]
        public int Id { get; set; }
        
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Name contains invalid characters.")]
        public string Name { get; set; }
    }
}
