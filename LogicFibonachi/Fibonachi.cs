using System;
using System.Collections.Generic;
namespace LogicFibonachi
{
	public static class Generator
	{
		/// <summary>
		/// Generate the specified quantity.
		/// </summary>
		/// <returns>The generate.</returns>
		/// <param name="n">Quantity.</param>
		public static IEnumerable<int> Generate(int n)
		{
			if (n < 1) throw new ArgumentException($"{nameof(n)} is invalid!");

			int prev = -1;
			int next = 1;
			int temp = 0;
			for (int i = 0; i < n; i++)
			{
				temp = next;
				yield return next += prev;
				prev = temp;
			}
		}
	}
}