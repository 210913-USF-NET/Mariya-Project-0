using System;
using System.Collections.Generic;
using Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entity = DL.Entities;
using DL;


namespace DL
{
    public class Repo : IRepo
    {
        private Entity.Project00Context _context;
        public Repo(Entity.Project00Context context)
        {
            _context = context;
        }

        public Models.Customer AddCustomer(Models.Customer newCustomer)
        {
            Entity.Customer customerToAdd = new Entity.Customer(){
               // CustomerId = newCustomer.CustomerId, ----should I add or just get from DB?
                CustomerName = newCustomer.Name,
                CustomerEmail = newCustomer.Email,
                CustomerUserName = newCustomer.UserName,
                CustomerPassWord = newCustomer.Password,
                CustomerAddress = newCustomer.Address,
                CustomerStore = newCustomer.CustomerDefaultStoreID
            };

            customerToAdd = _context.Add(customerToAdd).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Models.Customer(){
                CustomerId = customerToAdd.CustomerId,
                Name = customerToAdd.CustomerName,
                Email = customerToAdd.CustomerEmail,
                UserName = customerToAdd.CustomerUserName,
                Password = customerToAdd.CustomerPassWord,
                Address = customerToAdd.CustomerAddress,
                CustomerDefaultStoreID= customerToAdd.CustomerStore
            };
        }

        public Customer FindOneCustomer(string qryString)
        {
            Entity.Customer cutByUsername = _context.Customers.FirstOrDefault(u => u.CustomerUserName.Equals(qryString));

            return new Models.Customer(){
                CustomerId = cutByUsername.CustomerId,
                Name = cutByUsername.CustomerName,
                UserName = cutByUsername.CustomerUserName,
                Password = cutByUsername.CustomerPassWord,
                Email = cutByUsername.CustomerEmail,
                Address = cutByUsername.CustomerAddress,
                CustomerDefaultStoreID = cutByUsername.CustomerStore
            };

        }
    }

}