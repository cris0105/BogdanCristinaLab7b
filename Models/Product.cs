using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;   

namespace BogdanCristinaLab7.Models
{
    public class Product
    {
        internal int ID;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }
        [OneToMany]
        public List<ListProduct> ListProducts { get; set; }
    }
}
