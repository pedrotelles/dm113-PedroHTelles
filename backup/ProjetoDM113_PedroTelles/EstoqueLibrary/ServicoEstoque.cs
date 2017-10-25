using EstoqueEntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

using System.Text;

namespace EstoqueLibrary
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServicoEstoque : IServicoEstoque
    {

        public bool AdicionarEstoque(string NumeroProduto, int Quantidade)
        {
            try
            {
                // Connect to the ProductsModel database
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    // Find the ProductID for the specified produc
                    string productID = (from p in database.ProdutoEstoques
                                        where String.Equals(p.NumeroProduto, NumeroProduto) == true
                                        select p.NumeroProduto).First();

                    // Find the Stock object that matches the parameters passed
                    // in to the operation
                    ProdutoEstoque stock = database.ProdutoEstoques.First(pi => pi.NumeroProduto == productID);
                    
                    stock.EstoqueProduto = stock.EstoqueProduto + Quantidade;
                    
                    
                    
                    // Save the change back to the database
                    database.SaveChanges();
                }
            }
            catch
            {
                // If an exception occurs, return false to indicate failure
                return false;
            }
            // Return true to indicate success
            return true;


        }

        public int ConsultarEstoque(string NumeroProduto)
        {
            int quantityTotal = 0;
            try
            {
                // Connect to the ProductsModel database
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    // Calculate the sum of all quantities for the specified product
                    quantityTotal = (from p in database.ProdutoEstoques
                                     where String.Compare(p.NumeroProduto, NumeroProduto) == 0
                                     select (int)p.EstoqueProduto).Sum();
                }
            }
            catch
            {
                // Ignore exceptions in this implementation
            }
            // Return the stock level
            return quantityTotal;
        }

        public bool IncluirProduto(Produto Produto)
        {

            try
            {
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    bool rlista = (from p in database.ProdutoEstoques where string.Equals(p.NomeProduto, Produto.NomeProduto) 
                                   && string.Equals(p.NumeroProduto, Produto.NumeroProduto) select p).ToList().Count == 0;
                    Console.WriteLine(Produto.NumeroProduto);
                    Console.WriteLine(Produto.NomeProduto);
                    Console.WriteLine(Produto.EstoqueProduto);
                    Console.WriteLine(Produto.DescricaoProduto);
                    if (rlista == true)
                    {

                        ProdutoEstoque prod = new ProdutoEstoque
                        {
                            DescricaoProduto = Produto.DescricaoProduto,
                            EstoqueProduto = Produto.EstoqueProduto,
                            NomeProduto = Produto.NomeProduto,
                            NumeroProduto = Produto.NumeroProduto
                        };
                        database.ProdutoEstoques.Add(prod);
                        database.SaveChanges();
                        return true;
                    }
                    else
                    {

                        return false;
                    }

                }

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors: ",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }

        }

        public List<string> ListarProdutos()
        {
            List<string> productsList = new List<string>();
            try
            {
                // Connect to the ProductsModel database
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    // Fetch the products in the database
                    List<ProdutoEstoque> products = (from product in database.ProdutoEstoques
                                                     select product).ToList();
                    foreach (ProdutoEstoque product in products)
                    {

                        productsList.Add(product.NomeProduto);
                    }
                }
            }
            catch
            {
                // Ignore exceptions in this implementation
            }
            // Return the list of products
            return productsList;
        }

        public bool RemoverEstoque(string NumeroProduto, int Quantidade)
        {
            try
            {
                // Connect to the ProductsModel database
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    // Find the ProductID for the specified produc
                    string productID = (from p in database.ProdutoEstoques
                                        where String.Compare(p.NumeroProduto, NumeroProduto) == 0
                                        select p.NumeroProduto).First();

                    // Find the Stock object that matches the parameters passed
                    // in to the operation
                    ProdutoEstoque stock = database.ProdutoEstoques.First(pi => pi.NumeroProduto == productID);

                    stock.EstoqueProduto = stock.EstoqueProduto -  Quantidade;

                    

                    // Save the change back to the database
                    database.SaveChanges();
                }
            }
            catch
            {
                // If an exception occurs, return false to indicate failure
                return false;
            }
            // Return true to indicate success
            return true;
        }

        public bool RemoverProduto(string NumeroProduto)
        {
            try
            {
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    ProdutoEstoque prod = database.ProdutoEstoques.Where(p => p.NumeroProduto == NumeroProduto).FirstOrDefault();

                    if (prod != null)
                    {

                        database.ProdutoEstoques.Remove(prod);
                        database.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
            catch
            {
                return false;
            }

        }

        public Produto VerProduto(string NumeroProduto)
        {
            Produto product = null;
            try
            {
                using (ProvedorEstoque database = new ProvedorEstoque())
                {
                    // Find the first product that matches the specified product code
                    ProdutoEstoque matchingProduct = database.ProdutoEstoques.First(p => String.Compare(p.NumeroProduto, NumeroProduto) == 0);
                    product = new Produto()
                    {
                        NomeProduto = matchingProduct.NomeProduto,
                        NumeroProduto = matchingProduct.NumeroProduto,
                        DescricaoProduto = matchingProduct.DescricaoProduto,
                        EstoqueProduto = matchingProduct.EstoqueProduto
                    };

                }
            }
            catch
            {
                ///////////////////
            }

            return product;
        }
    }
}