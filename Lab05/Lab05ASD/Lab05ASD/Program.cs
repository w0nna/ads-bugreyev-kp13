using System;
using static System.Console;
using static System.Convert;

class Program
{
    static void Main(string[] args)
    {
        int what;

        Write("Впишите N: ");
        bool IsCorrect = false;
        int n = ToInt16(ReadLine());
        if (n > 0)
            IsCorrect = true;
        Clear();

        while (IsCorrect == false) ;
        if (n >= 4)
        {
            do
            {
                Write("[0 - Нет, 1 - Да] Сгенерировать 1 и больше чисел со 100% содержанием в начале - 2022: ");
                IsCorrect = false;
                what = ToInt16(ReadLine());
                if (what == 0 || what == 1)
                    IsCorrect = true;
                Clear();
            }
            while (IsCorrect == false);


            if (what == 1)
                DoWith2022(n);
            else
                DoWithOut2022(n);
        }
        else
            DoWithOut2022(n);
    }
    static void DoWith2022(int n)
    {
        Int64 temp;
        int schetchik = 0;
        int schetchik2 = 0;
        Random rnd = new Random();
        Int64[] mainMass = new Int64[n];
        int RandRow = rnd.Next(1, n - 1);
        Int64[] massWithout = new Int64[n - RandRow];
        Int64[] mass2022 = new long[RandRow];
        Int64[] massSorted = new long[n];
        int[] y2022 = new int[RandRow];

        WriteLine("-----GENERATING----\n");
        for (int i = 0; i < n; i++)
        {
            mainMass[i] = rnd.NextInt64(1000000000000000, 9999999999999999);
            WriteLine(mainMass[i]);
        }
        WriteLine("\n--------\ny ryads with 2022:[no sorted]");

        for (int i = 0; i < RandRow; i++)
        {
            var r = rnd.Next(0, n);
            if (!(y2022.Contains(r)))
                y2022[i] = r;
            else
                i--;
        }
        for (int i = 0; i < RandRow; i++)
            Write(y2022[i]);



        WriteLine("\ny ryads with 2022:[sorted]");
        Array.Sort(y2022);
        for (int i = 0; i < RandRow; i++)
        {
            Write(y2022[i]);
        }

        WriteLine("\n--------\nMain Mass with 2022\n");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < RandRow; j++)
                if (i == y2022[j])
                {
                    mainMass[i] = rnd.NextInt64(2022000000000000, 2022999999999999);
                }
            WriteLine(mainMass[i]);
        }

        for (int i = 0; i < n; i++)
        {
            temp = mainMass[i] / Convert.ToInt64(Math.Pow(10, 12));
            if (temp == 2022)
            {
                mass2022[schetchik2] = mainMass[i];
                schetchik2++;
            }
            else
            {
                massWithout[schetchik] = mainMass[i];
                schetchik++;
            }
        }
        WriteLine("\n----Massive not sorted----\n");

        for (int i = 0; i < RandRow; i++)
        {
            WriteLine(mass2022[i]);
        }
        WriteLine("\n----");
        for (int i = 0; i < n - RandRow; i++)
        {
            WriteLine(massWithout[i]);
        }
        if (massWithout.Length != 1)
            for (int z = 0; z < n; z++)
            {
                for (int j = 0; j < n - RandRow; j++)
                {
                    bool IsOkey = false;
                    int i = 16 - 1;
                    long temp2 = 0;

                    do
                    {
                        temp = massWithout[j] / Convert.ToInt64(Math.Pow(10, i));
                        if (!(j + 1 >= n - RandRow))
                            temp2 = massWithout[j + 1] / Convert.ToInt64(Math.Pow(10, i));
                        else
                        {
                            j++;
                            break;
                        }

                        if (temp > temp2)
                        {
                            if (!(j + 1 >= n - RandRow))
                            {
                                temp = massWithout[j + 1];
                                massWithout[j + 1] = massWithout[j];
                                massWithout[j] = temp;
                                j++;
                            }
                            else
                                j = 0;

                        }
                        else if (temp == temp2)
                            i--;
                        else
                            IsOkey = true;
                    }
                    while (IsOkey == false);

                }
            }
        WriteLine("\n----SORTED----\n");
        for (int i = 0; i < n - RandRow; i++)
        {
            WriteLine(massWithout[i]);
        }
        WriteLine(" ");
        if (y2022.Length != 1)
            for (int z = 0; z < n; z++)
            {
                for (int j = 0; j < RandRow; j++)
                {
                    bool IsOkey = false;
                    int i = 16 - 5;
                    long temp2 = 0;

                    do
                    {
                        temp = (mass2022[j] / Convert.ToInt64(Math.Pow(10, i))) % 10;
                        if (!(j + 1 >= RandRow))
                            temp2 = mass2022[j + 1] / (Convert.ToInt64(Math.Pow(10, i))) % 10;
                        else
                        {
                            if (!(j + 1 >= RandRow))
                            {
                                j++;
                                break;
                            }

                        }

                        if (temp < temp2)
                        {
                            if (!(j + 1 >= RandRow))
                            {
                                temp = mass2022[j + 1];
                                mass2022[j + 1] = mass2022[j];
                                mass2022[j] = temp;
                                j++;
                            }
                            else
                                j = 0;

                        }
                        else if (temp == temp2)
                        {
                            if (!(i <= 0))
                            {
                                i--;
                            }
                        }
                        else
                            IsOkey = true;
                    }
                    while (IsOkey == false);
                }
            }





        for (int i = 0; i < RandRow; i++)
        {
            WriteLine(mass2022[i]);
        }

        WriteLine("\n----RESULT----\n");


        for (int i = 0; i < RandRow; i++)
        {
            massSorted[y2022[i]] = mass2022[i];
        }

        int j1 = 0;
        for (int x = 0; x < n - RandRow; x++)
        {
            int i = 16 - 4;
            bool isOkey;
            temp = massWithout[x] / Convert.ToInt64(Math.Pow(10, i));
            do
            {
                isOkey = false;
                if (!(j1 > n))
                {
                    for (int y = 0; y < RandRow; y++)
                    {
                        if (j1 == y2022[y])
                        {
                            isOkey = true;
                        }
                    }
                    if (isOkey == false)
                    {
                        massSorted[j1] = massWithout[x];
                        j1++;
                        continue;
                    }
                    else
                    {
                        j1++;
                        continue;
                    }
                }
            }
            while (isOkey == true);



        }
        for (int i = 0; i < n; i++)
        {
            WriteLine(massSorted[i]);
        }

    }

    static void DoWithOut2022(int n)
    {
        Int64 temp;
        int schetchik = 0;
        int schetchik2 = 0;
        Random rnd = new Random();
        Int64[] mainMass = new Int64[n];
        int RandRow = 0;
        Int64[] massSorted = new long[n];
        WriteLine("-----GENERATING----\n");
        for (int i = 0; i < n; i++)
        {
            mainMass[i] = rnd.NextInt64(1000000000000000, 9999999999999999);
            WriteLine(mainMass[i]);
        }

        for (int i = 0; i < n; i++)
        {
            temp = mainMass[i] / Convert.ToInt64(Math.Pow(10, 12));
            if (temp == 2022)
            {
                RandRow++;
            }
        }
        int[] y2022 = new int[RandRow];
        if (RandRow != 0)
        {
            for (int i = 0; i < n; i++)
            {

                temp = mainMass[i] / Convert.ToInt64(Math.Pow(10, 12));
                if (temp == 2022)
                {
                    y2022[schetchik] = i;
                    schetchik++;
                }

            }
        }
        schetchik = 0;
        schetchik2 = 0;
        Int64[] mass2022 = new long[RandRow];
        Int64[] massWithout = new Int64[n - RandRow];

        for (int i = 0; i < n - RandRow; i++)
        {
            temp = mainMass[i] / Convert.ToInt64(Math.Pow(10, 12));
            if (temp != 2022)
            {
                massWithout[schetchik] = mainMass[i];
                schetchik++;
            }
            else
            {
                mass2022[schetchik2] = mainMass[i];
                schetchik2++;
            }

        }



        WriteLine("\n----Massive not sorted----\n");
        if (RandRow != 0)
        {
            for (int i = 0; i < RandRow; i++)
            {
                WriteLine(mass2022[i]);
            }

            WriteLine("\n----");
        }

        for (int i = 0; i < n - RandRow; i++)
        {
            WriteLine(massWithout[i]);
        }
        for (int z = 0; z < n; z++)
        {
            for (int j = 0; j < n - RandRow; j++)
            {
                bool IsOkey = false;
                int i = 16 - 1;
                long temp2 = 0;

                do
                {
                    temp = massWithout[j] / Convert.ToInt64(Math.Pow(10, i));
                    if (!(j + 1 >= n - RandRow))
                        temp2 = massWithout[j + 1] / Convert.ToInt64(Math.Pow(10, i));
                    else
                    {
                        j++;
                        break;
                    }

                    if (temp > temp2)
                    {
                        if (!(j + 1 >= n - RandRow))
                        {
                            temp = massWithout[j + 1];
                            massWithout[j + 1] = massWithout[j];
                            massWithout[j] = temp;
                            j++;
                        }
                        else
                            j = 0;

                    }
                    else if (temp == temp2)
                        i--;
                    else
                        IsOkey = true;
                }
                while (IsOkey == false);

            }
        }
        if (RandRow > 0)
        {
            for (int z = 0; z < n; z++)
            {
                for (int j = 0; j < RandRow; j++)
                {
                    bool IsOkey = false;
                    int i = 16 - 5;
                    long temp2 = 0;

                    do
                    {
                        temp = (mass2022[j] / Convert.ToInt64(Math.Pow(10, i))) % 10;
                        if (!(j + 1 >= RandRow))
                            temp2 = mass2022[j + 1] / (Convert.ToInt64(Math.Pow(10, i))) % 10;
                        else
                        {
                            if (!(j + 1 >= RandRow))
                            {
                                j++;
                                break;
                            }

                        }

                        if (temp < temp2)
                        {
                            if (!(j + 1 >= RandRow))
                            {
                                temp = mass2022[j + 1];
                                mass2022[j + 1] = mass2022[j];
                                mass2022[j] = temp;
                                j++;
                            }
                            else
                                j = 0;

                        }
                        else if (temp == temp2)
                        {
                            if (!(i <= 0))
                            {
                                i--;
                            }
                        }
                        else
                            IsOkey = true;
                    }
                    while (IsOkey == false);
                }
            }
        }
        WriteLine("\n----RESULT----\n");


        for (int i = 0; i < RandRow; i++)
        {
            massSorted[y2022[i]] = mass2022[i];
        }

        int j1 = 0;
        for (int x = 0; x < n - RandRow; x++)
        {
            int i = 16 - 4;
            bool isOkey;
            temp = massWithout[x] / Convert.ToInt64(Math.Pow(10, i));
            do
            {
                isOkey = false;
                if (!(j1 > n))
                {
                    for (int y = 0; y < RandRow; y++)
                    {
                        if (j1 == y2022[y])
                        {
                            isOkey = true;
                        }
                    }
                    if (isOkey == false)
                    {
                        massSorted[j1] = massWithout[x];
                        j1++;
                        continue;
                    }
                    else
                    {
                        j1++;
                        continue;
                    }
                }
            }
            while (isOkey == true);



        }
        for (int i = 0; i < n; i++)
        {
            WriteLine(massSorted[i]);
        }



    }


}





