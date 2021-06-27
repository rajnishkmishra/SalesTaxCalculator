using System;
using System.Collections.Generic;

namespace SalesTax
{
	public class Tax 
	{
		public bool TaxApplied;

		public double TaxPercentage;

		public double TaxAmount;

		public Tax(bool TaxApplied, double TaxPercentage)
		{
			this.TaxApplied = TaxApplied;
			this.TaxPercentage = TaxPercentage;
			this.TaxAmount = 0;
		}

		public void CalculateTax(double ShelfPrice)
		{
			if(this.TaxApplied == true)
			{
				this.TaxAmount = ShelfPrice * TaxPercentage / 100;
				this.TaxAmount = Math.Ceiling(this.TaxAmount * 20) / 20;
			}
		}
	} 
}