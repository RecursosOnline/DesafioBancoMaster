
namespace BancoMaster.TestesUnitarios;

public class CalculoRotasTest
{
    readonly Dictionary<string, Local> locais;
    readonly List<Rota> rotas;

    public CalculoRotasTest()
    {
        locais = new Dictionary<string, Local>();
        locais.Add("GRU", Local.Create("GRU"));
        locais.Add("BRC", Local.Create("BRC"));
        locais.Add("ORL", Local.Create("ORL"));
        locais.Add("SCL", Local.Create("SCL"));
        locais.Add("CDG", Local.Create("CDG"));

        rotas = new List<Rota>{
            Rota.Create(locais["GRU"], locais["BRC"], 10),
            Rota.Create(locais["BRC"], locais["SCL"], 5),
            Rota.Create(locais["GRU"], locais["CDG"], 75),
            Rota.Create(locais["GRU"], locais["SCL"], 20),
            Rota.Create(locais["GRU"], locais["ORL"], 56),
            Rota.Create(locais["ORL"], locais["CDG"], 5),
            Rota.Create(locais["SCL"], locais["ORL"], 20),            
        };        
    }
/*
 Consulte a rota: GRU-CGD
  Resposta: GRU - BRC - SCL - ORL - CDG ao custo de $40
  
  Consulte a rota: BRC-SCL
  Resposta: BRC - SCL ao custo de $5
*/
    [Theory]
    [InlineData("GRU","CDG", 40)]
    [InlineData("BRC","SCL", 5)]
    public async Task QuandoFornecerRotasValidas_RetornarSucesso(string origem, string destino, decimal valor)
    {

        var calculoRotas = new CalculoRotasService(rotas);
        var rotaMaisEconomica = await calculoRotas.GetRouteWithLowerPrice(locais[origem], locais[destino]);
        Assert.Equal(valor, rotaMaisEconomica.Sum(r => r.Valor));       

    }
}
//Eduardo Pires