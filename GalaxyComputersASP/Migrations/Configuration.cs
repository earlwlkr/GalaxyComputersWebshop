namespace GalaxyComputersASP.Migrations
{
    using GalaxyComputersASP.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GalaxyComputersASP.Models.GalaxyComputersASPContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "GalaxyComputersASP.Models.GalaxyComputersASPContext";
        }

        protected override void Seed(GalaxyComputersASP.Models.GalaxyComputersASPContext context)
        {
            context.Products.AddOrUpdate(i => i.ID,
                new Product
                {
                    ID = 1,
                    Name = "ASUS B85M-G",
                    Price = 1689996,
                    Description = "Media-renowned UEFI BIOS from ASUS provides the smoothest mouse-controlled graphical BIOS, and now features more intuitive functions to quickly take you to favorite BIOS pages and frequently-accessed settings through custom shortcuts. You can even write quick notes in-BIOS for future reference, view an activity log of setting changes and modifications, and rename SATA ports. EZ Mode has a whole new look, upgraded with extra-friendly capabilities like detailed fan controls, XMP profile settings, SATA information, and fast clock adjustment. Together, these perfect your BIOS experience. ",
                    PublishDate = DateTime.Now,
                    CategoryID = 1,
                    ManufacturerID = 1,
                    ImagePath = "/Images/asus-b85m-g.jpg",
                    Views = 0,
                    Sales = 0
                }
            );
            context.Categories.AddOrUpdate(i => i.ID,
                new Category
                {
                    ID = 1,
                    Name = "ASUS B85M-G"
                }
            );
            context.Manufacturers.AddOrUpdate(i => i.ID,
                new Manufacturer
                {
                    ID = 1,
                    Name = "ASUS",
                    Description ="ASUS is the world's fifth-largest PC vendor by 2014 unit sales (after Lenovo, HP, Dell and Acer).[5] ASUS appears in BusinessWeek’s 'InfoTech 100' and 'Asia’s Top 10 IT Companies' rankings, and it ranked first in the IT Hardware category of the 2008 Taiwan Top 10 Global Brands survey with a total brand value of $1.3 billion.",
                    WebPage = "https://www.asus.com/",
                    ImagePath = "/Images/asus.jpg",
                    Country = "Taiwan",
                    DateFounded = DateTime.Parse("1989-04-02T00:00:00+07:00")
                }
           );
        }
    }
}
