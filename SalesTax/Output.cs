using System;
using System.Collections.Generic;

namespace SalesTax
{
	public class Output
	{
		public List<string> Items;

		public double SalesTax;

		public double TotalPrice;

		public Output()
		{
			this.Items = new List<string>();
			this.SalesTax = 0;
			this.TotalPrice = 0;
		}

		public void GetOutput(List<Product> Products)
		{
			foreach(var item in Products)
			{
				string s = item.ProductDescription+": "+String.Format("{0:0.00}",item.TotalPrice);
				this.Items.Add(s);
	            this.SalesTax += item.BasicTax.TaxAmount + item.ImportTax.TaxAmount;
	            this.TotalPrice += item.TotalPrice;
			}
			this.SalesTax = Math.Round(SalesTax, 2);
			this.TotalPrice = Math.Round(TotalPrice, 2);
		}
	}
}