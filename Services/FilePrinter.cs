using System;
using System.Net;
using System.Net.Sockets;

namespace _3pl_invoice.Services
{
    public class FilePrinter
    {
        public static bool PrintPDF(byte[] file)
        {
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress connectionIP = IPAddress.Parse("10.10.66.244");
                IPEndPoint endPoint = new IPEndPoint(connectionIP, 9100);
                socket.Connect(endPoint);
                socket.Send(file);
                socket.Close();
                //File.WriteAllBytes(@"C:\Andrius\Supodadupafolder\"+ DateTime.UtcNow.ToFileTimeUtc() + ".pdf", file);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

