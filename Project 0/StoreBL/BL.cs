using System;
using System.Collections.Generic;
using DL;
using Models;

namespace StoreBL
{
    public class BL : IBL
    {
        private IRepo _repo;
        public BL(IRepo repo)
        {
            _repo = repo;
        }

        public Customer AddCustomer(Customer newCustomer)
        {
            return _repo.AddCustomer(newCustomer);
        }

        // public LineItem AddLineItem(LineItem newItem)
        // {
        //     return _repo.AddLineItem(newItem);
        // }

        // public Order AddOrder(Order newOrder)
        // {
        //     return _repo.AddOrder(newOrder);
        // }

        // public List<Customer> GetAllCustomers()
        // {
        //     return _repo.GetAllCustomers();
        // }
        public List<Customer> FindOneCustomer(string qryString)
        {
            return _repo.FindOneCustomer(qryString);
        }
        public List<Inventory> GetInventoryByStoreID(Customer newCustomer)
        {
            return _repo.GetInventoryByStoreID(newCustomer);
        }
        public List<StoreFront> GetStoreFronts()
        {
            return _repo.GetStoreFronts();
        }
        // public List<Inventory> GetAllInventories(int storeId)
        // {
        //     return _repo.GetAllInventories(storeId);
        // }

        // public List<LineItem> GetAllLineItems(int input)
        // {
        //     return _repo.GetAllLineItems(input);
        // }

        // public List<Order> GetAllOrders()
        // {
        //     return _repo.GetAllOrders();
        // }

        // public Inventory UpdateStoreInventory(Inventory newInventory)
        // {
        //     return _repo.UpdateStoreInventory(newInventory);
        //}
    }
}
