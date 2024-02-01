WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
var localConfigFile = Path.Combine(localAppDataPath, "Flamenco", "flamenco-local.json");

builder.Configuration.AddJsonFile(localConfigFile, optional: false, reloadOnChange: true);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseHttpsRedirection();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
