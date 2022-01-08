using System;

class Program
{
    class Cell{
        public int val;
        public int n;
        public int m;
        public Cell(int val, int n, int m)
        {
            this.val = val;
            this.n = n;
            this.m = m;
        }
        public Cell(int val)
        {
            this.val = val;
            n = -1;
            m = -1;
        }

        public bool hasValidIndexes()
        {
            return n >= 0 && m >= 0;
        }
        
        public void update(int val, int n, int m)
        {
            this.val = val;
            this.n = n;
            this.m = m;
        }

    }

    static Random rnd = new Random();

    static Cell minRight = new Cell(int.MaxValue);

    static Cell minDiag = new Cell(int.MaxValue);
    static Cell maxDiag = new Cell(int.MinValue);

    static Cell maxLeft = new Cell(int.MinValue);    

    static int N, M, k = 0;

    static int[,] matrix = { };

    static int matrixGenerationMode = -1;



    static void Main(string[] args)
    {
        menu();

        generateMatrix();

        printMatrix();

        algorithm();        

        printResults();

        Console.ReadKey();
                
    }



    static void menu()
    {
        bool correctInput = false;
        do
        {
            Console.WriteLine("Введите размер матрицы M=N");
            try
            {
                M = Convert.ToInt32((Console.ReadLine()));
            }
            catch (Exception)
            {
                Console.WriteLine("Неверные данные");
                continue;
            }
            if (M > 0)
            {
                N = M;
                correctInput = true;
            } else
            {
                Console.WriteLine("Размер отрицательный!");
            }
            
        }
        while (!correctInput);

        do
        {
            Console.WriteLine("Введите k>0");
            try
            {
                k = Convert.ToInt32((Console.ReadLine()));
            }
            catch (Exception)
            {
                Console.WriteLine("Неверные данные");
                continue;
            }
            if (k <1)            
            {
                Console.WriteLine("Должно быть > 0");
            }

        }
        while (k<1);


        do
        {            
            Console.WriteLine("Сгенерировать матрицу (1 -  псевдослучайно, 2 - контрольний пример)");
            try
            {
                matrixGenerationMode = Convert.ToInt32((Console.ReadLine()));
            }
            catch (Exception)
            {
                Console.WriteLine("Неверные данные");
                continue;
            }
            if (matrixGenerationMode != 1 && matrixGenerationMode != 2)            
            {
                Console.WriteLine("Неверные данные");
            }

        }
        while (matrixGenerationMode!=1 && matrixGenerationMode!=2);
        

    }
    static void generateMatrix()
    {
       if (matrixGenerationMode == 1)
        {
            generateRandomMatrix();
        } else if (matrixGenerationMode == 2)
        {
            generateControlMatrix();
        } else
        {
            throw new Exception("Wrong matrixGenerationMode");
        }
    }

    static void generateControlMatrix()
    {
        matrix = new int[N, M];
        int nextVal = 0;
        for (int i = 0; i < N; i++)
        {
            for(int j = 0; j < M; j++)
            {
                matrix[i, j] = nextVal;
                nextVal++;
            }
        }
    }
    static void generateRandomMatrix()
    {
        matrix = new int[N, M];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                matrix[i, j] = rnd.Next();
            }
        }
    }


    static void algorithm()
    {
        algorithmRight();

        algorithmDiag();

        algorithmLeft();        
    }

    static void algorithmRight()
    {
        for (int i = 0; i < N; i++)
            if ((i+1)%2 == 1)
            {
                for (int j = M-1 ; j > i; j--)
                {
                    algorithmRightHandle(i, j);
                }
            }
            else
            {
                for (int j = i+1; j < M; j++)
                {
                    algorithmRightHandle(i, j);
                }
            }
                
    }

    static void algorithmRightHandle(int n, int m)
    {        
        int val = matrix[n, m];
        Console.Write(val+", ");

        if (val % k == 0)
        {
            checkMin(minRight, n, m, val);            
        }
        

    }

    static void algorithmDiag()
    {
        Console.Write("|||");
        for (int i = M-1; i >= 0; i--)
        {
            algorithmDiagtHandle(i, i);
        }
    }

    static void algorithmDiagtHandle(int n, int m)
    {
        int val = matrix[n, m];
        Console.Write(val + ", ");

        checkMin(minDiag, n, m, val);
        checkMax(maxDiag, n, m, val);
        
    }

    static void algorithmLeft()
    {
        Console.Write("|||");
        int koef = -1;
        int rasn = maxDiag.val - minDiag.val;
        if (rasn % 2 == 0)
        {
            koef = rasn / 2;
        }

        for (int j = 0; j < N; j++)
            if ((j + 1) % 2 == 1)
            {
                for (int i = j + 1; i < M; i++)
                {
                    algorithmLeftHandle(i, j, koef);
                }
            }
            else
            {
                for (int i = M - 1; i > j; i--)
                {
                    algorithmLeftHandle(i, j, koef);
                }
            }
    }
    static void algorithmLeftHandle(int n, int m, int koef)
    {
        int val = matrix[n, m];
        Console.Write(val + ", ");
        if (koef > 0 && val%koef == 0)
        {
            checkMax(maxLeft, n, m, val);            
        }

    }


    static void checkMin(Cell curMin, int n, int m, int val)
    {
        if (curMin.hasValidIndexes())
        {
            if (curMin.val > val)
            {
                curMin.update(val, n, m);
            }
        }
        else
        {
            curMin.update(val, n, m);
        }
    }

    static void checkMax(Cell curMax, int n, int m, int val)
    {
        if (curMax.hasValidIndexes())
        {
            if (curMax.val < val)
            {
                curMax.update(val, n, m);
            }
        }
        else
        {
            curMax.update(val, n, m);
        }
    }


    static void printMatrix()
    {
        Console.WriteLine();
        Console.WriteLine("Матрица:");
        for (int i = 0; i < N; i++)
        {
            for (int j=0; j < M; j++)
            {
                Console.Write(String.Format("{0,5:###0}", matrix[i, j])+" ");
            }
            Console.WriteLine();
        }
    }

    static  void printResults()
    {
        Console.WriteLine();
        if (minRight.hasValidIndexes()) {
            Console.WriteLine("Над главной диагональю: минимальное кратное " + k + " по индексам ["
                + minRight.n+","+minRight.m+"]");
        }
        else
        {
            Console.WriteLine("Над главной диагональю нет элементов кратных "+ k);
        }
        

        Console.WriteLine("На главной диагонали: минимальный элемент" + "по индексам ["
                + minDiag.n + "," + minDiag.m + "], максимальный ["
                + maxDiag.n + "," + maxDiag.m + "]");


        if (maxLeft.hasValidIndexes())
        {
            Console.WriteLine("Под главной диагональю: максимальный элемент удов. условию по индексам ["
                + maxLeft.n + "," + maxLeft.m + "]");
        }
        else
        {
            Console.WriteLine("Под главной диагональю нет элементов удов. условию");
        }
    }
}