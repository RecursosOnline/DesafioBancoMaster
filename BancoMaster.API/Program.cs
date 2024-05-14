var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

/*
Origem: GRU, Destino: BRC, Valor: 10
Origem: BRC, Destino: SCL, Valor: 5
Origem: GRU, Destino: CDG, Valor: 75
Origem: GRU, Destino: SCL, Valor: 20
Origem: GRU, Destino: ORL, Valor: 56
Origem: ORL, Destino: CDG, Valor: 5
Origem: SCL, Destino: ORL, Valor: 20
*/

app.MapGet("/locais", () =>
{
    return locais.ToArray();
})
.WithName("Locais")
.WithOpenApi();

app.MapGet("/rotas", () =>
{
    return rotas.ToArray();
})
.WithName("Rotas")
.WithOpenApi();

app.Run();


record Local(string Nome)
{
    internal static Local Create(string Nome)
    {
        return new Local(Nome); 
    }
};
record Rota(Local Origem, Local Destino, decimal Valor)
{
    internal static Rota Create(Local Origem, Local Destino, decimal Valor)
    {
        return new Rota(Origem, Destino, Valor);
    }
}
