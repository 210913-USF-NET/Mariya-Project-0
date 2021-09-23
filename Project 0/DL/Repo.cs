using System;
using System.Collections.Generic;
using Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entity = DL.Entities;
using Models;

namespace DL
{
    public class Repo : IRepo
    {
        private Entities.Project00Context _context;
        public Repo(Entity.Project00Context context)
        {
            _context = context;
        }

        public Models.Customer AddCustomer(Models.Customer newCustomer)
        {
            Entity.Customer custToAdd = new Entity.Customer(){
               // CustomerId = newCustomer.CustomerId, ----should I add or just get from DB?
                CustomerName = newCustomer.Name,
                CustomerEmail = newCustomer.Email,
                CustomerUserName = newCustomer.UserName,
                CustomerPassWord = newCustomer.Password,
                CustomerAddress = newCustomer.Address,
                CustomerStore = newCustomer.CustomerDefaultStoreID
            };

            custToAdd = _context.Add(custToAdd).Entity;
            _context.SaveChanges();

            return new Models.Customer(){
                CustomerId = custToAdd.CustomerId,
                Name = custToAdd.CustomerName,
                Email = custToAdd.CustomerEmail,
                UserName = custToAdd.CustomerUserName,
                Password = custToAdd.CustomerPassWord,
                Address = custToAdd.CustomerAddress,
                CustomerDefaultStoreID= custToAdd.CustomerStore
            };
        }

        public Models.LineItem AddLineItem(Models.LineItem newItem)
        {
           Entity.LineItem lineToAdd = new Entity.LineItem(){
               OrderId1 = newItem.OrderID,
               // need to maybe create order 1st then 
               //add from order because order ID needs to be created 1st
               //store line items in list and then once order created add to 
               //line item list and remove from inventory...?
               OrderProductQantity = newItem.Quantity,
               OrderInvenId = newItem.Item.ProductId,
                };
           lineToAdd = _context.Add(lineToAdd).Entity;
           _context.SaveChanges();
           return new Models.LineItem(){
               OrderID = lineToAdd.OrderId1,
               Quantity = (int)lineToAdd.OrderProductQantity,
            //    Item = lineToAdd.OrderId1Navigation.Select()


           };
        }

        // public Models.Order AddOrder(Models.Order newOrder)
        // {
        //     Entity.Order orderToAdd = new Entity.Order(){
        //         OrderAccountId = newOrder.CustomerID,
        //         OrderInvenId = newOrder.LineItems.
        //     }
        // }

        public List<Models.Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
            Entity.Customer customerByName = new Entity.Customer(){

            };
        }
        List<Customer> FindCustomerByName()
        {
            throw new NotImplementedException();
        }

        public List<Inventory> GetAllInventories(int storeId)
        {
            throw new NotImplementedException();
        }

        public List<LineItem> GetAllLineItems(int input)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Inventory UpdateStoreInventory(Inventory newInventory)
        {
            throw new NotImplementedException();
        }
    }
}
