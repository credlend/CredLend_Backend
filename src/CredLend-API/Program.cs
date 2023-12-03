using System.Text;
using AutoMapper;
using Domain.Core.Data;
using Domain.Models.Identity;
using Domain.Models.OperationsModel;
using Domain.Models.PlanModel;
using Domain.Models.RoleModel;
using Domain.Models.UserModel;
using Infrastructure.Data;
using Infrastructure.Mapping;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CredLend-API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                        "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                        "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

builder.Services.AddDbContext<ApplicationDataContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultContext")));

builder.Services.AddScoped<IOperationsLoanPlanRepository, OperationsLoanPlanRepository>();
builder.Services.AddScoped<IOperationsInvestmentPlanRepository, OperationsInvestmentPlanRepository>();
builder.Services.AddScoped<ILoanPlanRepository, LoanPlanRepository>();
builder.Services.AddScoped<IInventmentPlanRepository, InvestmentPlanRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IUserRepository<>), typeof(UserRepository<>));
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddAutoMapper(typeof(ApplicationDataContext));

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfiles());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddIdentityCore<User>(options =>
{
    //options.SignIn.RequireConfirmedEmail = true;

    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 4;

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
})
.AddRoles<Role>()
.AddEntityFrameworkStores<ApplicationDataContext>()
.AddRoleValidator<RoleValidator<Role>>()
.AddRoleManager<RoleManager<Role>>()
.AddSignInManager<SignInManager<User>>()
.AddDefaultTokenProviders();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false

        };
    });

builder.Services.AddMvc(options =>
    {
        var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    }
);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
           .WithOrigins("http://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
