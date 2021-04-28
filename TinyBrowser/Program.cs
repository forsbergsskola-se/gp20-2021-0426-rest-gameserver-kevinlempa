using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;
using System.Text;

namespace TinyBrowser {
    class Program {
        static void Main(string[] args) {
            TcpClient client = new TcpClient();
            byte[] bytes = new byte[11000];
            string html;
            while (true) {
                client.Connect("www.acme.com", 80);
                if (client.Connected) {
                    var stream = client.GetStream();
                    var buffer = Encoding.ASCII.GetBytes("GET / HTTP/1.1"+Environment.NewLine +"Host: acme.com"+Environment.NewLine+Environment.NewLine);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Read(bytes, 0, bytes.Length);
                    html = Encoding.ASCII.GetString(bytes);
                    Console.WriteLine(html);
                    stream.Flush();
                    stream.Close();
                    client.Close();
                }
                break;
            }
            
            
        }
    }
}