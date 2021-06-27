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
            List<Product> Products = new List<Product>();
            foreach(var item in ShoppingCart)
            {
                Product product = new Product();
                var response = product.GetProductFromEachShoppingCartItem(item);
                if(response == false)
                {
                    return new Result<Output>()
                    {
                        message = Constants.Error
                    };
                }
                Products.Add(product);
            }
    		Output output = new Output();
            output.GetOutput(Products);
            return new Result<Output>()
            {
                message = Constants.Success,
                Data = output
            };
        }
    }
}