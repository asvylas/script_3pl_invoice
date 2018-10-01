using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _3pl_invoice.Models
{
    public class Order
    {
        public string deliveryID  {get;set;}
        public string filename {get;set;}

        public string partID {get;set;}
        public string serialID {get;set;}

        public override string ToString()
        {
            string toString = $"Delivery: {this.deliveryID}, Filename: {this.filename}, Partnumber: {this.partID}, Serial: {this.serialID}";
            return toString;
        }
    }

    public class JSONOrder 
    {
        public string serialno {get;set;}
        public string partnumber {get;set;}
    }
}