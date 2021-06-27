using System;
using System.Collections.Generic;

namespace SalesTax
{
	public class ImportTax : Tax
	{
		public const string Imported = "imported";

		public ImportTax() : base(false, 5) { }

		public void ImportTaxApplied(string[] splittedStrings)
		{
			if(Array.IndexOf(splittedStrings, Imported) >= 0)
				this.TaxApplied = true;
		}
	} 
}