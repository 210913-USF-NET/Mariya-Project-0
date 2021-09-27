using Models;
using System;
using System.Collections.Generic;


namespace DL
{
    public interface IRepo
    {
        // List<Customer> GetAllCustomers();
        List<Customer> FindOneCustomer(string qryString);
        Customer AddCustomer(Customer newCustomer);
        List<Product> ProductsList();
         List<Inventory> GetInventoryByStoreID(Customer newCustomer);
        List<StoreFront> GetStoreFronts();
        StoreFront GetMyStore(Customer cust);
        // Inventory UpdateStoreInventory(Inventory newInventory);
        // LineItem AddLineItem(LineItem newItem);
        // List<LineItem> GetAllLineItems(int input);

        // Order AddOrder(Order newOrder);
        // List<Order> GetAllOrders();

    }
}