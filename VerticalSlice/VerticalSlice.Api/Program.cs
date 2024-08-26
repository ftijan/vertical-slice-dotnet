using VerticalSlice.Api.Features.Person;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register features/slices
builder.Services.AddPersonFeature();

// ---

var app = builder.Build();

// Use features/slices
app.UsePersonFeature();

// ---

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Register features/slices endpoints
app.MapPersonEndpoints();

// ---

app.Run();
