using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace JobApplication
{
    class JobApp
    {
        private static int currentTask;
        public static void RunTask(int taskID)
        {
            currentTask = taskID;
            switch (taskID)
            {
                case 1:
                    palindromeCheck();
                    break;
                case 2:
                    minimalSplit();
                    break;
                case 3:
                    notContains();
                    break;
                case 4:
                    bracesCheck();
                    break;
                case 5:
                    stairClimbVariations();
                    break;
                case 6:
                    zeroAccessTime();
                    break;
                case 7:
                    ValuteConverter();
                    break;
                default:
                    break;
            }
        }
        public static void Main()
        {
            int AppID;
            Console.WriteLine("1) Palindrome Check");
            Console.WriteLine("2) Get Minimal Split");
            Console.WriteLine("3) Get Minimal Number Which Doesnt Exist In Array");
            Console.WriteLine("4) Proper Braces");
            Console.WriteLine("5) Stair Climb Variations");
            Console.WriteLine("6) O(1) Time Access");
            Console.WriteLine("7) Valute Exchange");
            AppID = Convert.ToInt32(Console.ReadLine());
            RunTask(AppID);
        }
        private static void returnToMenu()
        {
            Console.WriteLine("Would you like to return to menu? [y]/[n]");
            string answ = Console.ReadLine();
            if(answ.ToLower() == "y")
            {
                Console.Clear();
                Main();
            }
            else if(answ.ToLower() == "n")
            {
                RunTask(currentTask);
            }
        }
        private static void zeroAccessTime()
        {
            Console.Clear();
            List<int> numbers = new List<int>() { 1,2,3,4,5,6,7,8,9,10};
            
            for (int i = 0; i < numbers.Count; i++)
            {
                Console.Write(numbers[i]+", ");
            }
            Console.WriteLine();
            numbers.RemoveAt(numbers.Count);
            //იმედია სწორად გავიგე დავალება O(1) დროის მონაკვეთში რანდენადაც ვიცი მხოლოდ მასივის ბოლო წევრი იშლება რადგან სხვა ელემენტებს გადაადგილება არ უწევთ...
        }
        #region Valute
        private static void ValuteConverter()
        {
            Console.Clear();
            Console.WriteLine("Enter two currencies to get their exchange rates: [EURO]/[USD]/[GEL] ");
            string from, to;
            from = Console.ReadLine();
            to = Console.ReadLine();
            ConvertCurrency(from, to);
            returnToMenu();
        }
        private static void ConvertCurrency(string from , string to)
        {
            from = from.ToUpper();
            to = to.ToUpper();
            float USDCurrency = GetCurrency("USD");
            float EUROCurrency = GetCurrency("EURO");
            if (from == "GEL" && to == "USD"|| from == "USD" && to == "GEL")
                Console.WriteLine($"{USDCurrency} GEL = 1 USD || 1 GEL = {1 / USDCurrency} USD");
            else if (from == "GEL" && to == "EURO" || from == "EURO" && to == "GEL")
                Console.WriteLine($"{EUROCurrency} GEL = 1 USD || 1 GEL = {1 / EUROCurrency} USD");
            else if (from == "USD" && to == "EURO" || from == "EURO" && to == "USD")
                Console.WriteLine($"{EUROCurrency/USDCurrency} USD = 1 EURO || 1 USD = {USDCurrency / EUROCurrency} EURO");
        }
        private static float GetCurrency(string v)
        {
            XmlReader reader = XmlReader.Create("http://www.nbg.ge/rss.php");
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            string desc = "";
            string uscurrency = "";
            string eurocurrency = "";
            foreach (SyndicationItem item in feed.Items)
            {
                string subject = item.Title.Text;
                desc = item.Summary.Text;     
            }
            for (int i = 0; i < desc.Length; i++)
            {
                if(desc[i] == 'U' && desc[i + 1] == 'S' && desc[i+2] == 'D')
                {
                    uscurrency = desc.Substring(i + 41, 6);
                }
            }
            for (int i = 0; i < desc.Length; i++)
            {
                if (desc[i] == 'E' && desc[i + 1] == 'U' && desc[i + 2] == 'R')
                {
                    eurocurrency = desc.Substring(i + 35, 6);
                }
            }
            if (v == "EURO")
                return float.Parse(eurocurrency);
            else if (v == "USD")
                return float.Parse(uscurrency);
            else return 0;
        }
        #endregion
        #region stairClimbVariations
        private static void stairClimbVariations()
        {
            Console.Clear();
            Console.Write("Enter number of stairs: ");
            int stairAmount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"There are {countVariants(stairAmount)} ways to climb {stairAmount} stepped stairs");
            returnToMenu();
        }

        private static int countVariants(int stairAmount)
        {
            int[,] F = { { 1, 1 }, { 1, 0 } };
            if (stairAmount == 0)
            {
                return 0;
            }
            power(F, stairAmount);
            return F[0, 0];
        }
        static void power(int[,] F, int n)
        {
            if (n == 0 || n == 1)
            {
                return;
            }
            int[,] A = { { 1, 1 }, { 1, 0 } };

            power(F, n / 2);
            multiply(F, F);

            if (n % 2 != 0)
            {
                multiply(F, A);
            }
        }
        static void multiply(int[,] F, int[,] M)
        {
            int x = F[0, 0] * M[0, 0] + F[0, 1] * M[1, 0];
            int y = F[0, 0] * M[0, 1] + F[0, 1] * M[1, 1];
            int z = F[1, 0] * M[0, 0] + F[1, 1] * M[1, 0];
            int w = F[1, 0] * M[0, 1] + F[1, 1] * M[1, 1];

            F[0, 0] = x;
            F[0, 1] = y;
            F[1, 0] = z;
            F[1, 1] = w;
        }
        #endregion
        #region braceCheck
        private static void bracesCheck()
        {
            Console.Clear();
            Console.Write("Enter your braces: ");
            string braces = Console.ReadLine();
            if (isProperly(braces))
                Console.WriteLine("Mathematically Correct");
            else
                Console.WriteLine("Mathematically Incorrect");
            returnToMenu();
        }

        private static bool isProperly(string braces)
        {
            char[] filteredBraces = braces.ToCharArray();
            /* int defects = 0;
            for (int i = 0; i < NonFilteredBraces.Length; i++)
            {
                if (NonFilteredBraces[i] != ')' || NonFilteredBraces[i] != '(')
                {
                    defects++;
                    for (int x = NonFilteredBraces.Length; x > i; x--)
                    {
                        char temp = NonFilteredBraces[x];
                        NonFilteredBraces[x] = NonFilteredBraces[x - 1];
                        NonFilteredBraces[x - 1] = temp;
                    }
                }
            }
            char[] filteredBraces = new char[NonFilteredBraces.Length - defects];
            */
            int openingBraces = 0;
            int closingBraces = 0;
            if (filteredBraces[0] == '(' && filteredBraces[filteredBraces.Length - 1] == ')')
            {
                for (int i = 0; i < filteredBraces.Length; i++)
                {
                    if (filteredBraces[i] == '(')
                        openingBraces++;
                    else if (filteredBraces[i] == ')')
                        closingBraces++;
                }
            }
            if (openingBraces == closingBraces)
                return true;
            else
                return false;
        }
        #endregion
        #region NotContains
        private static void notContains()
        {
            Console.Clear();
            Random rand = new Random();
            Console.Write("Enter Max Range Of Generated Array: ");
            int temp = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[20];
            for (int i = 0; i < 20; i++)
            {
                arr[i] = rand.Next(1, temp);
                Console.Write($"{arr[i]}, ");
            }
            if(notContainsNum(arr) == -1)
                Console.WriteLine($"Array doesnt contain {notContainsNum(arr)}"); 
            else
                Console.WriteLine($"Array doesnt contain {notContainsNum(arr)}"); 
            returnToMenu();
        }

        private static int notContainsNum(int[] arr)
        {
            Console.WriteLine("");
            int notInArray = -1;
            int temp;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int a = 0; a < arr.Length-1; a++)
                {
                    if (arr[a] > arr[a + 1])
                    {
                        temp = arr[a + 1];
                        arr[a + 1] = arr[a];
                        arr[a] = temp;
                    }
                }
            }
            //Print Array
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]}, ");
            }
            for (int i = 1; i < arr[arr.Length-1]; i++)
            {
                if (!arr.Contains(i))
                {
                    notInArray = i;
                    break;
                }
            }
            Console.WriteLine();
            return notInArray;
        }
        #endregion
        #region MinSplit
        private static void minimalSplit()
        {
            Console.Clear();
            Console.Write("Enter amount of coins: ");
            int coins = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Your minimal split would consist of {calculateMinimalSplit(coins)} coins");
            returnToMenu();
        }

        private static int calculateMinimalSplit(int coins)
        {
            int currentAmount = coins;
            int minimalAmount = 0;
            while(currentAmount != 0)
            {
                if (currentAmount - 50 >= 0)
                {
                    currentAmount -= 50;
                    minimalAmount++;
                }
                else if (currentAmount - 20 >= 0)
                {
                    currentAmount -= 20;
                    minimalAmount++;
                }
                else if (currentAmount - 10 >= 0)
                {
                    currentAmount -= 10;
                    minimalAmount++;
                }
                else if (currentAmount - 5 >= 0)
                {
                    currentAmount -= 5;
                    minimalAmount++;
                }
                else{
                    currentAmount -= 1;
                    minimalAmount++;
                }
            }
            return minimalAmount;
        }
        #endregion
        #region PalindromeTask
        private static void palindromeCheck()
        {
            Console.Clear();
            Console.Write("Enter your text: ");
            string InputText = Console.ReadLine();
            if (isPalindrome(InputText))
                Console.WriteLine("Your text returned True for palindrome check");
            else
                Console.WriteLine("Your text returned False for palindrome check");

            returnToMenu();
        }

        private static bool isPalindrome(string inputText)
        {
            inputText = inputText.ToLower();
            char[] charArray = inputText.ToCharArray();
            Array.Reverse(charArray);
            string reversedInput = new string(charArray);
            return inputText.Equals(reversedInput);
        }
    }
    #endregion
}
