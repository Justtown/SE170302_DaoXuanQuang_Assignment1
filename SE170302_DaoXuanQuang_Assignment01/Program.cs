using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.DataAccess;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Config OData EDM Model
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Category>("Categories");
modelBuilder.EntitySet<SystemAccount>("SystemAccounts");
modelBuilder.EntitySet<NewsArticle>("NewsArticles"); 
modelBuilder.EntitySet<Tag>("Tags");

// CHỈ GỌI GetEdmModel() 1 LẦN DUY NHẤT Ở ĐÂY
var edmModel = modelBuilder.GetEdmModel();

builder.Services.AddControllers()
    .AddOData(options => options
        .Select()
        .Filter()
        .OrderBy()
        .Expand()
        .Count()
        .SetMaxTop(100)
        .AddRouteComponents("odata", edmModel))  
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

// Register DbContext
builder.Services.AddDbContext<FunewsManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Register Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();
builder.Services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();