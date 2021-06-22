using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chechka.DAL.Data;
using Chechka.DAL.Entities;
using Microsoft.AspNetCore.Identity;  //Lb4.5.6 Создание класса инициализации базы данных

namespace Chechka.Services
{
    //Lb4.5.6 {Создание класса инициализации базы данных
    public class DbInitializer
    {

        public static async Task Seed(ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            // создать БД, если она еще не создана
            context.Database.EnsureCreated();
            // проверка наличия ролей
            if (!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };
                // создать роль admin
                await roleManager.CreateAsync(roleAdmin);
            }
            // проверка наличия пользователей
            if (!context.Users.Any())
            {
                // создать пользователя user@mail.ru
                var user = new ApplicationUser
                {
                    Email = "user@mail.ru",
                    UserName = "user@mail.ru"
                };
                await userManager.CreateAsync(user, "123456");
                // создать пользователя admin@mail.ru
                var admin = new ApplicationUser
                {
                    Email = "admin@mail.ru",
                    UserName = "admin@mail.ru"
                };
                await userManager.CreateAsync(admin, "123456");
                // назначить роль admin
                admin = await userManager.FindByEmailAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");
            }


            //Lb.8.4.1{
            //проверка наличия групп объектов
            if (!context.ComputerPartGroups.Any())
            {
                context.ComputerPartGroups.AddRange(
                new List<ComputerPartGroup>
                {
                new ComputerPartGroup {GroupName="Стартеры"},
                new ComputerPartGroup {GroupName="Салаты"},
                new ComputerPartGroup {GroupName="Супы"},
                new ComputerPartGroup {GroupName="Основные блюда"},
                new ComputerPartGroup {GroupName="Напитки"},
                new ComputerPartGroup {GroupName="Десерты"}
                });
                await context.SaveChangesAsync();

            }
            // проверка наличия объектов
            if (!context.ComputerParts.Any())
            {
                context.ComputerParts.AddRange(
                new List<ComputerPart>
                {
                    new ComputerPart {
                        ComputerPartName="Процессор AMD Ryzen 5 3600",
                        Description="Socet AM4, 3.6/4.2 ГГц, 6 ядер, многопоточный, 65 Вт, 7 нм",
                        Price=455,
                        ComputerPartGroupId=1,
                        Image="cpu_amd_ryzen_5_3600.jpeg"
                    },
                    new ComputerPart {
                        ComputerPartName="Процессор AMD Ryzen 5 5600X",
                        Description="Socet AM4, 3.7/4.6 ГГц, 6 ядер, многопоточный, 65 Вт, 7 нм",
                        Price=816,
                        ComputerPartGroupId=1,
                        Image="cpu_amd_ryzen_5_5600X.jpeg"
                    },
                    new ComputerPart {
                        ComputerPartName="Процессор Intel Core i5-10400F",
                        Description="Socet LGA1200, 2.9/4.3 ГГц, 6 ядер, многопоточный, 65 Вт, 14 нм",
                        Price=431,
                        ComputerPartGroupId=1,
                        Image="cpu_Intel_core_i5_10400.jpeg"
                    },
                    new ComputerPart {
                        ComputerPartName="Видеокарта Gigabyte GeForce GTX 1660 OC 6GB GDDR5 GV-N1660OC-6GD",
                        Description="Частота ядра 1530/1830 МГц, 6 ГБ GDDR5, частота памяти 8 000 МГц, HDMI/DisplayPort(3)",
                        Price=1700,
                        ComputerPartGroupId=1,
                        Image="videocard_gigabyte_geforce_gtx_1660_oc_6gb_gddr5_gv-n1660oc-6gd.jpeg"
                    },
                    new ComputerPart {
                        ComputerPartName="Видеокарта ASUS Cerberus GeForce GTX 1050 Ti OC Edition 4GB GDDR5",
                        Description="Частота ядра 1392/1450 МГц, 4 ГБ GDDR5, частота памяти 7 008 МГц, DVI/HDMI/DisplayPort(3)",
                        Price=716,
                        ComputerPartGroupId=1,
                        Image="videocard_asus_cerberus_geforce_gtx_1050_ti_oc_edition_4gb_gddr5.jpeg"
                    },
                    new ComputerPart {
                        ComputerPartName="Gigabyte Radeon RX 580 Gaming 8GB GDDR5 GV-RX580GAMING-8GD rev. 1.0",
                        Description="Частота ядра 1257/2304 МГц, 8 ГБ GDDR5, частота памяти 8 000 МГц, DVI/HDMI/DisplayPort(3)",
                        Price=2893,
                        ComputerPartGroupId=1,
                        Image="videocard_gigabyte_radeon_rx_580_gaming_8gb_gddr5_gv-rx580gaming-8gd_rev._1.0.jpeg"
                    }
                });
                    await context.SaveChangesAsync();
                }
            //Lb.8.4.1}
        }
    }
    //Lb4.5.6 }
}
