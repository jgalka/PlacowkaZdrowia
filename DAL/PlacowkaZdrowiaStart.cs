using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PlacowkaZdrowia.Models;

namespace PlacowkaZdrowia.DAL
{
    public class PlacowkaZdrowiaStart : DropCreateDatabaseIfModelChanges<PlacowkaZdrowiaContext>
    {
        protected override void Seed(PlacowkaZdrowiaContext context)
        {
            var pacjenci = new List<Pacjent>
            {
                new Pacjent { Imie = "Carson",   Nazwisko = "Alexander", DataRejestracji = DateTime.Parse("2005-09-01") },
                new Pacjent { Imie = "Meredith", Nazwisko = "Alonso",    DataRejestracji = DateTime.Parse("2002-09-01") },
                new Pacjent { Imie = "Arturo",   Nazwisko = "Anand",     DataRejestracji = DateTime.Parse("2003-09-01") },
                new Pacjent { Imie = "Gytis",    Nazwisko = "Barzdukas", DataRejestracji = DateTime.Parse("2002-09-01") },
                new Pacjent { Imie = "Yan",      Nazwisko = "Li",        DataRejestracji = DateTime.Parse("2002-09-01") },
                new Pacjent { Imie = "Peggy",    Nazwisko = "Justice",   DataRejestracji = DateTime.Parse("2001-09-01") },
                new Pacjent { Imie = "Laura",    Nazwisko = "Norman",    DataRejestracji = DateTime.Parse("2003-09-01") },
                new Pacjent { Imie = "Nino",     Nazwisko = "Olivetto",  DataRejestracji = DateTime.Parse("2005-09-01") }
            };
            pacjenci.ForEach(s => context.Pacjenci.Add(s));
            context.SaveChanges();

            var lekarze = new List<Lekarz>
            {
                new Lekarz { Imie = "Kim",     Nazwisko = "Abercrombie", HireDate = DateTime.Parse("1995-03-11") },
                new Lekarz { Imie = "Fadi",    Nazwisko = "Fakhouri",    HireDate = DateTime.Parse("2002-07-06") },
                new Lekarz { Imie = "Roger",   Nazwisko = "Harui",       HireDate = DateTime.Parse("1998-07-01") },
                new Lekarz { Imie = "Candace", Nazwisko = "Kapoor",      HireDate = DateTime.Parse("2001-01-15") },
                new Lekarz { Imie = "Roger",   Nazwisko = "Zheng",       HireDate = DateTime.Parse("2004-02-12") }
            };
            lekarze.ForEach(s => context.Lekarze.Add(s));
            context.SaveChanges();

            var dzialy = new List<Dzial>
            {
                new Dzial { Name = "English",     Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), OsobaID = 9 },
                new Dzial { Name = "Mathematics", Budget = 100000, StartDate = DateTime.Parse("2007-09-01"), OsobaID = 10 },
                new Dzial { Name = "Engineering", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), OsobaID = 11 },
                new Dzial { Name = "Economics",   Budget = 100000, StartDate = DateTime.Parse("2007-09-01"), OsobaID = 12 }
            };
            dzialy.ForEach(s => context.Dzialy.Add(s));
            context.SaveChanges();

            var zabiegi = new List<Zabieg>
            {
                new Zabieg { ZabiegID = 1050, Tytul = "Chemistry",      Koszty = 3, DzialID = 3, Lekarze = new List<Lekarz>() },
                new Zabieg { ZabiegID = 4022, Tytul = "Microeconomics", Koszty = 3, DzialID = 4, Lekarze = new List<Lekarz>() },
                new Zabieg { ZabiegID = 4041, Tytul = "Macroeconomics", Koszty = 3, DzialID = 4, Lekarze = new List<Lekarz>() },
                new Zabieg { ZabiegID = 1045, Tytul = "Calculus",       Koszty = 4, DzialID = 2, Lekarze = new List<Lekarz>() },
                new Zabieg { ZabiegID = 3141, Tytul = "Trigonometry",   Koszty = 4, DzialID = 2, Lekarze = new List<Lekarz>() },
                new Zabieg { ZabiegID = 2021, Tytul = "Composition",    Koszty = 3, DzialID = 1, Lekarze = new List<Lekarz>() },
                new Zabieg { ZabiegID = 2042, Tytul = "Literature",     Koszty = 4, DzialID = 1, Lekarze = new List<Lekarz>() }
            };
            zabiegi.ForEach(s => context.Zabiegi.Add(s));
            context.SaveChanges();

            zabiegi[0].Lekarze.Add(lekarze[0]);
            zabiegi[0].Lekarze.Add(lekarze[1]);
            zabiegi[1].Lekarze.Add(lekarze[2]);
            zabiegi[2].Lekarze.Add(lekarze[2]);
            zabiegi[3].Lekarze.Add(lekarze[3]);
            zabiegi[4].Lekarze.Add(lekarze[3]);
            zabiegi[5].Lekarze.Add(lekarze[3]);
            zabiegi[6].Lekarze.Add(lekarze[3]);
            context.SaveChanges();

            var rejestracje = new List<Rejestracja>
            {
                new Rejestracja { OsobaID = 1, ZabiegID = 1050, Typ = 1 },
                new Rejestracja { OsobaID = 1, ZabiegID = 4022, Typ = 3 },
                new Rejestracja { OsobaID = 1, ZabiegID = 4041, Typ = 1 },
                new Rejestracja { OsobaID = 2, ZabiegID = 1045, Typ = 2 },
                new Rejestracja { OsobaID = 2, ZabiegID = 3141, Typ = 4 },
                new Rejestracja { OsobaID = 2, ZabiegID = 2021, Typ = 4 },
                new Rejestracja { OsobaID = 3, ZabiegID = 1050            },
                new Rejestracja { OsobaID = 4, ZabiegID = 1050,           },
                new Rejestracja { OsobaID = 4, ZabiegID = 4022, Typ = 4 },
                new Rejestracja { OsobaID = 5, ZabiegID = 4041, Typ = 3 },
                new Rejestracja { OsobaID = 6, ZabiegID = 1045            },
                new Rejestracja { OsobaID = 7, ZabiegID = 3141, Typ = 2 },
            };
            rejestracje.ForEach(s => context.Rejestracje.Add(s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment { OsobaID = 9, Location = "Smith 17" },
                new OfficeAssignment { OsobaID = 10, Location = "Gowan 27" },
                new OfficeAssignment { OsobaID = 11, Location = "Thompson 304" },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.Add(s));
            context.SaveChanges();
        }
    }
}