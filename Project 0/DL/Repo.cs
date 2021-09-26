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

        public List<Customer> FindOneCustomer(string qryString)
        {
            return _context.Customers.Where(u => u.CustomerUserName.Contains(qryString) || u.CustomerName.Contains(qryString)).Select(
                x => new Models.Customer(){
                    CustomerId = x.CustomerId,
                    Name = x.CustomerName,
                    UserName = x.CustomerUserName,
                    Password = x.CustomerPassWord,
                    Email = x.CustomerEmail,
                    Address = x.CustomerAddress,
                    CustomerDefaultStoreID = x.CustomerStore
            }
            ).ToList();
        }

        public List<Inventory> GetInventoryByStoreID(Customer newCustomer)
        {

            return _context.Inventories.Where(y => y.InvenStoreId == newCustomer.CustomerDefaultStoreID).Select(z => new Models.Inventory ()
            {
                StoreID = z.InvenStoreId,
                Quantity = z.InventoryQuantity,
                ProductID = z.InvenProductId,
            }
            ).ToList();


            // Entity.Inventory storeById = _context.Inventories.Include(x => x.InvenProduct).FirstOrDefault(y => y.InvenStoreId== newCustomer.CustomerDefaultStoreID);
            // //StoreFronts.Include(r => r.Inventories).ThenInclude(s => s.InvenProduct).FirstOrDefault(y => y.StoreId == newCustomer.CustomerDefaultStoreID);

            // return new Models.Inventory(){
            //     StoreID = storeById.InvenStoreId,
            //     StoreName = storeById.InvenStore.StoreName,
            //     Name = storeById,
            //     Address = storeById.StoreAddress,
            //     Inventories = storeById.Inventories.Select(y => new Models.Product(){
            //         StoreID = y.InvenStoreId,
            //         ProductID = y.InvenProductId,
            //         Quantity = y.InvenProductId,
            //     }).ToList()
            // };

        }

        public List<StoreFront> GetStoreFronts()
        {
            return _context.StoreFronts.Select(r => new Models.StoreFront()
            {
                StoreID = r.StoreId,
                Name = r.StoreName,
                Address = r.StoreAddress
            }).ToList();
        }
    }

}