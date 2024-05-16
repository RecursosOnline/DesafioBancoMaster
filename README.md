# Sobre
> [!NOTE]
> Estou sem meu computador no momento e estou utilizando o recurso do CodeSpace do github para desenvolver a partir de meu tablet.

> [!WARNING]
> Não consegui executar o projeto API pois esta dando erro no CodeSpace.

### Diagrama das possiveis rotas entre GRU e CDG

```mermaid
flowchart TD    
    A{ROTAS:GRU-CDG}
    A -->|GRU-BRC| B[R$ 10]
    A -->|GRU-CDG| C[R$ 75]
    A -->|GRU-SCL| D[R$ 20]
    A -->|GRU-ORL| E[R$ 56]
    B -->|BRC-SCL| F[R$ 5]
    F -->|SCL-ORL| G[R$ 20]
    G -->|ORL-CDG| H[R$ 5]
    H -->|TOTAL| HT[R$ 40]
    D -->|SCL-ORL| I[R$ 20]
    I -->|ORL-CDG| J[R$ 5]
    J -->|TOTAL| JT[R$ 45]
    E -->|ORL-CDG| K[R$ 5]
    K -->|TOTAL| KT[R$ 61]
```
Projeto de testes executado com sucesso.
```C#
    [Theory]
    [InlineData("GRU","CDG", 40)]
    [InlineData("BRC","SCL", 5)]
    public async Task QuandoFornecerRotasValidas_RetornarSucesso(string origem, string destino, decimal valor)
    {

        var calculoRotas = new CalculoRotasService(rotas);
        var rotaMaisEconomica = await calculoRotas.GetRouteWithLowerPrice(locais[origem], locais[destino]);
        Assert.Equal(valor, rotaMaisEconomica.Sum(r => r.Valor));       

    }
```

## Instruções para execução

### Executar o projeto de teste
```
dotnet test
```

### Executar o projeto API
```
dotnet run
```

