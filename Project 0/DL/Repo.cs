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

        public StoreFront GetMyStore(Customer cust)
        {
            Entity.StoreFront myStore = _context.StoreFronts.FirstOrDefault(x => x.StoreId == cust.CustomerDefaultStoreID);
            return new Models.StoreFront()
            {
                StoreID = myStore.StoreId,
                Name = myStore.StoreName,
                Address = myStore.StoreAddress
            };
        }

        public List<Inventory> GetInventoryByStoreID(Customer newCustomer)
        {   
            return  _context.Inventories.Where(y => y.InvenStoreId== newCustomer.CustomerDefaultStoreID).Select(i => new Models.Inventory()
            {
                StoreID = i.InvenStoreId,
                Quantity = i.InventoryQuantity,
                ProductID = i.InvenProductId,
            }).ToList();
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

        public List<Product> ProductsList()
        {
            return _context.Products.Select(r => new Models.Product()
            {
                ProductId =r.ProductId,
                Name = r.ProductName,
                Price = r.ProductPrice,
                Genre = r.ProductGenere,
                Description = r.ProductDescription
            }).ToList();
        }

        public Product GetProduct(int input)
        {
            Entity.Product myProduct = _context.Products.FirstOrDefault(x => x.ProductId == input);
            return new Models.Product(){
                ProductId = myProduct.ProductId,
                Price = myProduct.ProductPrice,
                Name = myProduct.ProductName,
                Genre = myProduct.ProductGenere,
                Description = myProduct.ProductDescription
            };
        }

        public Order AddNewOrder(Order newOrd)
        {
            Entity.Order orderToAdd = new Entity.Order(){
                OrderAccountId = newOrd.CustomerID,
                OrderTotal = newOrd.Total,
                OrderStoreId = newOrd.StoreID,
            };
            orderToAdd = _context.Add(orderToAdd).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            // Entity.Order addedOrder = _context.Orders.LastOrDefault();
            return new Models.Order
            {
                OrderId = orderToAdd.OrderId,
                CustomerID = orderToAdd.OrderAccountId,
                Total = orderToAdd.OrderTotal,
                Date = orderToAdd.OrderDate
            };

        }
        /// <summary>
        /// Takes a list of inventory from my
        /// </summary>
        /// <param name="items"></param>
        public void InventorToUpdate(List<Models.Inventory> items)
        {
            foreach(Models.Inventory item in items)
            {
                
                Entities.Inventory updatedInventory = (from i in _context.Inventories
                    where i.InvenProductId == item.ProductID && i.InvenStoreId == item.StoreID 
                    select i).SingleOrDefault();

                updatedInventory.InventoryQuantity = item.Quantity;
            }
            // List<Entity.Inventory> InvenToUpdate = items.Select(i => Entity.Inventory(){
            //     InvenStoreId = i.StoreID,
            //     InvenProductId = i.ProductID,
            //     InventoryQuantity = i.Quantity}).ToList();
                
            // _context.Inventories.UpdateRange(InvenToUpdate);
            _context.SaveChanges();
            // _context.ChangeTracker.Clear();
        }
        public void AddLineItems(List<LineItem> items)
        {
            foreach(Models.LineItem i in items)
            {
                Entity.LineItem lineToAdd = new Entity.LineItem(){
                LineOrderId = i.OrderID,
                LineStoreId = i.StoreId,
                LineInvenProdId = i.ProductID,
                OrderProductQantity = i.Quantity
            };
            lineToAdd = _context.Add(lineToAdd).Entity;
            }
            // List<Entity.Inventory> InvenToUpdate = items.Select(i => Entity.Inventory(){
            //     InvenStoreId = i.StoreID,
            //     InvenProductId = i.ProductID,
            //     InventoryQuantity = i.Quantity}).ToList();
                
            // _context.Inventories.UpdateRange(InvenToUpdate);
            _context.SaveChanges();
            // List<Entity.LineItem> itemsToAdd = items.Select(i => new Entity.LineItem(){
            //     LineOrderId = i.OrderID,
            //     LineStoreId = i.StoreId,
            //     LineInvenProdId = i.ProductID,
            //     OrderProductQantity = i.Quantity}).ToList();
                
            // _context.LineItems.AddRange(itemsToAdd);
            // _context.SaveChanges();
            // _context.ChangeTracker.Clear();
        }

        public List<Order> ListOfOrdersByCust(Customer cust)
        {
            return _context.Orders.Where(x => x.OrderStoreId == cust.CustomerDefaultStoreID).Select(r => new Models.Order()
            {
                OrderId =r.OrderId,
                CustomerID = r.OrderAccountId,
                StoreID = r.OrderStoreId,
                Total = r.OrderTotal,
                Date = r.OrderDate
            }).ToList();
        }
    }

}