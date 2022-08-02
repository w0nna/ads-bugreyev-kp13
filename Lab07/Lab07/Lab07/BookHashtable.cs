using System;
using System.Collections.Generic;

namespace lab7
{
    class Key
    {
        public int bookID;
        public Key(int ID)
        {
            bookID = ID;
        }
    }
    class Value
    {
        public string title;
        public LinkedList<string> authors;
        public int yearOfPublishing;

        public string PrintAuthors()
        {
            string temp = "";
            foreach (var currentAuthor in authors)
            {
                temp += $"{currentAuthor}, ";
            }
            temp = temp.Substring(0, temp.Length - 2);
            return temp;
        }
        public Value(string title, LinkedList<string> authors, int yearOfPublishing)
        {
            this.title = title;
            this.authors = authors;
            this.yearOfPublishing = yearOfPublishing;
        }
    }
    class Entry
    {
        public Key key;
        public Value value;
        public Entry next;
        public Entry(Key key, Value value)
        {
            this.key = key;
            this.value = value;
            this.next = null;
        }
        public void PrintEntry()
        {
            Console.WriteLine($"ID книги: {key.bookID} , Назва книги: {value.title},  " +
                $"Рік видання: {value.yearOfPublishing}; Автори: {value.PrintAuthors()};");
        }
    }
    class Hashtable
    {
        Entry[] table;
        int capacity;
        int size;
        double MaxLoadness = 0.6;

        public Hashtable(int maxTableSize)
        {
            size = 0;
            table = new Entry[maxTableSize];

            for (int i = 0; i < table.Length; i++)
            {
                table[i] = null;
            }

            capacity = maxTableSize;
        }

        public void findAllBooks(KeyAuthor key, AuthorsHashtable authorsHashtable)
        {
            authorsHashtable.FindEntry(key);
        }
        int GetHash(Key key)
        {
            return key.bookID % table.Length;
        }

        public void Print()
        {
            foreach (Entry currentTable in table)
            {
                var tempTable = currentTable;
                if (tempTable != null)
                {
                    tempTable.PrintEntry();
                    while (tempTable.next != null)
                    {
                        tempTable.next.PrintEntry();
                        tempTable = tempTable.next;
                    }
                }
            }
        }

        void Rehash()
        {
            size = 0;
            Entry[] tempTable = table;
            table = new Entry[tempTable.Length * 2];

            foreach (Entry currentEntry in tempTable)
            {
                Entry tempEntry = currentEntry;
                if (tempEntry != null)
                {
                    InsertEntry(tempEntry.key, tempEntry.value);

                    while (tempEntry.next != null)
                    {
                        tempEntry = tempEntry.next;
                        InsertEntry(tempEntry.key, tempEntry.value);
                    }
                }
            }
            Console.WriteLine("Таблицю перегешовано успішно!");
        }

        public void FindEntry(Key key)
        {
            int hash = GetHash(key);
            Entry currentEntry = table[hash];

            while (currentEntry != null)
            {
                if (currentEntry.key.bookID == key.bookID)
                {
                    Console.WriteLine("Книгу знайдено:");
                    currentEntry.PrintEntry();
                    return;
                }
                currentEntry = currentEntry.next;
            }
            Console.WriteLine("Книгу не знайдено!");
            return;
        }

        public void RemoveEntry(Key key)
        {
            int hash = GetHash(key);
            Entry currentEntry = table[hash];

            if (currentEntry != null)
            {
                if (currentEntry.key.bookID == key.bookID)
                {
                    if (currentEntry.next != null)
                    {
                        table[hash] = currentEntry.next;
                    }
                    else
                    {
                        table[hash] = null;
                        size--;
                    }
                    Console.WriteLine("Книгу видалено успішно!");
                    return;
                }
                else
                {
                    Entry previousEntry = table[hash];
                    Entry nextEntry = table[hash].next;
                    while (nextEntry != null)
                    {
                        if (nextEntry.key.bookID == key.bookID)
                        {
                            previousEntry.next = nextEntry.next;
                            Console.WriteLine("Книгу видалено успішно!");
                            return;
                        }
                        else
                        {
                            previousEntry = nextEntry;
                            nextEntry = nextEntry.next;
                        }
                    }
                }
            }

            Console.WriteLine("Книги не існує. Видалення неможливе!");
            return;
        }

        public void InsertEntry(Key key, Value value)
        {
            Entry newEntry = new Entry(key, value);

            int hash = GetHash(key);
            Entry currentEntry = table[hash];

            if (currentEntry == null)
            {
                table[hash] = newEntry;
                size++;
            }
            else
            {
                bool update = false;
                while (currentEntry.next != null)
                {
                    if (currentEntry.key == key)
                    {
                        currentEntry.value = value;
                        update = true;
                        return;
                    }
                    currentEntry = currentEntry.next;
                }
                if (!update)
                {
                    currentEntry = table[hash];
                    table[hash] = newEntry;
                    table[hash].next = currentEntry;
                }
            }
            double newLoadness = (1.0 * size) / (capacity);

            if (newLoadness >= MaxLoadness)
            {
                capacity *= 2;
                Console.WriteLine("Таблиця переповнена. Виконується перегешування...");
                Rehash();
            }
        }
    }
}
