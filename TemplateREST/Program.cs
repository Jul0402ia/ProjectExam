using TemplateLib.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repository 
builder.Services.AddSingleton<RepositoryTemplate>(); //hele API’et bruger det samme repository-objekt, så listen ikke nulstilles mellem requests.

// CORS 
builder.Services.AddCors(options =>  //
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin() //tillader anmodninger fra alle domæner
                  .AllowAnyMethod()//tillader alle HTTP-metoder (GET, POST, PUT, DELETE osv.)
                  .AllowAnyHeader();//tillader alla HTTP-headers (f.eks. Content-Type, Authorization osv.)
        });
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS skal stå før Authorization
app.UseCors("AllowAll");

app.UseAuthorization();

//finder og ruter HTTP-anmodninger til de tilsvarende controller-metoder baseret på URL og HTTP-metode (GET, POST, PUT, DELETE osv.)
app.MapControllers();

app.Run();
// hehehe