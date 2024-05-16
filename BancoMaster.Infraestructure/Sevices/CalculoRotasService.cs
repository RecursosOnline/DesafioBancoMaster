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
public interface ICalculoRotasService
{
    Task<List<Rota>> GetRouteWithLowerPrice(Local origem, Local destino);
}
public class CalculoRotasService : ICalculoRotasService
{
    private readonly List<Rota> _rotas;

    public CalculoRotasService(List<Rota> rotas) => _rotas = rotas;

    public async Task<List<Rota>> GetRouteWithLowerPrice(Local origem, Local destino)
    {
        var todasRotasPossiveis = CalcularTodasRotas(origem, destino);
        var menorPreco = todasRotasPossiveis.Min(rota => rota.Sum(r => r.Valor));

        return await Task.FromResult(todasRotasPossiveis.FirstOrDefault(rota => rota.Sum(r => r.Valor) == menorPreco)!);
    }

    private List<List<Rota>> CalcularTodasRotas(Local origem, Local destino)
    {
        var rotasPossiveis = new List<List<Rota>>();
        var rotasParciais = new Stack<Rota>();
        var visitados = new HashSet<Local>();

        Visit(origem, destino, rotasPossiveis, rotasParciais, visitados);

        return rotasPossiveis;
    }

    private void Visit(Local origem, Local destino, List<List<Rota>> rotasPossiveis, Stack<Rota> rotasParciais, HashSet<Local> visitados)
    {
        if (origem == destino)
        {
            rotasPossiveis.Add(rotasParciais.ToList());
            return;
        }

        visitados.Add(origem);

        foreach (var rota in _rotas.Where(r => r.Origem == origem && !visitados.Contains(r.Destino)))
        {
            rotasParciais.Push(rota);
            Visit(rota.Destino, destino, rotasPossiveis, rotasParciais, visitados);
            rotasParciais.Pop();
        }

        visitados.Remove(origem);
    }
}

public record Local(string Nome)
{
    public static Local Create(string Nome)
    {
        return new Local(Nome); 
    }
};
public record Rota(Local Origem, Local Destino, decimal Valor)
{
    public static Rota Create(Local Origem, Local Destino, decimal Valor)
    {
        return new Rota(Origem, Destino, Valor);
    }
}