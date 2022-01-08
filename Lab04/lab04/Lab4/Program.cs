using System;

class Program
{
    static MyLinkedList myLinkedList = new MyLinkedList();

    class DLNode
    {
        public int data;
        public DLNode? prev;
        public DLNode? next;

        public DLNode(int data)
        {
            this.data = data;
        }       
    }
    class MyLinkedList
    {
        public DLNode? head;

        public void AddFirst(int data)
        {
            DLNode newNode = new DLNode(data);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                newNode.next = head;
                head.prev = newNode;
                head = newNode;

            }
        }

        public void AddLast(int data)
        {
            DLNode? last = getLastNode();
            if (last == null) { 
                AddFirst(data);
            }
            else
            {
                DLNode newNode = new DLNode(data);
                last.next = newNode;
                newNode.prev = last;
            }            
        }

        public bool AddAtPosition(int data, int pos)
        {
            if (pos == 1)
            {
                AddFirst(data);
                return true;
            }
            else
            {
                DLNode? node = getNodeAtPosition(pos);
                if (node == null)
                {
                    int count = getCount();
                    if (pos == count+1) 
                    {
                        AddLast(data);
                        return true;
                    } else 
                    {
                        return false;
                    }
                    
                }
                else
                {
                    DLNode newNode = new DLNode(data);
                    DLNode? prev = node.prev;

                    if (prev != null)
                    {
                        prev.next = newNode;
                        newNode.prev = prev;
                        node.prev = newNode;
                        newNode.next = node;
                    }
                    else
                    {
                        throw new Exception("List Is Broken");//we check if first position at the beginning of method so if no prev - list is broken
                    }                    
                    
                    return true;
                }
            }
        }

        public bool DeleteFirst()
        {      
            bool result = false;

            if (head != null)
            {
                DLNode? next = head.next;
                if (next != null)
                {
                    next.prev = null;
                }
                head = next;

                result = true;
            }

            return result;
        }

        public bool DeleteLast()
        {
            bool result = false;

            DLNode? last = getLastNode();
            if (last != null)
            {
                DLNode? prev = last.prev;
                if(prev == null)
                {
                    head = null;
                }
                else
                {
                    prev.next = null;
                }
                result = true;
            }

            return result;
        }

        public bool DeleteAtPosition(int pos)
        {
            bool result = false;

            DLNode? node = getNodeAtPosition(pos);
            if (node != null)            
            {
                DLNode? prev = node.prev;
                DLNode? next = node.next;

                if (prev == null)
                {
                    result = DeleteFirst();
                }
                else
                {
                    if (next == null)
                    {
                       result = DeleteLast();
                    }
                    else  // next != null && prev != null so it's middle
                    {
                        prev.next = next;
                        next.prev = prev;
                        result = true;
                    }
                }
            }

            return result;
        }

        public void AddByTask(int data)
        {
            DLNode node = new DLNode(data);

            if (head == null)
            {
                head = node;
            } 
            else
            {
                if (data > 0 && data < 99)
                {
                    DLNode? next = head.next;
                    head.next = node;
                    if (next != null)
                    {
                        next.prev = node;
                    }
                    node.next = next;
                    node.prev = head;

                }
                else
                {
                    DLNode last = getLastNode()!; // we checked if head != null so we definitely have a tail
                    DLNode? prev = last.prev; 
                    if (prev == null)
                    {
                        head = node;
                    }
                    else
                    {
                        prev.next = node;                        
                    }
                    last.prev = node;
                    node.next = last;

                }
            }            
        }

        public int getCount()
        {
            int count = 0;
            DLNode? node = head;
            if(node!=null)
            {
                do
                {
                    count++;
                    node = node.next;
                } while (node != null);
            }
            return count;
        }

        private DLNode? getLastNode()
        {
            DLNode? node = head;

            if (node == null)
            {
                return null;
            }
            else
            {
                while (node.next != null)                    
                {
                    node = node.next;   
                }
                return node;
            }
        }
        private DLNode? getNodeAtPosition(int pos)
        {
            DLNode? node = head;

            if (node == null)
            {
                return null;
            }
            else
            {
                if(pos == 1)
                {
                    return node;
                }
                else
                {
                    for (int i = 1; i < pos; i++)
                    {
                        node = node.next;
                        if (node == null)
                        {
                            return null;
                        }
                    }
                    return node;
                }
                
            }
        }

        public void Print()
        {
            DLNode? node = head;

            Console.Write("Список: ");
            if (node == null)
            {
                Console.Write("пустой");
            }
            else
            {
                while (node != null)
                {       
                    Console.Write(node.data); 
                    node = node.next;
                    if (node != null)
                    {
                        Console.Write(", ");
                    }
                }
            }
        }

    }  

    static void Main(string[] args)
    {
        menu();
    }



    static void menu()
    {
        bool needExit = false;

        do
        {
            Console.Clear();
            int m = -1;

            myLinkedList.Print();
            Console.WriteLine();
            Console.WriteLine("Меню:");


            Console.WriteLine("1 - Добавить в голову");
            Console.WriteLine("2 - Добавить в хвост");
            Console.WriteLine("3 - Добавить в позицию");
            Console.WriteLine("4 - Удалить голову");
            Console.WriteLine("5 - Удалить хвост");
            Console.WriteLine("6 - Удалить по идексу");
            Console.WriteLine("7 - Печать");
            Console.WriteLine();
            Console.WriteLine("8 - Добавить после головы если (0;99) иначе перед хвостом (задание)");
            Console.WriteLine();
            Console.WriteLine("9 - Добавить 10 подряд");

            Console.WriteLine("0 - выход");



            
            try
            {
                m = Convert.ToInt32((Console.ReadLine()));
            }
            catch (Exception)
            {                
                continue;
            }
            if (m >= 0 && m < 10)
            {
                switch (m)
                {
                    case 1:
                        addFirst();
                        break;
                    case 2:
                        addLast();
                        break;
                    case 3:
                        addAtPosition();
                        break;
                    case 4:
                        delFirst();
                        break;
                    case 5:
                        delLast();
                        break;
                    case 6:
                        delAtPosition();
                        break;
                    case 7:
                        print();
                        break;
                    case 8:
                        doTask();
                        break;
                    case 9:
                        add10();
                        break;
                    case 0:
                        needExit = true;
                        break;
                }
            }
        } while (!needExit);

    }

    private static void addFirst()
    {
        Console.WriteLine("Введите целое число для добавления в начало списка:");

        int data = readInt();
        
        myLinkedList.AddFirst(data);
    }
    
    private static void addLast()
    {
        Console.WriteLine("Введите целое число для добавления в конец списка:");

        int data = readInt();

        myLinkedList.AddLast(data);
    }
    private static void addAtPosition()
    {
        Console.WriteLine("Введите целое число для добавления в список:");

        int data = readInt();

        Console.WriteLine("Введите позицию на которую поместить элемент:");

        int pos = readPositiveInt();

        bool success = myLinkedList.AddAtPosition(data,pos);
        if (!success)
        {
            printOperationError();
        }
    }
    private static void delFirst()
    {
        bool success = myLinkedList.DeleteFirst();
        if (!success)
        {
            printOperationError();
        }
    }
    private static void delLast()
    {
        bool success = myLinkedList.DeleteLast();
        if (!success)
        {
            printOperationError();
        }
    }
    private static void delAtPosition()
    {
        Console.WriteLine("Введите позицию с которой удалить элемент:");

        int pos = readPositiveInt();

        bool success = myLinkedList.DeleteAtPosition(pos);
        if (!success)
        {
            printOperationError();
        }
    }
    private static void print()
    {
        myLinkedList.Print();
        Console.WriteLine();
        Console.WriteLine("Нажмите любую кнопку для продолжения");
        
        Console.ReadKey();
    }
    private static void doTask()
    {
        Console.WriteLine("Введите целое число для добавления после головы если (0; 99) иначе перед хвостом:");

        int data = readInt();

        myLinkedList.AddByTask(data);
    }
    private static void add10()
    {
        for (int i = 0; i < 10; i++)
        {
            myLinkedList.AddLast(i+1);
        }
    }

    private static int readInt()
    {
        bool inputed = false;
        int data = 0;
        do
        {
            try
            {
                data = Convert.ToInt32((Console.ReadLine()));
            }
            catch (Exception)
            {
                Console.WriteLine("Введите целое число:");
                continue;
            }
            inputed = true;
        } while (!inputed);
        return data;
    }

    private static int readPositiveInt()
    {
        bool inputed = false;
        int data = 0;
        do
        {
            try
            {
                data = Convert.ToInt32((Console.ReadLine()));
            }
            catch (Exception)
            {
                Console.WriteLine("Введите целое положительное число:");
                continue;
            }

            if (data < 1)
            {
                Console.WriteLine("Введите целое положительное число:");
                continue;
            }

            inputed = true;
        } while (!inputed);
        return data;
    }

    private static void printOperationError()
    {
        ConsoleColor oldColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;

        Console.WriteLine("Операция не может быть выполнена! :(");
        Console.WriteLine("Нажмите любую кнопку для продолжения");

        Console.ForegroundColor=oldColor;

        Console.ReadKey();
    }

}