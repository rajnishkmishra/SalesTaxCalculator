using System;
using System.Collections.Generic;

namespace SalesTax
{
	public class BasicTax : Tax
	{
		public static string[] ExtemptedItems = {"chocolate", "chocolates", "book", "pills"};

		public BasicTax() : base(true, 10) { }

		public void BasicTaxApplied(string[] splittedStrings)
		{
			foreach(var item in ExtemptedItems)
    		{
        		if(Array.IndexOf(splittedStrings, item) >= 0)
        			this.TaxApplied = false;
    		}
		}
	} 
}