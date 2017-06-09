using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:1234";
            var apiRestfulClient = new ApiRestfulClient(url);

            var result = apiRestfulClient.GetTime();
            Console.WriteLine(result);

            var p = new Model.Product {Id = 1, Name = "product1"};

            result = apiRestfulClient.AddProduct(p);
            Console.WriteLine(result);

            result=apiRestfulClient.GetProduct(1);
            Console.WriteLine(result);
        }
    }
}
