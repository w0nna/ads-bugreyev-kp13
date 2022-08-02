using System;
using System.Collections.Generic;
using System.Text;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                try
                {
                    ShowAllCommands();
                    string enterCommand = Console.ReadLine();
                    switch (enterCommand)
                    {
                        case "/control":
                            booksHashtable = new Hashtable(4);
                            authorsHashTable = new AuthorsHashtable(11);
                            ShowSampleWork(booksHashtable, authorsHashTable);
                            break;

                        case "/empty":
                            booksHashtable = new Hashtable(5);
                            authorsHashTable = new AuthorsHashtable(4);
                            Console.Clear();
                            Console.WriteLine("Усі таблиці видалено!");
                            break;

                        case "/remove":
                            Console.Write($"Введіть ключ-ID, щоб видалити книгу: ");
                            try
                            {
                                int deleteKey = int.Parse(Console.ReadLine());
                                booksHashtable.RemoveEntry(new Key(deleteKey));
                            }
                            catch
                            {
                                Console.WriteLine("Некоректно введені дані!");
                            }
                            break;
                        case "/find":
                            Console.Write($"Введіть ключ-ID, щоб знайти книгу: ");
                            try
                            {
                                int findKey = int.Parse(Console.ReadLine());
                                booksHashtable.FindEntry(new Key(findKey));
                            }
                            catch
                            {
                                Console.WriteLine("Некоректно введені дані!");
                            }
                            break;
                        case "/findAll":
                            Console.Write($"Введіть автора, щоб знайти усі його книги: ");
                            string tempKeyAll = Console.ReadLine();
                            booksHashtable.findAllBooks(new KeyAuthor(tempKeyAll), authorsHashTable);
                            break;
                        case "/add":
                            if (booksHashtable == null)
                            {
                                booksHashtable = new Hashtable(5);
                                authorsHashTable = new AuthorsHashtable(5);
                            }
                            Console.Write($"Введіть назву нової книги:  ");
                            string tempName = Console.ReadLine();
                            Console.Write($"Введіть рік видання книги: ");
                            try
                            {
                                int tempYear = int.Parse(Console.ReadLine());
                                if (tempYear > 2022)
                                {
                                    Console.WriteLine("Цей рік ще не наступив. Тому рік видання буде дорівнювати поточному.");
                                    tempYear = 2022;
                                }
                                if (tempYear < 0)
                                {
                                    Console.WriteLine("Відлік років починається від 0, тому рік видання буде дорівнювати нульовому.");
                                    tempYear = 0;
                                }
                                Value entry = new Value(tempName, new LinkedList<string>(), tempYear);
                                List<string> Authors = new List<string>();
                                while (true)
                                {
                                    Console.Write($"Введіть автора: чи \"/end\", щоб завершити додавання книги:  ");
                                    string tempAuthorName = Console.ReadLine();
                                    if (tempAuthorName == "/exit")
                                    {
                                        break;
                                    }
                                    if (!string.IsNullOrEmpty(tempAuthorName) && !Authors.Contains(tempAuthorName))
                                    {
                                        if (tempAuthorName.Length <= 20)
                                        {
                                            if (IsAllLetters(tempAuthorName))
                                            {
                                                entry.authors.AddLast(tempAuthorName);
                                                authorsHashTable.InsertEntry(new KeyAuthor(tempAuthorName), new ValueAuthor(new LinkedList<string>()), tempName);
                                                Authors.Add(tempAuthorName);
                                            }
                                            else { Console.WriteLine("Некоректно введені дані!"); }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ім'я занадто довге. Введіть інше...");
                                        }
                                    }
                                }
                                Key entryKey = new Key(GenerateID());
                                IDlist.Add(entryKey.bookID);
                                booksHashtable.InsertEntry(entryKey, entry);
                            }
                            catch
                            {
                                Console.WriteLine("Некоректно введені дані!");
                            }
                            break;
                        case "/help":
                            break;

                        case "/showBooks":
                            booksHashtable.Print();
                            break;

                        case "/showAuthors":
                            authorsHashTable.Print();
                            break;
                        case ("/exit"):
                            System.Environment.Exit(1);
                            break;

                        default:
                            Console.WriteLine("Неправильна команда.");
                            break;
                    }
                }
                catch
                {
                }
            }
        }
        static Random rnd = new Random();
        public static Hashtable booksHashtable;
        public static AuthorsHashtable authorsHashTable;
        public static List<int> IDlist = new List<int>();

        public static bool IsAllLetters(string stroke)
        {
            foreach (char letter in stroke)
            {
                if (!(Char.IsLetter(letter) || letter == ' '))
                {
                    return false;
                }
            }
            return true;
        }

        public static void ShowAllCommands()
        {
            Console.WriteLine("\n        Список команд");
            Console.WriteLine("/control      Контрольний приклад");
            Console.WriteLine("/add          Додати книгу");
            Console.WriteLine("/find         Знайти книгу за ключем");
            Console.WriteLine("/remove       Видалити книгу за ключем");
            Console.WriteLine("/findAll      Показати книги заданого автора");
            Console.WriteLine("/showBooks    Вивести геш-таблицю книг");
            Console.WriteLine("/showAuthors  Вивести геш-таблицю авторів");
            Console.WriteLine("/help         Вивести список команд");
            Console.WriteLine("/empty        Видалити усі таблиці");
            Console.WriteLine("/exit         Вийти з програми\n");
        }

        static int GenerateID()
        {
            while (true)
            {
                string temp = "";
                for (int i = 0; i < 6; i++)
                {
                    temp += rnd.Next(0, 10).ToString();
                }
                if (!IDlist.Contains(int.Parse(temp)))
                {
                    return int.Parse(temp);
                }
            }
        }

        static void ShowSampleWork(Hashtable books, AuthorsHashtable authors)
        {

            Console.WriteLine("Створення таблиць...");
            Value entry1 = new Value("Гаррі Поттер. Філософський камінь", new LinkedList<string>(), 2001);
            entry1.authors.AddLast("Роулінг");
            int key1 = GenerateID();
            books.InsertEntry(new Key(key1), entry1);
            authors.InsertEntry(new KeyAuthor("Роулінг"),
                new ValueAuthor(new LinkedList<string>()), "Гаррі Поттер. Філософський камінь");

            Value entry3 = new Value("Гаррі Поттер. Таємна кімната", new LinkedList<string>(), 2003);
            entry3.authors.AddLast("Роулінг");
            books.InsertEntry(new Key(GenerateID()), entry3);
            authors.InsertEntry(new KeyAuthor("Роулінг"), new ValueAuthor(new LinkedList<string>()), "Гаррі Поттер. Таємна кімната");


            Value entry4 = new Value("Кобзар", new LinkedList<string>(), 1840);
            entry4.authors.AddLast("Шевченко");
            books.InsertEntry(new Key(GenerateID()), entry4);
            authors.InsertEntry(new KeyAuthor("Шевченко"), new ValueAuthor(new LinkedList<string>()), "Кобзар");


            Value entry2 = new Value("Хіба ревуть воли, як ясла повні?", new LinkedList<string>(), 1880);
            entry2.authors.AddLast("Панас Мирний");
            entry2.authors.AddLast("Іван Рудченко");
            books.InsertEntry(new Key(GenerateID()), entry2);
            authors.InsertEntry(new KeyAuthor("Панас Мирний"), new ValueAuthor(new LinkedList<string>()), "Хіба ревуть воли, як ясла повні?");
            authors.InsertEntry(new KeyAuthor("Іван Рудченко"), new ValueAuthor(new LinkedList<string>()), "Хіба ревуть воли, як ясла повні?");


            Console.WriteLine("\nТаблиця книг:");
            books.Print();
            Console.WriteLine("\nТаблиця авторів:");
            authors.Print();

            Console.WriteLine($"\nПошук книги \"{key1}\"");
            books.FindEntry(new Key(key1));
            Console.WriteLine($"\nВидалення книги \"{key1}\"");
            books.RemoveEntry(new Key(key1));
            Console.WriteLine($"\nПошук книги \"{key1}\"");
            books.FindEntry(new Key(key1));
            Console.WriteLine("\nТаблиця книг:");
            books.Print();
        }

    }
}