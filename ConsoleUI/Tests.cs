using System;
using LogicFibonachi;
using LogicSet;
using System.Collections.Generic;
namespace ConsoleUI
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			IEnumerable<int> generator = Generator.Generate(5);
			foreach(int i in generator)
			{
			    Console.WriteLine(i );
			}
			Set<string> set1 = new Set<string>();
			set1.Add("asdf");
			set1.Add("tgv");
			set1.Add("af");
			set1.Add("qwetre");
			set1.Add("cvbnbvcx");
			Console.WriteLine(set1);
		}
	}
}
