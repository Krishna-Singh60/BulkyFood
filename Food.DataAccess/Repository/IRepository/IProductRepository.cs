﻿using Bookie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.DataAccess.Repository.IRepository
{
    //Created Interface for Category
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
    }
}
