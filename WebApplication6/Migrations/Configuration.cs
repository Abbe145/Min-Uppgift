using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WebApplication6.Migrations.Skor
{
    internal sealed class Configuration : DbMigrationsConfiguration<MyDatabase.SkorModelDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebApplication6.MyDatabase.SkorModelDB";
            //MigrationsDirectory = @"Migrations\Skor";
        }

        protected override void Seed(WebApplication6.MyDatabase.SkorModelDB context)
        {
            context.CategoriesTable.AddOrUpdate(c => c.CategoryId,
                new Models.CategoryModel
                {
                    CategoryId = 1,
                    CategoryName = "Nike",
                    //CategoryDescription = "Nike it and Like it."
                },
                new Models.CategoryModel
                {
                    CategoryId = 2,
                    CategoryName = "Addidas",
                    //CategoryDescription = "Addidas it and Like it."
                },
                new Models.CategoryModel
                {
                    CategoryId = 3,
                    CategoryName = "Reebok",
                    //CategoryDescription = "Reebok it and Like it."
                });


            context.SkorTable.AddOrUpdate(x => x.SkorId,
                new Models.SkorModel
                {
                    SkorId = 1,
                    Name = "Runforst",
                    Color = "Black",
                    Model = "NikeXXX",
                    Size = 44,
                    Price = 3599,
                    Description = "Vill du springa l�ng str�ckor utan bl�sor p� f�tterna? D� �r Runforst skorna f�r dig. Det s�gs att en man sprang runt America i dessa skor. Run Forest Run!",

                    CategoryId = 1,
                },
                new Models.SkorModel
                {
                    SkorId = 2,
                    Name = "Air aax",
                    Color = "Red",
                    Model = "4999",
                    Size = 41,
                    Price = 1299,
                    Description = "Coola och fr�sha r�da nya Nike 4999.",

                    CategoryId = 1,
                },
                new Models.SkorModel
                {
                    SkorId = 3,
                    Name = "Adidas A",
                    Color = " Black",
                    Model = "1499",
                    Size = 44,
                    Price = 799,
                    Description = "Passar i alla l�gen, Adidas A 1499.",

                    CategoryId = 2,

                },
                new Models.SkorModel
                {
                    SkorId = 4,
                    Name = "Adidas B",
                    Color = "Black",
                    Model = "1499",
                    Size = 44,
                    Price = 1399,
                    Description = "Dessa skor �r en favorit i m�nga �gon.",

                    CategoryId = 2,
                },
                new Models.SkorModel
                {
                    SkorId = 6,
                    Name = "Reebok ZZZ",
                    Color = "Blue",
                    Model = "1499",
                    Size = 44,
                    Price = 599,
                    Description = "Snygga skor men inte f�r dyra som passar dig som vill ha n�got som ser bra ut men �ven kunna g� ut i skolen och springa.",

                    CategoryId = 3,
                },
                new Models.SkorModel
                {
                    SkorId = 5,
                    Name = "Reebok XXX",
                    Color = "Yellow",
                    Model = "Moda",
                    Size = 33,
                    Price = 399,
                    Description = "Nya Reebok XXX skorna �r designade f�r sport.",

                    CategoryId = 3,
                });
        }
    }
}

