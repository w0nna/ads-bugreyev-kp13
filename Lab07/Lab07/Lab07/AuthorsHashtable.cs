using System;
using System.Collections.Generic;
using System.Numerics;

namespace lab7
{
    class KeyAuthor
    {
        public string Name;
        public KeyAuthor(string authorName)
        {
            Name = authorName;
        }
    }
    class ValueAuthor
    {
        public LinkedList<string> Books;
        public ValueAuthor(LinkedList<string> booksList)
        {
            Books = booksList;
        }
        public string PrintBooks()
        {
            string temp = "";
            foreach (var bookTitle in Books)
            {
                temp += $"{bookTitle}, ";
            }
            temp = temp.Substring(0, temp.Length - 2);
            return temp;
        }
    }
    class EntryAuthor
    {
        public KeyAuthor key;
        public ValueAuthor value;
        public EntryAuthor next;
        public EntryAuthor(KeyAuthor key, ValueAuthor value)
        {
            this.key = key;
            this.value = value;
            this.next = null;
        }
        public void PrintEntry()
        {
            Console.WriteLine($"Автор: {key.Name}. Написані книги: {value.PrintBooks()}.");
        }
    }

    class AuthorsHashtable
    {
        public EntryAuthor[] table;
        int capacity;
        int size;
        double MaxLoadness = 0.6;
        public AuthorsHashtable(int length)
        {
            size = 0;
            table = new EntryAuthor[length];
            capacity = length;
        }

        public int GetHash(string key)
        {
            key = key.ToLower();
            string hashAlphabet = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя ";

            BigInteger hash = 0;
            for (int i = 0; i < key.Length; i++)
            {
                hash += (BigInteger)((hashAlphabet.IndexOf(key[i]) + 1) * Math.Pow(23, key.Length - 1 - i));
            }
            return (int)(hash % table.Length);
        }

        public void InsertEntry(KeyAuthor key, ValueAuthor value, string newBookTitle)
        {
            EntryAuthor newEntry = new EntryAuthor(key, value);

            int hash = GetHash(key.Name);
            EntryAuthor current = table[hash];

            if (current == null)
            {
                newEntry.value.Books.AddLast(newBookTitle);
                table[hash] = newEntry;
                size++;
            }
            else
            {
                bool update = false;

                while (current != null)
                {
                    if (current.key.Name == key.Name)
                    {
                        update = true;
                        current.value.Books.AddLast(newBookTitle);
                        return;
                    }
                    current = current.next;
                }

                if (!update)
                {
                    current = table[hash];
                    table[hash] = newEntry;
                    table[hash].next = current;
                }
            }
            double newLoadness = (double)size / capacity;
            if (newLoadness >= MaxLoadness)
            {
                capacity *= 2;
                Console.WriteLine("Таблиця авторів переповнена. Старт перегешування...");
                Rehash();
            }
        }

        public void Print()
        {
            foreach (EntryAuthor item in table)
            {
                var temp = item;
                if (temp != null)
                {
                    temp.PrintEntry();
                    while (temp.next != null)
                    {
                        temp.next.PrintEntry();
                        temp = temp.next;
                    }
                }

            }
        }

        void Rehash()
        {
            size = 0;
            EntryAuthor[] oldTable = table;
            table = new EntryAuthor[oldTable.Length * 2];

            foreach (EntryAuthor currentEntry in oldTable)
            {
                EntryAuthor tempEntry = currentEntry;
                if (tempEntry != null)
                {
                    InsertEntry(tempEntry.key, tempEntry.value, null);

                    while (tempEntry.next != null)
                    {
                        tempEntry = tempEntry.next;
                        InsertEntry(tempEntry.key, tempEntry.value, null);
                    }
                }
            }

            Console.WriteLine("Таблицю перегешовано успішно!");
        }

        public void FindEntry(KeyAuthor key)
        {
            int hash = GetHash(key.Name);
            EntryAuthor currentEntry = table[hash];

            while (currentEntry != null)
            {
                if (currentEntry.key.Name == key.Name)
                {
                    currentEntry.PrintEntry();
                    return;
                }
                else
                {
                    currentEntry = currentEntry.next;
                }
            }
            Console.WriteLine("Не знайдено такого автора!");
        }
        public void RemoveEntry(KeyAuthor key)
        {
            int hash = GetHash(key.Name);
            EntryAuthor head = table[hash];

            if (head != null)
            {
                if (head.key.Name == key.Name)
                {
                    if (head.next != null)
                    {
                        table[hash] = head.next;
                        return;
                    }
                    else
                    {
                        table[hash] = null;
                        size--;
                    }
                    Console.WriteLine("Автора видалено успішно!");
                    return;
                }
                else
                {
                    EntryAuthor previousAuthor = table[hash];
                    EntryAuthor current = table[hash].next;

                    while (current != null)
                    {
                        if (current.key.Name == key.Name)
                        {
                            previousAuthor.next = current.next;
                            Console.WriteLine("Автора видалено успішно!");
                            return;
                        }
                        else
                        {
                            previousAuthor = current;
                            current = current.next;
                        }
                    }
                }
            }
            Console.WriteLine("Не знайдено такого автора! Видалення неможливе");
            return;
        }
    }
}
