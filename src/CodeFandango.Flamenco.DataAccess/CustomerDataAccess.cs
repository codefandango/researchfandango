using CodeFandango.Flamenco.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeFandango.Flamenco.DataAccess
{
    public class CustomerDataAccess : IObjectDataAccess<Customer>
    {
        private DataAccess data;

        public CustomerDataAccess(DataAccess dataAccess)
        {
            this.data = dataAccess;
        }

        public async Task<Customer> CreateObject(Customer customer)
        {
            if (data.Database.Customers.Any(s => s.Id == customer.Id))
            {
                throw new Exception("Customer already exists");
            }

            data.Database.Customers.Add(customer);
            await data.Database.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> GetObject(long id)
        {
            return await data.Database.Customers.FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception("Customer not found.");
        }

        public async Task<List<Customer>> GetObjects()
        {
            return await data.Database.Customers.ToListAsync();
        }

        public async Task<IQueryable<Customer>> QueryObjects()
        {
            return await Task.FromResult(data.Database.Customers);
        }

        public Task<Customer> UpdateObject(Customer customer)
        {
            if (!data.Database.Customers.Any(s => s.Id == customer.Id))
            {
                throw new Exception("Customer not found");
            }

            data.Database.Customers.Update(customer);
            return Task.FromResult(customer);
        }
    }
}