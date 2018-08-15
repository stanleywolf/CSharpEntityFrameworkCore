using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace P01.TestAutoMapping.Data.Models.Mapping
{
    public class StorageProfile:Profile
    {
        public StorageProfile()
        {
            CreateMap<Storage, StorageDTO>()
                .Include<Warehouse, WarehouseDTO>();
            CreateMap<Warehouse, WarehouseDTO>();
        }
    }
}
