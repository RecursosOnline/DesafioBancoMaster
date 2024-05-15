namespace BancoMaster.TestesUnitarios;

public class CalculoRotasTest
{
    [Fact]
    public void QuandoFornecerRotasValidas_RetornarSucesso()
    {
        var locais = new Dictionary<string, Local>();
        locais.Add("GRU", Local.Create("GRU"));
        locais.Add("BRC", Local.Create("BRC"));
        locais.Add("ORL", Local.Create("ORL"));
        locais.Add("SCL", Local.Create("SCL"));
        locais.Add("CDG", Local.Create("CDG"));

        var rotas = new List<Rota>{
            Rota.Create(locais["GRU"], locais["BRC"], 10),
            Rota.Create(locais["BRC"], locais["SCL"], 5),
            Rota.Create(locais["GRU"], locais["CDG"], 75),
            Rota.Create(locais["GRU"], locais["SCL"], 20),
            Rota.Create(locais["GRU"], locais["ORL"], 56),
            Rota.Create(locais["ORL"], locais["CDG"], 5),
            Rota.Create(locais["SCL"], locais["ORL"], 20),
        };
        Assert.NotNull(locais["GRU"]);
    }
}
/*
## Explicando ## 
Uma viajem de **GRU** para **CDG** existem as seguintes rotas:

1. GRU - BRC - SCL - ORL - CDG ao custo de $40
2. GRU - ORL - CDG ao custo de $61
3. GRU - CDG ao custo de $75
4. GRU - SCL - ORL - CDG ao custo de $45

O melhor preÃ§o Ã© da rota **1**, apesar de mais conexÃµes, seu valor final Ã© menor.
O resultado da consulta deve ser: **GRU - BRC - SCL - ORL - CDG ao custo de $40**.

Sendo assim, o endpoint de consulta deverÃ¡ efetuar o calculo de melhor rota.
*/
public class CalculoRotas
{
    private readonly List<Rota> _rotas;
    private List<List<Rota>> _rotasCalculadas;
    public CalculoRotas(List<Rota> rotas) => _rotas = rotas;
    public Task<List<Rota>> GetRouteWithLowerPrice(Local Origem, Local Destino)
    {
        var rotasParaDestino = _rotas
                    .Where(r => r.Origem == Origem)
                    .ToList();
         
        return Task.FromResult(_rotasCalculadas);
    }
    private IEnumerable<Rota> GetRoutes(Rota rota, Local Destino)
    {
        if(rota.Destino == Destino) yield return rota;
         var rotasSelecionadas = _rotas
                                .Where(r => r.Origem == rota.Origem)
                                .ToList();
        foreach (var _rota in rotasSelecionadas)
        {
            //yield GetRoutes(_rota, Destino);
        } 
    }
}
public record Local(string Nome)
{
    internal static Local Create(string Nome)
    {
        return new Local(Nome); 
    }
};
public record Rota(Local Origem, Local Destino, decimal Valor)
{
    internal static Rota Create(Local Origem, Local Destino, decimal Valor)
    {
        return new Rota(Origem, Destino, Valor);
    }
}