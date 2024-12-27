using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BogdanCristinaLab7.Models
{
    public class ListProduct
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(ShopList))]
        public int ShopListId { get; set; }
        public int ProductId { get; set; }
    }
}
