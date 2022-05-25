using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthSample.Data;
using System.Security.Claims;
using AuthSample.Core;
using Microsoft.AspNetCore.Authorization;
{
    var builder = WebApplication.CreateBuilder(args);
    
    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("PremiumContentPolicy",
            policy => policy.RequireClaim("PremiumUser"));
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ExpressShippingPolicy",
            policy => policy.RequireClaim(ClaimTypes.Country, "US", "UK", "IN"));
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Over14", policy =>
            policy.Requirements.Add(new MinimumAgeRequirement(14)));
    });
    builder.Services.AddSingleton<IAuthorizationHandler,
        MinimumAgeAuthorizationHandler>();

    builder.Services.AddSingleton<IAuthorizationPolicyProvider,
        MinimumAgePolicyProvider>();

    var app = builder.Build();
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        await SetupRoles(services);
        await SetupUsers(services);
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    app.Run();
}
async Task SetupRoles(IServiceProvider serviceProvider)
{
    var rolemanager = serviceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "Support", "User" };
    foreach (var role in roles)
    {
        var roleExist = await rolemanager.RoleExistsAsync(role);
        if (!roleExist)
        {
            await rolemanager.CreateAsync(new IdentityRole(role));
        }
    }
}
async Task SetupUsers(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider
        .GetRequiredService<UserManager<IdentityUser>>();
    var adminUser = await userManager.FindByEmailAsync("admin@abc.com");
    if (adminUser == null)
    {
        var newAdminUser = new IdentityUser
        {
            UserName = "admin@abc.com",
            Email = "admin@abc.com",
        };
        var result = await userManager
            .CreateAsync(newAdminUser, "Password@123");
        if (result.Succeeded)
            await userManager.AddToRoleAsync(newAdminUser, "Admin");
    }
    var supportUser = await userManager
        .FindByEmailAsync("support@abc.com");
    if (supportUser == null)
    {
        var newSupportUser = new IdentityUser
        {
            UserName = "support@abc.com",
            Email = "support@abc.com",
        };
        var result = await userManager
            .CreateAsync(newSupportUser, "Password@123");
        if (result.Succeeded)
            await userManager.AddToRoleAsync(newSupportUser, "Support");
    }
    var user = await userManager.FindByEmailAsync("user@abc.com");
    if (user == null)
    {
        var newUser = new IdentityUser
        {
            UserName = "user@abc.com",
            Email = "user@abc.com",
        };
        var result = await userManager.CreateAsync(newUser, "Password@123");
        if (result.Succeeded)
        {
            await userManager
                .AddToRoleAsync(newUser, "User");
            await userManager
                .AddClaimAsync(newUser, new Claim("PremiumUser", "true"));
            await userManager
                .AddClaimAsync(newUser, new Claim(ClaimTypes.Country, "US"));
            await userManager
                            .AddClaimAsync(newUser, new Claim(ClaimTypes.DateOfBirth, "1-5-2003"));
        }
    }
}


