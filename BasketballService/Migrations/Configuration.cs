namespace BasketballService.Migrations
{
    using BasketballService.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BasketballService.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BasketballService.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Klubovi.AddOrUpdate(x => x.Id,
                new KosarkaskiKlub()
                {
                    Id = 1,
                    Naziv = "Sacramento Kings",
                    Liga = "NBA",
                    GodinaOsnivanja = 1985,
                    BrojTrofeja = 5
                },
                new KosarkaskiKlub()
                {
                    Id = 2,
                    Naziv = "Dallas Mavericks",
                    Liga = "NBA",
                    GodinaOsnivanja = 1980,
                    BrojTrofeja = 6
                },
                new KosarkaskiKlub()
                {
                    Id = 3,
                    Naziv = "Indiana Pacers",
                    Liga = "NBA",
                    GodinaOsnivanja = 1967,
                    BrojTrofeja = 13
                }
                );

            context.Kosarkasi.AddOrUpdate(x => x.Id,
                new Kosarkas()
                {
                    Id = 1,
                    ImePrezime = "Bogdan Bogdanovic",
                    GodinaRodjenja = 1992,
                    BrojUtakmica = 96,
                    ProsecanBrPoena = 12.3,
                    KosarkaskiKlubId = 1
                },
                 new Kosarkas()
                 {
                     Id = 2,
                     ImePrezime = "Luka Doncic",
                     GodinaRodjenja = 1999,
                     BrojUtakmica = 26,
                     ProsecanBrPoena = 18.2,
                     KosarkaskiKlubId = 2
                 },
                  new Kosarkas()
                  {
                      Id = 3,
                      ImePrezime = "Bojan Bogdanovic",
                      GodinaRodjenja = 1989,
                      BrojUtakmica = 105,
                      ProsecanBrPoena = 14.8,
                      KosarkaskiKlubId = 3
                  },
                   new Kosarkas()
                   {
                       Id = 4,
                       ImePrezime = "Nemanja Bjelica",
                       GodinaRodjenja = 1988,
                       BrojUtakmica = 25,
                       ProsecanBrPoena = 10.8,
                       KosarkaskiKlubId = 1
                   });
        }
    }
}
