using System;
using System.Collections.Generic;

namespace SalesTax
{
    public class SalesTaxApplication
    {
        public SalesTaxApplication()
        {
        	Console.WriteLine("*******************************************************************************");
        	Console.WriteLine("                          SALES TAX APPLICATION");
        	Console.WriteLine("*******************************************************************************");
        }

		public List<string> GetShoppingCartFromUser()
        {
        	Console.WriteLine(Constants.InitialMessage);
        	List<string> ShoppingCart = new List<string>();
			while(true)
			{
				string cartItem= Console.ReadLine();
				if(cartItem == Constants.quit || cartItem == Constants.Quit)
					break;
				ShoppingCart.Add(cartItem);
			}
			return ShoppingCart;
        }

        public Result<Output> CalculateTaxesAndTotalCost(List<string> ShoppingCart)
        {
    		var productResponse = GetProductDetails(ShoppingCart);
    		if(productResponse.message == Constants.Error)
    		{
    			return new Result<Output>()
    			{
    				message = Constants.Error
    			};
    		}
			
    		Result<Output> result = new Result<Output>();
    		Output output = new Output();
    		List<Product> items= new List<Product>();
    		double salesTaxes = 0, resultTotal =0;
			foreach(var item in productResponse.Data)
			{
    			double total = item.Price;
		        double currentItemTax = 0;
		        if(item.BasicTaxApplied == true && item.AdditionalTaxApplied == true)
		        {
		            currentItemTax = item.Price * 0.15;
		            currentItemTax = Math.Ceiling(currentItemTax*20)/20;
		        }
		        else if(item.BasicTaxApplied == true)
		        {
		            currentItemTax = item.Price * 0.1;
		            currentItemTax = Math.Ceiling(currentItemTax*20)/20;
		        }
		        else if(item.AdditionalTaxApplied == true)
		        {
		            currentItemTax = item.Price * 0.05;
		            currentItemTax = Math.Ceiling(currentItemTax*20)/20;
		        }
		        salesTaxes = salesTaxes + currentItemTax;
		        item.TotalPrice = Math.Round((item.Price + currentItemTax) * item.Quantity,2);
		        resultTotal = resultTotal + item.TotalPrice;
			}
			items = productResponse.Data;
			output.Items = items;
			output.SalesTax = Math.Round(salesTaxes,2);
			output.TotalPrice = Math.Round(resultTotal,2);
			return new Result<Output>()
			{
				message = Constants.Success,
				Data = output
			};
        }

        private Result<List<Product>> GetProductDetails(List<string> ShoppingCart)
        {
        	List<Product> Products = new List<Product>();
    		foreach(var item in ShoppingCart)
    		{
        		string[] item1 = item.Split(' ');
        		Product product = new Product();
        		try
        		{
        			product.Quantity = Convert.ToInt32(item1[0]);
        			product.Price = Convert.ToDouble(item1[item1.Length-1]);
        		}
        		catch(FormatException)
        		{
        			return new Result<List<Product>>()
        			{
        				message = Constants.Error
        			};
        		}
        		if(Array.IndexOf(item1,Constants.Imported)>=0)
        			product.AdditionalTaxApplied = true;
        		else
        			product.AdditionalTaxApplied = false;
        		product.BasicTaxApplied = true;
        		foreach(var i in Constants.FoodItems)
        		{
            		if(Array.IndexOf(item1,i)>=0)
            		{
                		product.BasicTaxApplied = false;
                		break;
            		}
        		}
        		string description=""; 
        		for(var i=1;i<item1.Length-2;i++)
        		{
        			if(item1[i]!=Constants.Imported)
        			{
        				description+=item1[i];
        				if(i<item1.Length-3)
        					description+=" ";
        			}
        		}
        		if(product.AdditionalTaxApplied == true)
        			product.ProductDescription = string.Format("{0} {1} {2}",product.Quantity,Constants.Imported,description);
        		else
        			product.ProductDescription = string.Format("{0} {1}",product.Quantity,description);

        		Products.Add(product);
    		}
    		return new Result<List<Product>>(){ 
    			message = Constants.Success,
    			Data = Products
    		};
        }
    }
}