using Microsoft.AspNetCore.Mvc;

var locais = new Dictionary<string, Local>();
locais.Add("GRU", Local.Create("GRU"));
locais.Add("BRC", Local.Create("BRC"));
locais.Add("ORL", Local.Create("ORL"));
locais.Add("SCL", Local.Create("SCL"));
locais.Add("CDG", Local.Create("CDG"));
/*
Origem: GRU, Destino: BRC, Valor: 10
Origem: BRC, Destino: SCL, Valor: 5
Origem: GRU, Destino: CDG, Valor: 75
Origem: GRU, Destino: SCL, Valor: 20
Origem: GRU, Destino: ORL, Valor: 56
Origem: ORL, Destino: CDG, Valor: 5
Origem: SCL, Destino: ORL, Valor: 20
*/
var rotas = new List<Rota>{
    Rota.Create(locais["GRU"], locais["BRC"], 10),
    Rota.Create(locais["BRC"], locais["SCL"], 5),
    Rota.Create(locais["GRU"], locais["CDG"], 75),
    Rota.Create(locais["GRU"], locais["SCL"], 20),
    Rota.Create(locais["GRU"], locais["ORL"], 56),
    Rota.Create(locais["ORL"], locais["CDG"], 5),
    Rota.Create(locais["SCL"], locais["ORL"], 20),
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICalculoRotasService>(x => new CalculoRotasService(rotas));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/rotaeconomica/{origem}-{destino}", async ([FromServices] ICalculoRotasService _calc, [FromRoute] string origem, [FromRoute] string destino)=>
{
    return await _calc.GetRouteWithLowerPrice(Local.Create(origem), Local.Create(destino));
})
.WithName("RotaEconomica")
.WithOpenApi();

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

app.MapPost("/rotas", ([FromBody] Rota rota) =>
{
    if(!rotas.Any(x=> x.Origem == rota.Origem && x.Destino == rota.Destino))    
    {
        rotas.Add(rota);
    }
})
.WithName("Rotas")
.WithOpenApi();

app.MapDelete("/rotas", ([FromBody] Rota rota) =>
{
    var _rota = rotas.FirstOrDefault(x=> x.Origem == rota.Origem && x.Destino == rota.Destino);
    if(_rota is not null)    
    {
        rotas.Remove(_rota);
    }
})
.WithName("Rotas")
.WithOpenApi();

app.MapPatch("/rotas", ([FromBody] Rota rota) =>
{
    var _rota = rotas.FirstOrDefault(x=> x.Origem == rota.Origem && x.Destino == rota.Destino);
    if(_rota is not null)    
    {
         rotas.Remove(_rota);
         
         rotas.Add(rota);
    }
})
.WithName("Rotas")
.WithOpenApi();

app.Run();


