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
        List<Inventory> GetInventoryByStoreID(Customer newCustomer);
        List<StoreFront> GetStoreFronts();
        // Inventory UpdateStoreInventory(Inventory newInventory);
        // LineItem AddLineItem(LineItem newItem);
        // List<LineItem> GetAllLineItems(int input);

        // Order AddOrder(Order newOrder);
        // List<Order> GetAllOrders();

    }
}