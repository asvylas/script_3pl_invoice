using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using _3pl_invoice.Models;
using System.Data.SqlTypes;
using System.Data;

namespace _3pl_invoice.Repository
{
    public class DataHandler
    {

        public static IEnumerable<Order> FetchPrintingList()
        {
            var list = new List<Order>();
            using (var conn = new SqlConnection(connectionString: StringRepository.connectionString))
            {
                using (var commmand = new SqlCommand(cmdText: StringRepository.printingListQuery, connection: conn))
                {
                    conn.Open();
                    var reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {
                        var order = new Order()
                        {
                            deliveryID = reader[0].ToString(),
                            filename = reader[1].ToString()
                        };
                        list.Add(order);
                    }
                }
            }
            return list;
        }

        public static IEnumerable<Order> FetchSerials(IEnumerable<Order> initialList)
        {
            foreach (var item in initialList)
            {
                using (var conn = new SqlConnection(connectionString: StringRepository.connectionString))
                {
                    using (var command = new SqlCommand(cmdText: StringRepository.serialListQuery , connection: conn))
                    {
                        command.Parameters.Add("@deliveryID", sqlDbType: SqlDbType.BigInt ).Value = item.deliveryID;
                        conn.Open();
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            item.partID = reader[0].ToString();
                            item.serialID = reader[1].ToString();
                        }
                    }
                }
            }

            return initialList;
        }
    }
}