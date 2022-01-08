using System;

class Program
{
    static Random rnd = new Random();

    static int N = -1;

    static int[] fullArray = { };
    static int[] array2n = { };
    static int[] array3n = { };

    static ConsoleColor color2 = ConsoleColor.Green;
    static ConsoleColor color3 = ConsoleColor.Red;


    static void Main(string[] args)
    {
        menu();

        generateArray();

        Console.WriteLine("Несортированный массив:");
        printArray(fullArray);
        Console.WriteLine();

        splitFullArray();

        selectionSort(array2n, false);        

        selectionSort(array3n, true);  

        joingToFullArray();

        Console.WriteLine("Сортированный массив:");
        printArray(fullArray);
        Console.WriteLine();

        Console.ReadKey();

    }

    static void menu()
    {
        do
        {
            Console.WriteLine("Введите N>0");
            try
            {
                N = Convert.ToInt32((Console.ReadLine()));
            }
            catch (Exception)
            {
                Console.WriteLine("Неверные данные");
                continue;
            }
            if (N < 1)
            {
                Console.WriteLine("Должно быть > 0");
            }

        }
        while (N < 1);

    }

    private static void generateArray()
    {
        fullArray = new int[N];
        for (int i = 0; i < N; i++)
        {
            bool generated = false;
            do
            {
                int val = rnd.Next(0, 400);
                
                if (!fullArray.Contains(val))
                {
                    fullArray[i] = val;
                    generated = true;
                }

            } while (!generated);
        }
    }

    static void splitFullArray()
    {
        int n2 = 0;
        int n3 = 0;
        for (int i = 0; i<fullArray.Length; i++)
        {
            int numbers = getNumbersCount(fullArray[i]);
            switch (numbers)
            {
                case 2: n2++; break;
                case 3: n3++; break;
            }
        }

        array2n = new int[n2];
        array3n = new int[n3];

        int i2 = 0;
        int i3 = 0;
        for (int i = 0; i < fullArray.Length; i++)
        {
            int cell = fullArray[i];
            int numbers = getNumbersCount(cell);
            switch (numbers)
            {
                case 2:
                    array2n[i2] = cell;
                    i2++; 
                    break;
                case 3:
                    array3n[i3] = cell;
                    i3++; 
                    break;
            }
        }

    }

    static void joingToFullArray()
    {        
        int i2 = 0;
        int i3 = 0;
        for (int i = 0; i < fullArray.Length; i++)
        {
            int numbers = getNumbersCount(fullArray[i]);
            switch (numbers)
            {
                case 2:
                    fullArray[i] = array2n[i2];
                    i2++;
                    break;
                case 3:
                    fullArray[i] = array3n[i3];
                    i3++;
                    break;
            }
        }

    }
    static void selectionSort(int[] array, bool asscending)
    {
        for (int i =0; i < array.Length-1; i++)
        {
            int selectedIndex = i;
            for (int j = i+1; j < array.Length; j++)
            {
                bool needSel = false;
                if (asscending)
                {
                    needSel = array[j] < array[selectedIndex];
                }
                else
                {
                    needSel = array[j] > array[selectedIndex];
                }

                if (needSel)
                {
                    selectedIndex = j;
                }
            }
            if (i!=selectedIndex)
            {
                swap(array, selectedIndex, i);
            }
        }
    } 

    static void swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];    
        array[j] = temp;
    }

    static void printArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            printCell(array[i]);
        }
    }

    static int getNumbersCount(int number)
    {
        int digits = 0;
        while (number > 0)
        {
            digits++;
            number = (int)(number / 10);
        }
        return digits;
    }

    static void printCell(int val)
    {
        ConsoleColor oldColor = Console.ForegroundColor;
        int numbers = getNumbersCount(val);
        switch (numbers)
        {
            case 2: Console.ForegroundColor = color2; break;
            case 3: Console.ForegroundColor = color3; break;
        }
        Console.Write(val+" ");
        Console.ForegroundColor = oldColor;
    }


}
