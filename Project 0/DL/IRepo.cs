using Models;
using System;
using System.Collections.Generic;


namespace DL
{
    public interface IRepo
    {
         List<StoreFront> GetAllStoreFronts ();
    }
}