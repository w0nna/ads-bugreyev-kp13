using System;
using static System.Console;
using static System.Convert;


class Programm
{
    static int n;
    static int[,] mas;
    static int[] masSorted;
    static int what;
    static bool IsCorrect = false;

    static void Main()
    {
        do
        {
            Write("Впишите N: ");
            IsCorrect = false;
            n = ToInt16(ReadLine());
            if(n > 0)
            {
                IsCorrect = true;
            }
            Clear();
        }
        while (IsCorrect == false);
        if (n >= 4)
        {
            do
            {
                Write("[0 - Нет, 1 - Да] Сгенерировать 1 и больше чисел со 100% содержанием в начале - 2022: ");
                IsCorrect = false;
                what = ToInt16(ReadLine());
                if (what == 0 || what == 1)
                {
                    IsCorrect = true;
                }
                Clear();
            }
            while (IsCorrect == false);
           

            if (what == 1)
            {
                GeneratorWith2022();
            }
            else
            {
                GeneratorDef();
            }
        }
        else
        {
            GeneratorWith2022();
        }
        
    }
    static void GeneratorDef()
    {
        mas = new int[n,16];
        Random rnd = new Random();
        for (int i = 0; i < n; i++)
        {
            for(int x =0; x < 16; x++)
            {
                mas[i, x] = rnd.Next(0, 9);
            }

        }
    }

    static void GeneratorWith2022()
    {
        WriteLine("Номера на сортировку.");
        mas = new int[n, 16];
        int f;
        Random rnd = new Random();
        if (n >= 4)
        {
            f = rnd.Next(1, n - 2);
        }
        else
        {
            f = 1;
        }

        int[] x2022 = new int[f];
        int RandRow = rnd.Next(0, n);
        for (int i = 0; i < n; i++)
        {
            for (int x = 0; x < 16; x++)
            {
                mas[i, x] = rnd.Next(0, 9);
            }

        }
        for (int i = 0; i < f; i++)
        {
            for (int x = 0; x < 16; x++)
            {
                if (x == 0 || x == 1 || x == 2 || x == 3)
                {
                    switch (x)
                    {
                        case 0:
                            mas[RandRow, x] = 2;
                            break;
                        case 1:
                            mas[RandRow, x] = 0;
                            break;
                        case 2:
                            mas[RandRow, x] = 2;
                            break;
                        case 3:
                            mas[RandRow, x] = 2;
                            break;

                    }

                }


            }
            x2022[i] = RandRow;
            
            RandRow = rnd.Next(0, n);
        }
        for(int i =0; i<f; i++)
        {
            Write(x2022[i] + "; \n");
        }
       

        int v = 0;
        for (int i = 0; i < n; i++)
        {
            for (int x = 0; x < 16; x++)
            {
                Write(mas[i, x]);
            }
            WriteLine(" ");
        }
        //Sort
        v = Sorted(f, x2022, v);

    }

    private static int Sorted(int f, int[] x2022, int v)
    {
        int[,] masSorted2022 = new int[f, 16];
        int[,] masSorted = new int[n - f, 16];
        int[] ysorted = new int[n-f]; 
        for (int i = 0; i < n; i++)
        {
            bool No2022 = false;


            for (int p = 0; p < f; p++)
            {
                if (i != x2022[p])
                {
                    No2022 = true;
                }
            }
            if (No2022 == true)
            {
                if (!(i + 1 >= n))
                {
                    v = SortMain(f, v, masSorted, i);
                }

            }
            else
            {
                if (f != 1)
                {
                    continue;
                }
                else
                {
                    for (int p = 0; p < f; p++)
                    {
                        for (int r = 0; r < 16; r++)
                        {
                            masSorted2022[p, r] = mas[x2022[p], r];
                        }
                    }
                }

            }
        }
        WriteRez(f, x2022, masSorted);

        return v;
    }

    private static int SortMain(int f, int v, int[,] masSorted, int i)
    {
        int z = 0;
        do
        {
            v++;
        }
        while ((!(v >= 16 && i + 1 >= n) && mas[i, v] == mas[i + 1, v]));

        if (!(v >= 16 && i + 1 >= n))
        {
            if (mas[i, v] < mas[i + 1, v])
            {

                for (int r = 0; r <= 15; r++)
                {
                    if (!(z + 1 >= n - f && i + 1 >= n))
                    {
                        masSorted[z, r] = mas[i, r];
                        
                       
                    }
                }
                if (!(z >= n - f))
                {
                    z++;
                }

            }
            else
            {
                MensheChemSledMassiv(f, masSorted, i ,v, z);
            }
        }

        return v;
    }

    private static void MensheChemSledMassiv(int f, int[,] masSorted, int i, int v , int z)
    {
        for (int r = 0; r <= 15; r++)
        {
            if (!(z + 1 >= n - f && i + 1 >= n))
            {
                mas[i + 1, r] = mas[i, r];


            }
        }
        if (!(z >= n - f))
        {
            z++;
        }
    }

    private static void WriteRez(int f, int[] x2022, int[,] masSorted)
    {
        for (int p = 0; p < (n - f); p++)
        {
            for (int r = 0; r < 16; r++)
            {
                Write(masSorted[p, r]);
            }
            WriteLine("");
        }
    }
}