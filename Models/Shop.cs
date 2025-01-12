﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BogdanCristinaLab7.Models
{
    public class Shop
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string Address { get; set; }
        public string ShopDetails
        {
            get
            {
                return ShopName + " " + Address;
            }
        }
        [OneToMany]
        public List<ShopList> ShopLists { get; set; }
    }
}
