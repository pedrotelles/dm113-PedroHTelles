using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ClienteEstoque.ServicoEstoque;
namespace ClienteEstoque
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Press ENTER when the service has started");
            Console.ReadLine();
            ServicoEstoqueClient proxy = new ServicoEstoqueClient("NetTcpBinding_IServicoEstoque");
            //Adicionar Produto 11
            Console.WriteLine("Teste 1: Adicionar produto 11");
            Console.WriteLine();

            Produto Produto = new Produto
            {
                NumeroProduto = "13000",
                NomeProduto = "Produto 13",
                EstoqueProduto = 220,
                DescricaoProduto = "Esse é o produto 13"
            };

            if (proxy.IncluirProduto(Produto) == true)
            {
                Console.WriteLine("Adicionado com sucesso");
            }
            else
            {
                Console.WriteLine("Esse produto já foi adicionado");
            }

            Console.WriteLine();
            Console.WriteLine("Teste 2: Remover o produto 10");
            if (proxy.RemoverProduto("10000"))
            {
                Console.WriteLine("Produto 10 foi removido");
            }
            else
            {
                Console.WriteLine("Produto 10 já foi removido");
            }
            Console.WriteLine();
            Console.WriteLine("Teste 3: Listar todos os produtos");
            List<string> ps = proxy.ListarProdutos().ToList();
            foreach (string prod in ps)
            {
                Console.WriteLine("Produto: {0}", prod);
                Console.WriteLine();
            }

            Produto p = proxy.VerProduto("2000");
            Console.WriteLine("Teste 4: Verificar informações do Produto 2");

            Console.WriteLine("Número do produto 2: {0}", p.NumeroProduto);
            Console.WriteLine("Nome do produto 2: {0}", p.NomeProduto);
            Console.WriteLine("Estoque do produto 2: {0}", p.EstoqueProduto);
            Console.WriteLine("Descricao do produto 2: {0}", p.DescricaoProduto);
            Console.WriteLine();

            Console.WriteLine("Teste 5: Adicionar 10 itens no estoque do produto 2");
            if (proxy.AdicionarEstoque("2000", 10))
            {
                Console.WriteLine("Adicionado 10 itens no estoque do Produto 2");
            }
            Console.WriteLine();
            Console.WriteLine("Teste 6: Estoque atual do produto 2:");
            Console.WriteLine("Estoque atual: " + proxy.ConsultarEstoque("2000").ToString());
            Console.WriteLine();


            Console.WriteLine("Teste 7: Estoque atual do produto 1:");
            Console.WriteLine("Estoque atual: " + proxy.ConsultarEstoque("1000").ToString());
            Console.WriteLine();

            Console.WriteLine("Teste 8: Remover 20 unidades do produto 1:");
            if (proxy.RemoverEstoque("1000", 10))
            {
                Console.WriteLine("Removido 10 itens no estoque do Produto 1");
            }
            Console.WriteLine();

            Console.WriteLine("Teste 9: Estoque atual do produto 1:");
            Console.WriteLine("Estoque atual: " + proxy.ConsultarEstoque("1000").ToString());
            Console.WriteLine();

            Produto p2 = proxy.VerProduto("1000");
            Console.WriteLine("Teste 10: Verificar informações do Produto 1");
            Console.WriteLine("Número do produto 1: {0}", p2.NumeroProduto);
            Console.WriteLine("Nome do produto 1: {0}", p2.NomeProduto);
            Console.WriteLine("Estoque do produto 1: {0}", p2.EstoqueProduto);
            Console.WriteLine("Descricao do produto 1: {0}", p2.DescricaoProduto);

            proxy.Close();
            Console.WriteLine("Press ENTER to finish");
            Console.ReadLine();

        }
    }
}
