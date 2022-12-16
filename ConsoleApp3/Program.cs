using System;
using System.Globalization;

public static class Program
{
    public static void Main()
    {
        var pokups = new Pokup[5];
        for (var i = 0; i < pokups.Length; i++)
        {
            Console.WriteLine($"Покупка №{i + 1}");
            pokups[i] = new Pokup(
                client: ReadInput("Название товара: "),
                shop: ReadInput("Количество: "),
                cache: ReadInput("Цена: "),

                weight: ReadNumber("Сколько вы дали кассиру: ")
            );
        }

        Console.WriteLine($"Купленные товары:");
        Console.WriteLine(string.Join("\n", pokups));
    }

    public static ulong ReadNumber(string title)
    {
        string input;
        ulong result;
        do
        {
            input = ReadInput(title);
        }
        while (!ulong.TryParse(input, out result));
        return result;
    }

    public static DateTime ReadDateTime(string title)
    {
        string input;
        DateTime result;
        do
        {
            input = ReadInput(title);
        }
        while (!DateTime.TryParseExact(input, @"dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out result));
        return result;
    }

    public static string ReadInput(string title)
    {
        Console.Write(title);
        return Console.ReadLine();
    }

    public readonly struct Pokup
    {
        public Pokup(string client, string shop, ulong weight, string cache) : this()
        {
            Client = client;
            Shop = shop;
            Weight = weight;
        }

        public Pokup(string client, string shop, DateTime purchaseDate, ulong weight)
        {
            Client = client;
            Shop = shop;
            PurchaseDate = purchaseDate;
            Weight = weight;
        }

        public string Client { get; }

        public string Shop { get; }

        public DateTime PurchaseDate { get; }

        public ulong Weight { get; }

        public override string ToString() =>
            $"Покупка на клиента {Client} в магазине {Shop} чек {Weight.Normalize()}. {PurchaseDate}";
    }
}

public static class WeightNormalizer
{
    public static string Normalize(this ulong weight) => weight >= 1000
        ? $"{weight / 1000} руб {weight % 1000} руб"
        : $"{weight} г";
}