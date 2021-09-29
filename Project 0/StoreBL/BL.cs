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
        public Product GetProduct(int input)
        {
            return _repo.GetProduct(input);
        }
        public List<Customer> FindOneCustomer(string qryString)
        {
            return _repo.FindOneCustomer(qryString);
        }
        public  List<Inventory> GetInventoryByStoreID(Customer newCustomer)
        {
            return _repo.GetInventoryByStoreID(newCustomer);
        }

        public StoreFront GetMyStore(Customer cust)
        {
            return _repo.GetMyStore(cust);
        }

        public List<StoreFront> GetStoreFronts()
        {
            return _repo.GetStoreFronts();
        }
        List<Product> IBL.ProductsList()
        {
            return _repo.ProductsList();
        }

        public Order AddNewOrder(Order newOrd)
        {
            return _repo.AddNewOrder(newOrd);
        }

        public void AddLineItems(List<LineItem> items)
        {
            _repo.AddLineItems(items);
        }

        public void InventorToUpdate(List<Inventory> items)
        {
            _repo.InventorToUpdate(items);
        }

        public List<Order> ListOfOrdersByCust(Customer cust)
        {
            return _repo.ListOfOrdersByCust(cust);
        }

        public List<LineItem> LineItemsList()
        {
            return _repo.LineItemsList();
        }

        public List<Order> ListOrder()
        {
            return _repo.ListOrder();
        }

        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public Customer CustomerStoreUpdate(Customer cust)
        {
            return _repo.CustomerStoreUpdate(cust);
        }

        public List<Inventory> GetInventoryForAdmin(int input)
        {
            return _repo.GetInventoryForAdmin(input);
        }
    }
}
