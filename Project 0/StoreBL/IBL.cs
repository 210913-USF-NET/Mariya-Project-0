using System.Collections.Generic;
using Models;

namespace StoreBL
{
    public interface IBL
    {
        List<Customer> GetAllCustomers();
        Customer AddCustomer(Customer newCustomer);
        List<Inventory> GetAllInventories(int storeId);
        Inventory UpdateStoreInventory(Inventory newInventory);
        LineItem AddLineItem(LineItem newItem);
        List<LineItem> GetAllLineItems(int input);
        Order AddOrder(Order newOrder);
        List<Order> GetAllOrders();

    }
}