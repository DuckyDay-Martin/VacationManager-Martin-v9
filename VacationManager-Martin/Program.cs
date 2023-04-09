using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VacationManager_Martin.Data;
using VacationManager_Martin.Data.DataSeeder;
using VacationManager_Martin.Data.Entities;
using VacationManager_Martin.Global;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//!

//!
//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddUserManager<UserManager<User>>()
    .AddSignInManager<SignInManager<User>>()
    .AddRoleManager<RoleManager<IdentityRole>>();

builder.Services.AddControllersWithViews();
    

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    bool roleExists = await roleManager.RoleExistsAsync(RoleConstants.Roles.CEO);
    if (!roleExists)
    {
        IdentityResult result = await roleManager.CreateAsync(new IdentityRole(RoleConstants.Roles.CEO));
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
        }
    }
    roleExists = await roleManager.RoleExistsAsync(RoleConstants.Roles.TeamLead);
    if (!roleExists)
    {
        IdentityResult result = await roleManager.CreateAsync(new IdentityRole(RoleConstants.Roles.TeamLead));
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
        }
    }
    roleExists = await roleManager.RoleExistsAsync(RoleConstants.Roles.Developer);
    if (!roleExists)
    {
        IdentityResult result = await roleManager.CreateAsync(new IdentityRole(RoleConstants.Roles.Developer));
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
        }
    }

    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    //string id = "1a";
    string userName = "Pesho.G";
    string firstName = "Pesho";
    string lastName = "Goshev";
    string password = "Test1,";
    //int? teamId = 1;   

    if (await userManager.FindByNameAsync(userName) == null)
    {
        var user = new User();
       // user.Id = id;
        user.UserName = userName;
        user.FirstName = firstName;
        user.LastName = lastName;
       // user.Password = password;
        //user.TeamId = teamId;

        await userManager.CreateAsync(user, password);
        await userManager.AddPasswordAsync(user, password);

        await userManager.AddToRoleAsync(user, "CEO");
    }

}

using (var scope = app.Services.CreateScope())
{

    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    //string id = "1a";
    string userName = "Mariq.V";
    string firstName = "Mariq";
    string lastName = "Veleva";
    string password = "Test1,";
    //int? teamId = 1;   

    if (await userManager.FindByNameAsync(userName) == null)
    {
        var user = new User();
        // user.Id = id;
        user.UserName = userName;
        user.FirstName = firstName;
        user.LastName = lastName;
        // user.Password = password;
        //user.TeamId = teamId;

        await userManager.CreateAsync(user, password);
        await userManager.AddPasswordAsync(user, password);

        await userManager.AddToRoleAsync(user, RoleConstants.Roles.CEO);
    }

}



app.Run();
