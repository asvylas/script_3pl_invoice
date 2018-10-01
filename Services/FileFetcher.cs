using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using _3pl_invoice.Models;
using _3pl_invoice.Repository;
using Newtonsoft.Json;

namespace _3pl_invoice.Services
{
    public class FileFetcher
    {
        public static void GatherData()
        {
            IEnumerable<Order> initialList = DataHandler.FetchPrintingList();
            IEnumerable<Order> completeList = DataHandler.FetchSerials(initialList);
            foreach (var item in completeList)
            {
                FetchFile(item);
            }
        }

        public static bool FetchFile(Order item)
        {
            JSONOrder jsonOrder = new JSONOrder()
            {
                serialno = item.serialID,
                partnumber = item.partID
            };
            byte[] fileData = null;
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonOrder);

            string itemOrder = (item.filename.Split(";"))[0];
            string endPoint = ""; //URL

            using(var client = new HttpClient())
            {
                var file = client.GetAsync(endPoint).Result;
                if(file.IsSuccessStatusCode)
                {
                    fileData = file.Content.ReadAsByteArrayAsync().Result;
                }
                FilePrinter.PrintPDF(fileData);
                Console.WriteLine(fileData.Length);
            }

            return true;
        } 
    }
}
