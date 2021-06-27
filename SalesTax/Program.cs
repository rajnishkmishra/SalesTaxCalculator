using System;

namespace SalesTax
{
    class Program
    {
        static void Main(string[] args)
        {
            var salesTaxApplication = new SalesTaxApplication();
            var shoppingCart = salesTaxApplication.GetShoppingCartFromUser();
            if(shoppingCart.Count > 0)
            {
	            var result = salesTaxApplication.CalculateTaxesAndTotalCost(shoppingCart);
	            Console.WriteLine("****************************************");
	            if(result.message == Constants.Error)
	            {
	            	Console.WriteLine("ERROR OCCURED");
	            }
	            else
	            {
	            	foreach(var item in result.Data.Items)
	            	{
	            		Console.WriteLine(item);
	            	}
	            	Console.WriteLine(Constants.SalesTaxes+String.Format("{0:0.00}",result.Data.SalesTax));
					Console.WriteLine(Constants.Total+String.Format("{0:0.00}",result.Data.TotalPrice));
	            }
	        }
	        else
	        {
	        	Console.WriteLine(Constants.EmptyShoppingCart);
	        }
        }
    }
}
