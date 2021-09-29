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
/// <summary>
/// Takes created model customer and converts to Entity customer then returns once customer has been added to DB
/// </summary>
/// <param name="newCustomer">model created from user input</param>
/// <returns>Return customer obj once it's been added to DB</returns>
        public Models.Customer AddCustomer(Models.Customer newCustomer)
        {
            Entity.Customer customerToAdd = new Entity.Customer(){
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
/// <summary>
/// Searches DB for any customers with name or username like input
/// </summary>
/// <param name="qryString">name string to get from user input in console</param>
/// <returns>List Of Customers with matching names</returns>
        public List<Models.Customer> FindOneCustomer(string qryString)
        {
            return _context.Customers.Where(u => u.CustomerUserName.ToLower().Trim().Contains(qryString.ToLower().Trim()) || u.CustomerName.ToLower().Trim().Contains(qryString.ToLower().Trim())).Select(
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
/// <summary>
/// Returns model store by inputting customer default store
/// </summary>
/// <param name="cust"> obj that is passed around while still logged in</param>
/// <returns>StoreFront Obj</returns>
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
/// <summary>
/// Returns list of Customers by using customer obj passed in getting list where cust store == inventory store
/// </summary>
/// <param name="newCustomer">bj that is passed around while still logged in</param>
/// <returns>List of inventory items by customer default store</returns>
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
        /// Takes a list of inventory created in UI and adds in a foreach loop
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
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
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
            
            _context.SaveChanges();
        }

        public List<Order> ListOfOrdersByCust(Customer cust)
        {
            return _context.Orders.Where(x => x.OrderAccountId == cust.CustomerId).Select(r => new Models.Order()
            {
                OrderId =r.OrderId,
                CustomerID = r.OrderAccountId,
                StoreID = r.OrderStoreId,
                Total = r.OrderTotal,
                Date = r.OrderDate
            }).ToList();
        }
        public List<LineItem> LineItemsList()
        {
            return _context.LineItems.Select(i => new Models.LineItem()
            {
                OrderID = i.LineOrderId,
                ProductID = i.LineInvenProdId,
                StoreId = i.LineStoreId,
                Quantity = (int)i.OrderProductQantity
            }).ToList();
        }

        public List<Order> ListOrder()
        {
            return _context.Orders.Select(r => new Models.Order()
            {
                OrderId =r.OrderId,
                CustomerID = r.OrderAccountId,
                StoreID = r.OrderStoreId,
                Total = r.OrderTotal,
                Date = r.OrderDate
            }).ToList();
        } 

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.Select(
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

    public Models.Customer CustomerStoreUpdate(Models.Customer cust)
        {
            
            
                
                Entities.Customer updatedCust = (from i in _context.Customers
                    where i.CustomerId == cust.CustomerId
                    select i).FirstOrDefault();
                updatedCust.CustomerStore = cust.CustomerDefaultStoreID;
            
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Models.Customer(){
                CustomerId = updatedCust.CustomerId,
                Name = updatedCust.CustomerName,
                Email = updatedCust.CustomerEmail,
                UserName = updatedCust.CustomerUserName,
                Password = updatedCust.CustomerPassWord,
                Address = updatedCust.CustomerAddress,
                CustomerDefaultStoreID= updatedCust.CustomerStore
            };
        }

        public List<Inventory> GetInventoryForAdmin(int input)
        {
            return  _context.Inventories.Where(y => y.InvenStoreId== input).Select(i => new Models.Inventory()
            {
                StoreID = i.InvenStoreId,
                Quantity = i.InventoryQuantity,
                ProductID = i.InvenProductId,
            }).ToList();
        }
    }
}