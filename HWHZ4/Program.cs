using System;
using System.Linq;

class Program
{
    // Зав 1

    // Метод для отримання усіх парних чисел у масиві
    static IEnumerable<int> GetEvenNumbers(int[] n)
    {
        return n.Where(x => x % 2 == 0);
    }

    // Метод для отримання усіх непарних чисел у масиві
    static IEnumerable<int> GetOddNumbers(int[] n)
    {
        return n.Where(x => x % 2 != 0);
    }

    // Метод для перевірки, чи число є простим
    static bool IsPrime(int n)
    {
        if (n <= 1)
            return false;
        if (n == 2)
            return true;
        if (n % 2 == 0)
            return false;

        int boundary = (int)Math.Floor(Math.Sqrt(n));

        for (int i = 3; i <= boundary; i += 2)
        {
            if (n % i == 0)
                return false;
        }

        return true;
    }

    // Метод для отримання усіх простих чисел у масиві
    static IEnumerable<int> GetPrimeNumbers(int[] n)
    {
        return n.Where(IsPrime);
    }

    // Метод для отримання усіх чисел Фібоначчі в масиві
    static IEnumerable<int> GetFibonacciNumbers(int[] n)
    {
        List<int> fibonacciNumbers = new List<int>();
        foreach (int num in n)
        {
            if (IsFibonacci(num))
                fibonacciNumbers.Add(num);
        }
        return fibonacciNumbers;
    }

    // Перевірка, чи число є числом Фібоначчі
    static bool IsFibonacci(int n)
    {
        double sqrt5 = Math.Sqrt(5);
        double phi = (1 + sqrt5) / 2;

        double fib = Math.Round(Math.Pow(phi, n) / sqrt5);
        return fib == n;
    }

    // Зав 2

    // Метод для відображення поточного часу
    static void DisplayCurrentTime()
    {
        Console.WriteLine($"Поточний час: {DateTime.Now.ToShortTimeString()}");
    }

    // Метод для відображення поточної дати
    static void DisplayCurrentDate()
    {
        Console.WriteLine($"Поточна дата: {DateTime.Now.ToShortDateString()}");
    }

    // Метод для відображення поточного дня тижня
    static void DisplayCurrentDayOfWeek()
    {
        Console.WriteLine($"Поточний день тижня: {DateTime.Now.DayOfWeek}");
    }

    // Метод для підрахунку площі трикутника
    static double CalculateTriangleArea(double Length, double height)
    {
        return 0.5 * Length * height;
    }

    // Метод для підрахунку площі прямокутника
    static double CalculateRectangleArea(double length, double width)
    {
        return length * width;
    }

    class CС
    {
        public event EventHandler<double> AccountReplenished;
        public event EventHandler<double> MoneySpent;
        public event EventHandler CreditStarted;
        public event EventHandler<double> LimitReached;
        public event EventHandler PinChanged;

        public string CardNumber { get; private set; }
        public string CardHolderName { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public string Pin { get; private set; }
        public double CreditLimit { get; private set; }
        public double Balance { get; private set; }

        public CС(string cardN, string cardHN, DateTime eD, string pin, double cL)
        {
            CardNumber = cardN;
            CardHolderName = cardHN;
            ExpiryDate = eD;
            Pin = pin;
            CreditLimit = cL;
            Balance = 0;
        }

        public void ReplenishAccount(double a)
        {
            Balance += a;
            AccountReplenished?.Invoke(this, a);
        }

        public void SpendMoney(double a)
        {
            if (Balance >= a)
            {
                Balance -= a;
                MoneySpent?.Invoke(this, a);
            }
            else
            {
                Console.WriteLine("Недостатньо коштів на рахунку!");
            }
        }

        public void StartCredit()
        {
            CreditStarted?.Invoke(this, EventArgs.Empty);
        }

        public void ChangePin(string newPin)
        {
            Pin = newPin;
            PinChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    static void Main(string[] args)
    {
        // Приклади використання методів з завдання 1
        int[] n = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Console.WriteLine("Парні числа:");
        Console.WriteLine(string.Join(", ", GetEvenNumbers(n)));
        Console.WriteLine("Непарні числа:");
        Console.WriteLine(string.Join(", ", GetOddNumbers(n)));
        Console.WriteLine("Прості числа:");
        Console.WriteLine(string.Join(", ", GetPrimeNumbers(n)));
        Console.WriteLine("Числа Фібоначчі:");
        Console.WriteLine(string.Join(", ", GetFibonacciNumbers(n)));

        // Приклади використання методів з завдання 2
        Console.WriteLine();
        DisplayCurrentTime();
        DisplayCurrentDate();
        DisplayCurrentDayOfWeek();
        Console.WriteLine("Площа трикутника зі стороною 3 і висотою 4: " + CalculateTriangleArea(3, 4));
        Console.WriteLine("Площа прямокутника зі сторонами 5 і 6: " + CalculateRectangleArea(5, 6));
        CС myCard = new CС("1234567890123456", "John Doe", new DateTime(2026, 12, 31), "1234", 1000);

        // Підписка на події
        myCard.AccountReplenished += (s, a) => Console.WriteLine($"Рахунок поповнено на {a} грн.");
        myCard.MoneySpent += (s, a) => Console.WriteLine($"Знято {a} грн. з рахунку.");
        myCard.CreditStarted += (s, e) => Console.WriteLine("Старт кредитного використання.");
        myCard.LimitReached += (s, a) => Console.WriteLine($"Досягнуто ліміт в розмірі {a} грн.");
        myCard.PinChanged += (s, e) => Console.WriteLine("PIN успішно змінено.");

        // Дії з кредитною карткою
        myCard.ReplenishAccount(500); // Поповнення рахунку
        myCard.SpendMoney(200);       // Витрата коштів
        myCard.StartCredit();          // Старт кредитного використання
        myCard.SpendMoney(800);       // Витрата коштів (перевищення ліміту)
        myCard.ChangePin("5678");     // Зміна PIN

        Console.WriteLine($"Баланс: {myCard.Balance} грн.");
    }
}