using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder.Options;

            using (ApplicationContext db = new ApplicationContext(options))
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                // заполняем таблицу валют
                var cur1 = new Currency 
                {
                    CurrencyFull = "Рубль", 
                    CurrencyShort = "RUB" 
                };
                var cur2 = new Currency 
                {
                    CurrencyFull = "Евро",
                    CurrencyShort = "EUR" 
                };
                var cur3 = new Currency 
                { 
                    CurrencyFull = "Британские фунты",
                    CurrencyShort = "GBP"
                };
                var cur4 = new Currency 
                {
                    CurrencyFull = "Японская йена",
                    CurrencyShort = "JPY"
                };
                var cur5 = new Currency 
                {
                    CurrencyFull = "Австралийский доллар",
                    CurrencyShort = "AUD" 
                };
                db.Currencies.AddRange(cur1, cur2, cur3, cur4, cur5);

                // заполняем таблицу типов сделок
                var dealtype1 = new DealType
                { 
                    Type = "брокерская"
                };
                var dealtype2 = new DealType
                { 
                    Type = "дилерская"
                };
                db.DealTypes.AddRange(dealtype1, dealtype2);

                // заполняем таблицу мест проведения сделок
                var dealplace1 = new DealPlace
                {
                    DealPlaceFull = "National Association of Securities Dealers Automated Quotation",
                    DealPlaceShort = "NASDAQ"
                };
                var dealplace2 = new DealPlace
                {
                    DealPlaceFull = "New York Stock Exchange",
                    DealPlaceShort = "NYSE"
                };
                var dealplace3 = new DealPlace
                {
                    DealPlaceFull = "Франкфуртская фондовая биржа",
                    DealPlaceShort = "FWB"
                };
                var dealplace4 = new DealPlace
                {
                    DealPlaceFull = "Гонконгская фондовая биржа",
                    DealPlaceShort = "HKSE"
                };
                var dealplace5 = new DealPlace
                {
                    DealPlaceFull = "Лондонская фондовая биржа",
                    DealPlaceShort = "LSE"
                };
                db.DealPlaces.AddRange(dealplace1, dealplace2, dealplace3, dealplace4, dealplace5);

                // заполняем таблицу сделок
                var deal1 = new Deal
                {
                    Type = dealtype1,
                    Place = dealplace1,
                    Currency = cur1,
                    Tiker = "NVDA",
                    Order = 123,
                    Number = "ea345",
                    Quantity = 340,
                    Price = 10.5F,
                    TotalCost = 3570,
                    Trader = "uw1234u",
                    Commision = 4
                };
                var deal2 = new Deal
                {
                    Type = dealtype2,
                    Place = dealplace2,
                    Currency = cur2,
                    Tiker = "GOOG",
                    Order = 52,
                    Number = "gd123",
                    Quantity = 100,
                    Price = 0.5F,
                    TotalCost = 50,
                    Trader = "hg8933i",
                    Commision = 7
                };
                var deal3 = new Deal
                {
                    Type = dealtype2,
                    Place = dealplace3,
                    Currency = cur3,
                    Tiker = "AMZN",
                    Order = 3458,
                    Number = "oz390",
                    Quantity = 9000,
                    Price = 1.0F,
                    TotalCost = 9000,
                    Trader = "uw1234u",
                    Commision = 100
                };
                var deal4 = new Deal
                {
                    Type = dealtype1,
                    Place = dealplace4,
                    Currency = cur4,
                    Tiker = "TSLA",
                    Order = 1,
                    Number = "qa101",
                    Quantity = 20000,
                    Price = 1000F,
                    TotalCost = 20000000,
                    Trader = "lm1089c",
                    Commision = 9000
                };
                var deal5 = new Deal
                {
                    Type = dealtype2,
                    Place = dealplace5,
                    Currency = cur1,
                    Tiker = "AMD",
                    Order = 908,
                    Number = "nb959",
                    Quantity = 465,
                    Price = 127.320F,
                    TotalCost = 59203.8F,
                    Trader = "rt3090x",
                    Commision = 1002
                };
                var deal6 = new Deal
                {
                    Type = dealtype1,
                    Place = dealplace3,
                    Currency = cur1,
                    Tiker = "WBD",
                    Order = 3557,
                    Number = "uy001",
                    Quantity = 57399,
                    Price = 10,
                    TotalCost = 573990,
                    Trader = "ve3902x",
                    Commision = 5839.9F
                };
                var deal7 = new Deal
                {
                    Type = dealtype1,
                    Place = dealplace1,
                    Currency = cur2,
                    Tiker = "CVX",
                    Order = 75,
                    Number = "jc910",
                    Quantity = 860,
                    Price = 1234.0F,
                    TotalCost = 1061240.0F,
                    Trader = "uw1234u",
                    Commision = 3890.219F
                };
                db.Deals.AddRange(deal1, deal2, deal3, deal4, deal5, deal6, deal7);
                db.SaveChanges();
            }
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var deals = db.Deals
                    .Include(deal => deal.Type)
                    .Include(deal => deal.Place)
                    .Include(deal => deal.Currency)
                    .ToList();

                Console.WriteLine("---- Жадная загрузка ----\n");

                foreach (var deal in deals)
                {
                    Console.WriteLine($"Сделка номер {deal.Number}");
                    Console.WriteLine($"Место сделки и валюта: " +
                        $"{deal.Place?.DealPlaceFull}({deal.Place?.DealPlaceShort}), " +
                        $"{deal.Currency?.CurrencyFull}({deal.Currency?.CurrencyShort})");
                    Console.WriteLine($"Тикер: {deal.Tiker}\n" +
                        $"Номер поручения: {deal.Order}\n" +
                        $"Количество ценных бумаг: {deal.Quantity}\n" +
                        $"Цена: {deal.Price}\nОбщая сумма: {deal.TotalCost}\n" +
                        $"Код трейдера: {deal.Trader}\n" +
                        $"Сумма коммиссии: {deal.Commision}");
                    Console.WriteLine("\n-----------------------------");
                }
            }
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var place = db.DealPlaces.FirstOrDefault();
                db.Deals.Where(d => d.PlaceId == place.Id).Load();

                Console.WriteLine("\n---- Явная загрузка ----\n");

                Console.WriteLine($"Место проведения встречи: { place.DealPlaceFull} ({ place.DealPlaceShort})");
                Console.WriteLine("-----------------------------");
                foreach (var deal in place.Deals)
                    Console.WriteLine($"Номер сделки: {deal.Number}");

            }
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var deals = db.Deals.ToList();

                Console.WriteLine("\n\n---- Ленивая загрузка ----\n");

                foreach (var deal in deals)
                {
                    Console.WriteLine($"Сделка номер {deal.Number}");
                    Console.WriteLine($"Место сделки и валюта: " +
                        $"{deal.Place?.DealPlaceFull}({deal.Place?.DealPlaceShort}), " +
                        $"{deal.Currency?.CurrencyFull}({deal.Currency?.CurrencyShort})");
                    Console.WriteLine($"Тикер: {deal.Tiker}\n" +
                        $"Номер поручения: {deal.Order}\n" +
                        $"Количество ценных бумаг: {deal.Quantity}\n" +
                        $"Цена: {deal.Price}\nОбщая сумма: {deal.TotalCost}\n" +
                        $"Код трейдера: {deal.Trader}\n" +
                        $"Сумма коммиссии: {deal.Commision}");
                    Console.WriteLine("\n-----------------------------");
                }

            }
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var deals = db.Deals
                    .Include(o => o.Type)
                    .Include(o => o.Place)
                    .Where(o => o.TypeId == 1)
                    .OrderBy(o => o.Order)
                    .ToList();

                Console.WriteLine("\n\n---- LINQ запрос ----\n");
                foreach (var deal in deals)
                {
                    Console.WriteLine($"Сделка номер {deal.Number}");
                    Console.WriteLine($"Место сделки и валюта: " +
                        $"{deal.Place?.DealPlaceFull}({deal.Place?.DealPlaceShort}), " +
                        $"{deal.Currency?.CurrencyFull}({deal.Currency?.CurrencyShort})");
                    Console.WriteLine($"Тикер: {deal.Tiker}\n" +
                        $"Номер поручения: {deal.Order}\n" +
                        $"Количество ценных бумаг: {deal.Quantity}\n" +
                        $"Цена: {deal.Price}\nОбщая сумма: {deal.TotalCost}\n" +
                        $"Код трейдера: {deal.Trader}\n" +
                        $"Сумма коммиссии: {deal.Commision}");
                    Console.WriteLine("\n-----------------------------");
                }
            }
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var deals = from deal in db.Deals
                            join type in db.DealTypes on deal.TypeId equals type.Id
                            join place in db.DealPlaces on deal.PlaceId equals place.Id
                            select new
                            {
                                number = deal.Number,
                                type = type.Type,
                                place = place.DealPlaceFull
                            };

                Console.WriteLine("\n\n---- Соединение трех таблиц ----\n");
                foreach (var deal in deals)
                {
                    Console.WriteLine($"Сделка номер {deal.number}");
                    Console.WriteLine($"Тип сделки: {deal.type}");
                    Console.WriteLine($"Место сделки: {deal.place}\n");
                }
            }
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var deals = db.Deals.Where(d => d.TotalCost > 50000)
                    .Union(db.Deals.Where(d => d.Price <= 10));

                Console.WriteLine("\n\n---- Union - объединение ----\n");

                foreach (var deal in deals)
                {
                    Console.WriteLine($"Сделка номер {deal.Number}");
                    Console.WriteLine($"Общая сумма и цена: {deal.TotalCost}, {deal.Price}\n");
                }
            }
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var query1 = db.Deals.Where(d => d.TotalCost > 30000);
                var query2 = db.Deals.Where(d => d.Price <= 200);

                var deals = query1.Except(query2);

                Console.WriteLine("\n\n---- Except - разность ----\n");

                foreach (var deal in deals)
                {
                    Console.WriteLine($"Сделка номер {deal.Number}");
                    Console.WriteLine($"Общая сумма и цена: {deal.TotalCost}, {deal.Price}\n");
                }
            }
            using (ApplicationContext db = new ApplicationContext(options))
            {
                var deals = db.Deals.Where(d => d.TotalCost > 30000)
                    .Intersect(db.Deals.Where(d => d.Price <= 200));

                Console.WriteLine("\n\n---- Intersect - пересечение ----\n");

                foreach (var deal in deals)
                {
                    Console.WriteLine($"Сделка номер {deal.Number}");
                    Console.WriteLine($"Общая сумма и цена: {deal.TotalCost}, {deal.Price}\n");
                }
            }
            using (ApplicationContext db = new ApplicationContext(options))
            {
                double dealsSum = db.Deals
                    .Where(d => d.TotalCost < 10000)
                    .Sum(d => d.TotalCost);

                Console.WriteLine("---- Sum - сумма ----");
                Console.WriteLine($"\nСуммарная стоимость всех сделок где " +
                    $"общая стоимость сделки не превышает 10000: {dealsSum}$");
            }
            using (ApplicationContext db = new ApplicationContext(options))
            {
                int dealsCnt = db.Deals.Count(d => d.Commision < 5000);

                Console.WriteLine("---- Count - аггрегирование ----");

                Console.WriteLine($"\nКоличество сделок {dealsCnt}\n");
            }
            Console.Read();
        }
    }
}
