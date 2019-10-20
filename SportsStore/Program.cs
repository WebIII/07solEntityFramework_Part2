using System;
using System.Collections.Generic;
using SportsStore.Data;
using System.Linq;
using SportsStore.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                new SportsStoreDataInitializer(context).InitializeData();
                var products = context.Products.ToList();
                products.ForEach(c => Console.WriteLine(c.Name));

                Console.WriteLine("Database is aangemaakt");
            }

            DoExercise();
            Console.ReadLine();
        }

        private static void DoExercise()
        {

            IEnumerable<Product> products;
            Product product;
            IEnumerable<Customer> customers;
            Customer customer;


            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("--1. Alle producten gesorteerd op prijs oplopend, dan op naam--");
                products = context.Products
                     .OrderBy(p => p.Price)
                     .ThenBy(p => p.Name);
                WriteProducts(products);
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("\n--2. Alle klanten die wonen te gent, gesorteerd op naam --");
                customers = context.Customers
                     .Where(c => c.City.Name == "Gent")
                     .OrderBy(c => c.Name)
                     .ThenBy(c => c.FirstName).ToList();
                WriteCustomers(customers);
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("\n--3. Het aantal klanten in Gent--");
                Console.WriteLine(customers.Count());
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("\n--4. De klant met  customername student4. --");
                customer = context.Customers
                    .Include(c => c.Orders)
                    .ThenInclude(l => l.OrderLines)
                    .ThenInclude(ol=>ol.Product)
                    .SingleOrDefault(c => c.CustomerName == "student4");
                Console.WriteLine(customer.Name + " " + customer.FirstName);

                Console.WriteLine("\n--5. Vervolg op vorige query. Print nu de orders van die klant af. Print (methode WriteOrder) de Orderdate, DeliveryDate, en Total af. --");
                //Vervolledig hiervoor eerst de Property Total in de klasse Order. Haal de klant niet opnieuw op! Haal klant en zijn Orders en Orderlines in 1 keer op. 
                //zie methode WriteOrder voor het weergeven van een order
                if (customer != null)
                    foreach (Order o in customer.Orders)
                        WriteOrder(o);
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("\n--6. Alle producten van de categorie soccer, gesorteerd op prijs descending--");
                products =
                    context.Categories.Include(c=>c.Products).ThenInclude(p=>p.Product)
                    .FirstOrDefault(c => c.Name == "soccer")
                    .Products.Select(p => p.Product)
                    .OrderByDescending(p => p.Price);
                WriteProducts(products);
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("\n--7A. Maak een nieuwe cart aan en voeg product met id 1 toe(aantal 2).--");
                Product p1 = context.Products
                   .FirstOrDefault(p => p.ProductId == 1);
                Cart cart = new Cart();
                cart.AddLine(p1, 2);
  
                Console.WriteLine("\n--7B. Plaatst dan een order voor student 4 voor deze cart, deliverydate binnen 20 dagen, giftwrapping false en deliveryAddress = adres van de klant--. Persisteer in database. Print vervolgens alle orders van de klant.");
                customer = context.Customers.Include(c => c.City)
                    .SingleOrDefault(c => c.CustomerName == "student4");
                if (customer != null)
                    customer.PlaceOrder(cart, DateTime.Today.AddDays(20), false, customer.Street, customer.City);
                context.SaveChanges();
                if (customer != null)
                {
                    customer = context.Customers
                        .Include(c => c.Orders).ThenInclude(o => o.OrderLines).ThenInclude(ol => ol.Product)
                        .FirstOrDefault(c => c.CustomerName == "student4");
                    foreach (Order o in customer.Orders)
                        WriteOrder(o);
                }
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("\n--8. Alle klanten met een order met DeliveryDate binnen de 10 dagen, sorteer op naam--");
                customers = context.Customers.Where(c => c.Orders.Any(o => o.DeliveryDate <= DateTime.Today.AddDays(10))).OrderBy(c=>c.Name);
                //Beter : bepaal eerst de datum van vandaag + 10 dagen. Voer dan de query uit met de berekende datum
                DateTime date = DateTime.Today.AddDays(10);
                customers = context.Customers.Where(c => c.Orders.Any(o => o.DeliveryDate <= date)).OrderBy(c => c.Name);
                WriteCustomers(customers);
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("\n--9. Alle klanten die een product met id 1  hebben besteld--");
                product = context.Products.FirstOrDefault(p => p.ProductId == 1);
                //Alle klanten die dit product hebben besteld
                //Oplossing 1 : ToList, HasOrdered kan niet vertaald worden naar een query
                customers = context.Customers.Include(c => c.Orders).ThenInclude(o => o.OrderLines).ThenInclude(ol => ol.Product).ToList().Where(c => c.Orders.Any(o => o.HasOrdered(product)));
                WriteCustomers(customers);


                //Oplossing 2 : overloop alle klanten met foreach. Voor elke klant overloop je dan alle orders met een foreach, maar een klant kan nu meerdere keren voorkomen
                foreach (Customer c in context.Customers.Include(c => c.Orders).ThenInclude(o => o.OrderLines).ThenInclude(ol => ol.Product))
                    foreach (Order o in c.Orders)
                        if (o.HasOrdered(product))
                            Console.WriteLine(c.Name + "  " + c.FirstName);
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("\n--10. Alle klanten met orders, met vermelding van aantal orders. Maak gebruik van een anoniem type--");
                var customers2 =
                    context.Customers
                    .Where(c => c.Orders.Any())
                    .Select(c => new { c.Name, Orders = c.Orders.Count() });

                foreach (var c in customers2)
                    Console.WriteLine(c.Name + " " + c.Orders);
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("\n--11. Pas de naam aan van klant student5, in je eigen naam en voornaam--");
                customer = context.Customers.SingleOrDefault(c => c.CustomerName == "student5");
                customer.Name = "MijnNaam";
                customer.FirstName = "MijnVoornaam";
                context.SaveChanges();
                customers = context.Customers;
                WriteCustomers(customers);
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("\n--12A. Verwijder de eerste klant (in alfabetische volgorde van customername) zonder orders--");
                customer = context.Customers.FirstOrDefault(c => !c.Orders.Any());
                context.Customers.Remove(customer);
                context.SaveChanges();
                customers = context.Customers;
                WriteCustomers(context.Customers);
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("\n--13. Maak een nieuw product training, prijs 80, aan die behoort tot de categorie soccer en watersports en print na toevoeging aan de database het productid af--");
                Product training = new Product("training", 80, "Adidas Performance CONDIVO 14");
               Category soccer = context.Categories.SingleOrDefault(c => c.Name == "Soccer");
               Category watersports = context.Categories.SingleOrDefault(c => c.Name == "WaterSports");
                soccer.AddProduct(training);
                watersports.AddProduct(training);
                context.SaveChanges();

                Console.WriteLine(soccer.FindProduct("training")?.ProductId);


                Console.WriteLine("\n--14. Product training behoort niet langer tot de category soccer. Ga na of CategoryProduct ook verwijderd wordt-");
                if (training != null)
                    soccer.RemoveProduct(training);
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Console.WriteLine("\n--15. Probeer de stad Gent te verwijderen. Waarom lukt dit niet?--");
                City city = context.Cities.FirstOrDefault(c => c.Name == "Gent");
                try
                {
                    context.Cities.Remove(city);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Verwijderen Gent faalt. Reason : {ex.Message}");
                }
            }
            Console.ReadKey();
        }

        private static void WriteOrder(Order o)
        {
            if (o != null)
                Console.WriteLine($"{o.DeliveryDate} {o.OrderDate} {o.Total}");
        }


        private static void WriteCustomers(IEnumerable<Customer> customers)
        {
            customers.ToList().ForEach(c => Console.WriteLine($"{c.Name} {c.FirstName}"));
        }

        private static void WriteProducts(IEnumerable<Product> products)
        {
            products.ToList().ForEach(p => Console.WriteLine($"{p.ProductId} {p.Name} {p.Price:0.00}"));
        }
    }
}
