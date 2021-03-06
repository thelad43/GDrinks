﻿namespace GDrinks.Services.Implementations
{
    using GDrinks.Data;
    using GDrinks.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;

    public class DbSeederService : IDbSeederService
    {
        public void SeedData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<GDrinksDbContext>();

                this.SeedCategories(db);

                this.SeedDrinks(db);
            }
        }

        private void SeedDrinks(GDrinksDbContext db)
        {
            var alcoholicCategory = db
                .Categories
                .FirstOrDefault(c => c.Name == "Alcoholic");

            var nonAlcoholicCategory = db
                .Categories
                .FirstOrDefault(c => c.Name == "Non-alcoholic");

            var drinks = new Drink[]
            {
                new Drink
                {
                    Name = "Beer",
                    Price = 7.95M,
                    Description = "The most widely consumed alcohol",
                    FullDescription = "Beer is the world's oldest[1][2][3] and most widely consumed[4] alcoholic drink; it is the third most popular drink overall, after water and tea.[5] The production of beer is called brewing, which involves the fermentation of starches, mainly derived from cereal grains—most commonly malted barley, although wheat, maize (corn), and rice are widely used.[6] Most beer is flavoured with hops, which add bitterness and act as a natural preservative, though other flavourings such as herbs or fruit may occasionally be included. The fermentation process causes a natural carbonation effect, although this is often removed during processing, and replaced with forced carbonation.[7] Some of humanity's earliest known writings refer to the production and distribution of beer: the Code of Hammurabi included laws regulating beer and beer parlours.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/beerL_2.jpg",
                    IsInStock = true,
                    IsPreferred = true,
                    ImageThumbnailUrl = "http://imgh.us/beerS_1.jpeg"
                },
                new Drink
                {
                    Name = "Rum & Coke",
                    Price = 12.95M,
                    Description = "Cocktail made of cola, lime and rum.",
                    FullDescription = "The world's second most popular drink was born in a collision between the United States and Spain. It happened during the Spanish-American War at the turn of the century when Teddy Roosevelt, the Rough Riders, and Americans in large numbers arrived in Cuba. One afternoon, a group of off-duty soldiers from the U.S. Signal Corps were gathered in a bar in Old Havana. Fausto Rodriguez, a young messenger, later recalled that Captain Russell came in and ordered Bacardi (Gold) rum and Coca-Cola on ice with a wedge of lime. The captain drank the concoction with such pleasure that it sparked the interest of the soldiers around him. They had the bartender prepare a round of the captain's drink for them. The Bacardi rum and Coke was an instant hit. As it does to this day, the drink united the crowd in a spirit of fun and good fellowship. When they ordered another round, one soldier suggested that they toast ¡Por Cuba Libre! in celebration of the newly freed Cuba.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/rumCokeL.jpg",
                    IsInStock = true,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/rumAndCokeS.jpg"
                },
                new Drink
                {
                    Name = "Tequila",
                    Price = 12.95M,
                    Description = "Beverage made from the blue agave plant.",
                    FullDescription = "Tequila (Spanish About this sound [teˈkila] (help·info)) is a regionally specific name for a distilled beverage made from the blue agave plant, primarily in the area surrounding the city of Tequila, 65 km (40 mi) northwest of Guadalajara, and in the highlands (Los Altos) of the central western Mexican state of Jalisco. Although tequila is similar to mezcal, modern tequila differs somewhat in the method of its production, in the use of only blue agave plants, as well as in its regional specificity. Tequila is commonly served neat in Mexico and as a shot with salt and lime across the rest of the world.The red volcanic soil in the surrounding region is particularly well suited to the growing of the blue agave, and more than 300 million of the plants are harvested there each year.[1] Agave tequila grows differently depending on the region. Blue agaves grown in the highlands Los Altos region are larger in size and sweeter in aroma and taste. Agaves harvested in the lowlands, on the other hand, have a more herbaceous fragrance and flavor.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/tequilaL.jpg",
                    IsInStock = true,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/tequilaS.jpg"
                },
                new Drink
                {
                    Name = "Wine ",
                    Price = 16.75M,
                    Description = "A very elegant alcoholic drink",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/wineL.jpg",
                    IsInStock = true,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/wineS.jpg"
                    },
                new Drink
                {
                    Name = "Margarita",
                    Price = 17.95M,
                    Description = "A cocktail with sec, tequila and lime",
                    FullDescription= "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/margaritaL.jpg",
                    IsInStock = true,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/margaritaS.jpg"
                },
                new Drink
                {
                    Name = "Whiskey with Ice",
                    Price = 15.95M,
                    Description = "The best way to taste whiskey",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/whiskyIceL.jpg",
                    IsInStock = false,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/whiskeyS.jpg"
                },
                new Drink
                {
                    Name = "Jägermeister",
                    Price = 15.95M,
                    Description = "A German digestif made with 56 herbs",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/jagermeisterL.jpg",
                    IsInStock = false,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/jagermeisterS.jpg"
                    },
                new Drink
                {
                    Name = "Champagne",
                    Price = 15.95M,
                    Description = "That is how sparkling wine can be called" ,
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/champagneL.jpg",
                    IsInStock = false,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/champagneS.jpg"
                    },
                new Drink
                {
                    Name = "Piña colada ",
                    Price = 15.95M,
                    Description = "A sweet cocktail made with rum.",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/pinaColadaL.jpg",
                    IsInStock = false,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/pinaColadaS.jpg"
                    },
                new Drink
                {
                    Name = "White Russian",
                    Price = 15.95M,
                    Description = "A cocktail made with vodka ",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/whiteRussianL.jpg",
                    IsInStock = false,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/whiteRussianS.jpg"
                    },
                new Drink
                {
                    Name = "Long Island Iced Tea",
                    Price = 15.95M,
                    Description = "Aa mixed drink made with tequila.",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/longTeaL.jpg",
                    IsInStock = false,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/islandTeaS.jpg"
                    },
                new Drink
                {
                    Name = "Vodka",
                    Price = 15.95M,
                    Description = "A distilled beverage with water and ethanol.",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/vodkaL.jpg",
                    IsInStock = false,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/vodkaS.jpg"
                    },
                new Drink
                {
                    Name = "Gin and tonic",
                    Price = 15.95M,
                    Description = "Made with gin and tonic water poured over ice.",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/ginTonicL.jpg",
                    IsInStock = false,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/ginTonicS.jpg"
                    },
                new Drink
                {
                    Name = "Cosmopolitan",
                    Price = 15.95M,
                    Description = "Made with vodka, triple sec, cranberry juice.",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = alcoholicCategory,
                    ImageUrl = "http://imgh.us/cosmopolitanL.jpg",
                    IsInStock = false,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/cosmopolitanS.jpg"
                    },
                new Drink
                {
                    Name = "Tea ",
                    Price = 12.95M,
                    Description = "Made by leaves of the tea plant in hot water.",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = nonAlcoholicCategory,
                    ImageUrl = "http://imgh.us/teaL.jpg",
                    IsInStock = true,
                    IsPreferred = true,
                    ImageThumbnailUrl = "http://imgh.us/teaS.jpg"
                    },
                new Drink
                {
                    Name = "Water ",
                    Price = 12.95M,
                    Description = " It makes up more than half of your body weight ",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = nonAlcoholicCategory,
                    ImageUrl = "http://imgh.us/waterL.jpg",
                    IsInStock = true,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/waterS_1.jpg"
                    },
                new Drink
                {
                    Name = "Coffee ",
                    Price = 12.95M,
                    Description = " A beverage prepared from coffee beans",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = nonAlcoholicCategory,
                    ImageUrl = "http://imgh.us/coffeeL.jpg",
                    IsInStock = true,
                    IsPreferred = true,
                    ImageThumbnailUrl = "http://imgh.us/coffeS.jpg"
                    },
                new Drink
                {
                    Name = "Kvass",
                    Price = 12.95M,
                    Description = "A very refreshing Russian beverage",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = nonAlcoholicCategory,
                    ImageUrl = "http://imgh.us/kvassL.jpg",
                    IsInStock = true,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/kvassS.jpg"
                    },
                new Drink
                {
                    Name = "Juice ",
                    Price = 12.95M,
                    Description = "Naturally contained in fruit or vegetable tissue.",
                    FullDescription = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, comes from a line in section 1.10.32. The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 from de Finibus Bonorum et Malorum by Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham.",
                    Category = nonAlcoholicCategory,
                    ImageUrl = "http://imgh.us/juiceL.jpg",
                    IsInStock = true,
                    IsPreferred = false,
                    ImageThumbnailUrl = "http://imgh.us/juiceS.jpg"
                    }
            };

            db.AddRange(drinks);
            db.SaveChanges();
        }

        private void SeedCategories(GDrinksDbContext db)
        {
            var categories = new Category[]
            {
                new Category { Name = "Alcoholic", Description="All alcoholic drinks" },
                new Category { Name = "Non-alcoholic", Description="All non-alcoholic drinks" }
            };

            db.AddRange(categories);
            db.SaveChanges();
        }
    }
}