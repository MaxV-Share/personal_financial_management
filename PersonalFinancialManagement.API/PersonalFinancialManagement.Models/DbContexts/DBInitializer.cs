using System.Transactions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PersonalFinancialManagement.Models.Entities.Identities;

namespace PersonalFinancialManagement.Models.DbContexts;

public class DbInitializer(
    ApplicationDbContext context,
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IConfiguration configuration)
{
    private readonly string? _adminRoleName = configuration.GetValue<string>("AdminRoleName");
    protected readonly IConfiguration _configuration = configuration;

    public async Task Seed()
    {
        using var transaction = new CommittableTransaction(new TransactionOptions
            { IsolationLevel = IsolationLevel.RepeatableRead, Timeout = TimeSpan.FromMinutes(1) });

        #region Role

        if (!roleManager.Roles.Any())
            if (_adminRoleName != null)
                await roleManager.CreateAsync(new Role
                {
                    Id = _adminRoleName,
                    Name = _adminRoleName,
                    NormalizedName = _adminRoleName.ToUpper()
                });

        #endregion Role

        #region User

        if (!userManager.Users.Any())
        {
            var result = await userManager.CreateAsync(new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                FullName = "Quản trị",
                Email = "admin@admin.com",
                LockoutEnabled = false
            }, "Admin@123");
            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync("admin");
                if (_adminRoleName != null && user != null)
                    await userManager.AddToRoleAsync(user, _adminRoleName);
            }
        }

        #endregion User

        #region Lang

        //if (!_context.Langs.Any())
        //{
        //    await _context.Langs.AddAsync(new Lang
        //    {
        //        Id = "vi",
        //        Name = "Tiếng việt",
        //        Order = 1,
        //        CreateBy = "Seed"
        //    });
        //    await _context.Langs.AddAsync(new Lang
        //    {
        //        Id = "en",
        //        Name = "English",
        //        Order = 2,
        //        CreateBy = "Seed"
        //    });
        //}

        //if (!_context.Functions.Any())
        //{
        //    //_context.Functions.AddRange(new List<Function>
        //    //{
        //    //    new Function {Id = "DASHBOARD", Name = "Thống kê", ParentId = null, SortOrder = 1,Url = "/dashboard",Icon="fa-dashboard" },

        //    //    new Function {Id = "CONTENT",Name = "Nội dung",ParentId = null,Url = "/contents",Icon="fa-table" },

        //    //    new Function {Id = "CONTENT_CATEGORY",Name = "Danh mục",ParentId ="CONTENT",Url = "/contents/categories"  },
        //    //    new Function {Id = "CONTENT_KNOWLEDGEBASE",Name = "Bài viết",ParentId = "CONTENT",SortOrder = 2,Url = "/contents/knowledge-bases",Icon="fa-edit" },
        //    //    new Function {Id = "CONTENT_COMMENT",Name = "Trang",ParentId = "CONTENT",SortOrder = 3,Url = "/contents/comments",Icon="fa-edit" },
        //    //    new Function {Id = "CONTENT_REPORT",Name = "Báo xấu",ParentId = "CONTENT",SortOrder = 3,Url = "/contents/reports",Icon="fa-edit" },

        //    //    new Function {Id = "STATISTIC",Name = "Thống kê", ParentId = null, Url = "/statistics",Icon="fa-bar-chart-o" },

        //    //    new Function {Id = "STATISTIC_MONTHLY_NEWMEMBER",Name = "Đăng ký từng tháng",ParentId = "STATISTIC",SortOrder = 1,Url = "/statistics/monthly-registers",Icon = "fa-wrench"},
        //    //    new Function {Id = "STATISTIC_MONTHLY_NEWKB",Name = "Bài đăng hàng tháng",ParentId = "STATISTIC",SortOrder = 2,Url = "/statistics/monthly-newkbs",Icon = "fa-wrench"},
        //    //    new Function {Id = "STATISTIC_MONTHLY_COMMENT",Name = "Comment theo tháng",ParentId = "STATISTIC",SortOrder = 3,Url = "/statistics/monthly-comments",Icon = "fa-wrench" },

        //    //    new Function {Id = "SYSTEM", Name = "Hệ thống", ParentId = null, Url = "/systems",Icon="fa-th-list" },

        //    //    new Function {Id = "SYSTEM_USER", Name = "Người dùng",ParentId = "SYSTEM",Url = "/systems/users",Icon="fa-desktop"},
        //    //    new Function {Id = "SYSTEM_ROLE", Name = "Nhóm quyền",ParentId = "SYSTEM",Url = "/systems/roles",Icon="fa-desktop"},
        //    //    new Function {Id = "SYSTEM_FUNCTION", Name = "Chức năng",ParentId = "SYSTEM",Url = "/systems/functions",Icon="fa-desktop"},
        //    //    new Function {Id = "SYSTEM_PERMISSION", Name = "Quyền hạn",ParentId = "SYSTEM",Url = "/systems/permissions",Icon="fa-desktop"},
        //    //});
        //}

        #endregion

        await context.SaveChangesAsync();
        transaction.Commit();
    }
}