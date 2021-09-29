using Models;
using System;
using System.Collections.Generic;


namespace DL
{
    public interface IRepo
    {
        List<Customer> GetAllCustomers();
        List<Customer> FindOneCustomer(string qryString);
        Customer AddCustomer(Customer newCustomer);
        List<Product> ProductsList();
        List<Inventory> GetInventoryByStoreID(Customer newCustomer);
        List<Inventory> GetInventoryForAdmin(int input);
        List<StoreFront> GetStoreFronts();
        StoreFront GetMyStore(Customer cust);
        Product GetProduct(int input);
        Order AddNewOrder(Order newOrd);
        void InventorToUpdate(List<Models.Inventory> items);
        void AddLineItems(List<LineItem> items);
        List<Order> ListOfOrdersByCust(Customer cust);
        List<Order> ListOrder();
        List<LineItem> LineItemsList();
        Models.Customer CustomerStoreUpdate(Models.Customer cust);

    }
}