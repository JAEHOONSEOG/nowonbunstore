﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraping.Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello world");
            foreach (string a in args)
            {
                Console.WriteLine(a);
            }
            Console.ReadKey();
        }
    }
}