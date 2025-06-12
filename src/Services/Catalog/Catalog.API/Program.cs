var builder = WebApplication.CreateBuilder(args);

// Add Services here
builder.Services.AddCarter();
builder.Services.AddMediatR(confing => confing.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure Http requests pipeline

app.MapCarter();
app.Run();
