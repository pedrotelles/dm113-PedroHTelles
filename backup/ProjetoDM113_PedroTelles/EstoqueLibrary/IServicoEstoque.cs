using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

using System.Text;

namespace EstoqueLibrary
{
    [ServiceContract(Namespace = "http://projetoavaliativo.dm113/01", Name = "IServicoEstoque")]
    public interface IServicoEstoque
    {

        [OperationContract]
        List<String> ListarProdutos();

        [OperationContract]
        bool IncluirProduto(Produto Produto);

        [OperationContract]
        bool RemoverProduto(String NumeroProduto);

        [OperationContract]
        int ConsultarEstoque(String NumeroProduto);

        [OperationContract]
        bool AdicionarEstoque(String NumeroProduto, int Quantidade);

        [OperationContract]
        bool RemoverEstoque(String NumeroProduto, int Quantidade);

        [OperationContract]
        Produto VerProduto(String NumeroProduto);

    }

    [ServiceContract(Namespace = "http://projetoavaliativo.dm113/02", Name = "IServicoEstoque")]
    public interface IServicoEstoqueV2
    {

        [OperationContract]
        bool AdicionarEstoque(String NumeroProduto, int Quantidade);

        [OperationContract]
        bool RemoverEstoque(String NumeroProduto, int Quantidade);

        [OperationContract]
        int ConsultarEstoque(String NumeroProduto);



    }
    [DataContract]
    public class Produto
    {
        [DataMember]
        public string NumeroProduto;
        [DataMember]
        public string NomeProduto;
        [DataMember]
        public string DescricaoProduto;
        [DataMember]
        public int EstoqueProduto;
    }
}