using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace TinyBrowser {
    class Program {


        static List<int> GetIndexList(String str, String s) {
            var list = new List<int>();
            bool flag = false;
            for (int i = 0; i < str.Length - s.Length + 1; i++) {
                if (str.Substring(i,
                        s.Length)
                    .Equals(s)) {
                    Console.Write(i + " ");
                    list.Add(i);
                    flag = true;
                }
            }

            if (flag == false) {
                Console.WriteLine("NONE");
            }

            return list;
        }

        static void Main(string[] args) {
            List<int> indices = new List<int>();
            TcpClient client = new TcpClient();
            byte[] bytes = new byte[11000];
            string html = "";
            while (true) {
                client.Connect("www.acme.com", 80);
                if (client.Connected) {
                    var stream = client.GetStream();
                    var buffer = Encoding.ASCII.GetBytes("GET / HTTP/1.1" + Environment.NewLine + "Host: acme.com" +
                                                         Environment.NewLine + Environment.NewLine);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Read(bytes, 0, bytes.Length);
                    html = Encoding.ASCII.GetString(bytes);
                    stream.Flush();
                    stream.Close();
                    client.Close();
                }

                break;
            }

            var from = html.IndexOf("<title>") + "<title>".Length;
            var to = html.LastIndexOf("</title>");
            var result = html.Substring(from, to - from);
            Console.WriteLine(html);
           var list =  GetIndexList(html, "<a href=\"");
           var list2 = GetIndexList(html, "</a>");
       

            Console.WriteLine(result);
            Console.WriteLine(list.Count);
            Console.WriteLine(list2.Count);
        }
    }
}