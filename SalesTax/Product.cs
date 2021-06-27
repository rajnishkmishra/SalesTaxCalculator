using System;

using System.Text;

namespace SalesTax
{
    public class Product
    {
    	public int Quantity;
    	public string ProductDescription;
    	public double ShelfPrice;
    	public double TotalPrice;
    	public BasicTax BasicTax;
    	public ImportTax ImportTax;

    	public bool GetProductFromEachShoppingCartItem(string ShoppingCartItem)
    	{
    		string[] splittedString = ShoppingCartItem.Split(' ');
    		try
    		{
    			this.Quantity = Convert.ToInt32(splittedString[0]);
    			this.ShelfPrice = Convert.ToDouble(splittedString[splittedString.Length-1]);
    		}
    		catch(FormatException)
    		{
    			return false;
    		}
    		
    		this.BasicTax = GetBasicTax(splittedString);
    		this.ImportTax = GetImportTax(splittedString);;
    		this.ProductDescription = GetProductDescription(splittedString, this.ImportTax);
    		this.TotalPrice = GetTotalPrice(this.BasicTax, this.ImportTax);
    		return true;
    	}

    	private BasicTax GetBasicTax(string[] splittedString)
    	{
    		BasicTax basicTax = new BasicTax();
    		basicTax.BasicTaxApplied(splittedString);
    		basicTax.CalculateTax(this.ShelfPrice);
    		return basicTax;
    	}

    	private ImportTax GetImportTax(string[] splittedString)
    	{
    		ImportTax importTax = new ImportTax();
    		importTax.ImportTaxApplied(splittedString);
    		importTax.CalculateTax(this.ShelfPrice);
    		return importTax;
    	}

    	private string GetProductDescription(string[] splittedString, ImportTax importTax)
    	{
    		string description=""; 
    		for(var i=1;i<splittedString.Length-2;i++)
    		{
    			if(splittedString[i]!=ImportTax.Imported)
    			{
    				description+=splittedString[i];
    				if(i<splittedString.Length-3)
    					description+=" ";
    			}
    		}
    		if(importTax.TaxApplied == true)
    			return string.Format("{0} {1} {2}",this.Quantity,ImportTax.Imported,description);
    		else
    			return string.Format("{0} {1}",this.Quantity,description);

    	}

    	private double GetTotalPrice(BasicTax basicTax, ImportTax importTax)
    	{
    		return Math.Round(this.ShelfPrice+basicTax.TaxAmount+importTax.TaxAmount,2);
    	}
    }
}
