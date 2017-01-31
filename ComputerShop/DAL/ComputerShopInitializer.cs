using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ComputerShop.Models;

namespace ComputerShop.DAL
{
    /// <summary>
    /// Usuwa i dodaje bazę danych jeżeli model uległ zmianie
    /// </summary>
    public class ComputerShopInitializer : DropCreateDatabaseIfModelChanges<ComputerShopContext>
    {
        /// <summary>
        /// Uzupełnia bazę o podane rekordy
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ComputerShopContext context)
        {
            var userRoles = new List<UserRole>
            {
                new UserRole {Name="Administrator" },
                new UserRole {Name="Moderator" },
                new UserRole {Name="Klient" },
                new UserRole {Name="Sprzedawca" }

            };

            userRoles.ForEach(u => context.UserRoles.Add(u));
            context.SaveChanges();

            

            var orderPayment = new List<OrderPayment>
            {
                new OrderPayment {Name="Przelew" },
                new OrderPayment {Name="Gotówka" },
            };

            orderPayment.ForEach(u => context.OrderPayments.Add(u));
            context.SaveChanges();

            var orderState = new List<OrderState>
            {
                new OrderState {Name="Dodany" },
                new OrderState {Name="Zamówiony" },
                new OrderState {Name="Zrealizowany" },
                new OrderState {Name="Niezrealizowany" }
            };

            orderState.ForEach(u => context.OrderStates.Add(u));
            context.SaveChanges();

            var orderSupply = new List<OrderSupply>
            {
                new OrderSupply {Name="Kurier" },
                new OrderSupply {Name="Poczta" },
                new OrderSupply {Name="Odbiór osobisty" },
            };

            orderSupply.ForEach(u => context.OrderSupplies.Add(u));
            context.SaveChanges();

            var productOrderState = new List<ProductOrderState>
            {
                new ProductOrderState { Name="POS1"},
                new ProductOrderState { Name="POS2"},
                new ProductOrderState { Name="POS3"},
                new ProductOrderState { Name="POS4"}
            };

            productOrderState.ForEach(u => context.ProductOrderStates.Add(u));
            context.SaveChanges();

            var categories = new List<Category>
            {
                new Category { Name = "Procesory", Description = "Description1",Enable=true},
                new Category { Name = "Płyty główne", Description = "Description1",Enable=true},
                new Category { Name = "Zasilacze", Description = "Description1",Enable=true},
                new Category { Name = "Karty graficzne", Description = "Description1",Enable=true},
                new Category { Name = "Dyski", Description = "Description1",Enable=true},
                new Category { Name = "HDD", Description = "Description1",Enable=true,ParentCategoryID=5},
                new Category { Name = "SSD", Description = "Description1",Enable=true,ParentCategoryID=5},
                new Category { Name = "Pamięci RAM", Description = "Description1",Enable=true},
                new Category { Name = "DDR1", Description = "Description1",Enable=true,ParentCategoryID=8},
                new Category { Name = "DDR2", Description = "Description1",Enable=true,ParentCategoryID=8},
                new Category { Name = "DDR3", Description = "Description1", ParentCategoryID=8,Enable=true},
                new Category { Name = "Drukarki", Description = "Description1",Enable=true},
                new Category { Name = "Laserowe", Description = "Description1", ParentCategoryID=12,Enable=true},
                new Category { Name = "Atramentowe", Description = "Description1", ParentCategoryID=12,Enable=true},
                new Category { Name = "Igłowe", Description = "Description1", ParentCategoryID=12,Enable=true},
                new Category { Name = "Monitory", Description = "Description1", Enable=true},
                new Category { Name = "Klawiatury", Description = "Description1",Enable=true},
                new Category { Name = "Myszki", Description = "Description1", Enable=true},
                new Category { Name = "Obudowy", Description = "Description1", Enable=true},
                new Category { Name = "AMD", Description = "Description1",Enable=true, ParentCategoryID=1},
                new Category { Name = "Intel", Description = "Description1",Enable=true, ParentCategoryID=1}
            };

            categories.ForEach(u => context.Categories.Add(u));
            context.SaveChanges();

            var productPictures = new List<ProductPicture>
            {
                new ProductPicture { Path = "/Images/amd1.jpg" },
                new ProductPicture { Path = "/Images/amd2.png" },
                new ProductPicture { Path = "/Images/intel1.jpg" },
                new ProductPicture { Path = "/Images/Intel_xeon.jpg" },
                new ProductPicture { Path = "/Images/mb1.jpg" },
                new ProductPicture { Path = "/Images/mb2.png" },
                new ProductPicture { Path = "/Images/psu1.jpg" },
                new ProductPicture { Path = "/Images/psu2.png" },
                new ProductPicture { Path = "/Images/gpu1.jpg" },
                new ProductPicture { Path = "/Images/gpu2.jpeg" },
                new ProductPicture { Path = "/Images/hdd1.jpg" },
                new ProductPicture { Path = "/Images/hdd2.jpg" },
                new ProductPicture { Path = "/Images/ssd1.jpg" },
                new ProductPicture { Path = "/Images/ssd2.jpg" },
                new ProductPicture { Path = "/Images/ddr11.jpg" },
                new ProductPicture { Path = "/Images/ddr12.jpg" },
                new ProductPicture { Path = "/Images/ddr21.jpg" },
                new ProductPicture { Path = "/Images/ddr22.jpg" },
                new ProductPicture { Path = "/Images/ddr31.jpg" },
                new ProductPicture { Path = "/Images/ddr32.jpg" },
                new ProductPicture { Path = "/Images/laserowa1.jpg" },
                new ProductPicture { Path = "/Images/laserowa2.jpg" },
                new ProductPicture { Path = "/Images/atramentowa1.jpg" },
                new ProductPicture { Path = "/Images/atramentowa2.jpg" },
                new ProductPicture { Path = "/Images/iglowa1.jpg" },
                new ProductPicture { Path = "/Images/iglowa2.jpg" },
                new ProductPicture { Path = "/Images/monitor1.jpg" },
                new ProductPicture { Path = "/Images/monitor2.jpg" },
                new ProductPicture { Path = "/Images/klawiatura1.jpg" },
                new ProductPicture { Path = "/Images/klawiatura2.jpg" },
                new ProductPicture { Path = "/Images/mysz1.jpg" },
                new ProductPicture { Path = "/Images/mysz2.jpg" },
                new ProductPicture { Path = "/Images/obudowa1.jpg" },
                new ProductPicture { Path = "/Images/obudowa2.jpg" },


            };
            productPictures.ForEach(u => context.ProductPictures.Add(u));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product { CategoryID = 20, Name = "AMD e1-6010",Price = 15, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=1},
                new Product { CategoryID = 20, Name = "AMD FX-6300",Price = 155, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=2},
                new Product { CategoryID = 21, Name = "Intel i7 3200",Price = 125, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=3},
                new Product { CategoryID = 21, Name = "Intel i5 8000",Price = 1155, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=4},
                new Product { CategoryID = 2, Name = "msi z97 gaming 7",Price = 1985, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=5},
                new Product { CategoryID = 2, Name = "Gigabyte h61M-D2P-B3",Price = 125, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=6},
                new Product { CategoryID = 3, Name = "PSU1",Price = 155, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=7},
                new Product { CategoryID = 3, Name = "PSU2",Price = 115, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=8},
                new Product { CategoryID = 4, Name = "Radeon 7950",Price = 155, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=9},
                new Product { CategoryID = 4, Name = "Radeon r9 270x",Price = 115, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=10},
                new Product { CategoryID = 6, Name = "Samsung ST500LM012",Price = 515, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=11},
                new Product { CategoryID = 6, Name = "Seagate 500gb st500dm002 ",Price = 915, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=12},
                new Product { CategoryID = 7, Name = "Crucial BX200 240GB",Price = 515, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=13},
                new Product { CategoryID = 7, Name = "Goodram CX100 240GB",Price = 215, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=14},
                new Product { CategoryID = 9, Name = "Crucial Ballistix Sport 4GB DDR1-1600",Price = 115, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=15},
                new Product { CategoryID = 9, Name = "Corsair Vengeance Low Profile 2x8GB",Price = 315, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=16},
                new Product { CategoryID = 10, Name = "G.Skill RipjawsX DDR2 2x4GB 1600MHz",Price = 615, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=17},
                new Product { CategoryID = 10, Name = "G.Skill RipjawsX DDR2 2x8GB 1600MHz",Price = 515, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=18},
                new Product { CategoryID = 11, Name = "GoodRam 4GB DDR3-1333 CL9 Single Rank",Price = 115, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=19},
                new Product { CategoryID = 11, Name = "GoodRam 4GB DDR3-1600 CL11",Price = 215, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=20},
                new Product { CategoryID = 13, Name = "HP LaserJet Pro P1102W",Price = 515, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=21},
                new Product { CategoryID = 13, Name = "Pantum P2500",Price = 150, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=22},
                new Product { CategoryID = 14, Name = "Brother HL-L8350CDW",Price = 155, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=23},
                new Product { CategoryID = 14, Name = "Pantum P3255DN",Price = 154, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=24},
                new Product { CategoryID = 15, Name = "Brother HL-5340DL",Price = 715, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=25},
                new Product { CategoryID = 15, Name = "HP LaserJet Pro 200 M201dw",Price = 215, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=26},
                new Product { CategoryID = 16, Name = "Philips 246V5LHAB",Price = 315, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=27},
                new Product { CategoryID = 16, Name = "LG 24M37D-B",Price = 215, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=28},
                new Product { CategoryID = 17, Name = "Natec Genesis RX55",Price = 1215, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=29},
                new Product { CategoryID = 17, Name = "Natec Genesis RX33",Price = 155, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=30},
                new Product { CategoryID = 18, Name = "Acme MW14",Price = 515, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=31},
                new Product { CategoryID = 18, Name = "Hama 7200P",Price = 150, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=32},
                new Product { CategoryID = 19, Name = "Mad Catz Cooler Master Storm Ceres-300",Price = 155, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=33},
                new Product { CategoryID = 19, Name = "Tracer Transformer TRM-504",Price = 154, Quantity = 1000,Enable=true,Description="Zestaw pamięci DDR3 DIMM o pojemności 8 GB (2x 4096 MB) pracująca z częstotliwością 1600 MHz o napięciu 1.65V. Opóźnienia cykli zegara wynoszą 9 CL. Chłodzona za pomocą radiatora.",ProductPictureID=34},
            };

            products.ForEach(u => context.Products.Add(u));
            context.SaveChanges();

            var contact = new Contact
            {
                AccountNumber = "00123412344567891478523695",
                Address = "Orzegowska 70/55, Bytom",
                Email = "kkrolikowski92@gmail.com",
                Telephone = 782247147,
                WeekOpenTime = "8:00-16:00",
                SaturdayOpenTime = "8:00-13:00",
                SundayOpenTime = "8:00-12:00",
                GoogleMapUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2546.565457518366!2d18.93627485173083!3d50.337357879359125!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x4716d31e0f9edcf3%3A0x47a2873dc5f0e177!2sChorzowska+27%2C+Bytom!5e0!3m2!1spl!2spl!4v1451671365753"
            };

            context.Contacts.Add(contact);
            context.SaveChanges();

            var regulation = new Regulation
            {
                Rule = "To jest regulamin sklepu"
            };

            context.Regulations.Add(regulation);
            context.SaveChanges();
            //var users = new List<User>
            //{
            //    new User {Name="Krzysiek" ,Surname="Królikowski",Email="kkrolikowski92@gmail.com",Password="supertajnehaslo", Address="Orzegowska 70/55, Bytom",Telephone=782247147,UserRoleID=1}
            //};

            //users.ForEach(u => context.Users.Add(u));
            //context.SaveChanges();

        }
    }
}