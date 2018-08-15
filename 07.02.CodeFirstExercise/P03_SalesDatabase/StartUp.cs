using System;
using P03_SalesDatabase.Data;

namespace P03_SalesDatabase
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (SalesContext context = new SalesContext())
            {
                // context.Database.EnsureCreated();
                int[] arr = new int[]{1,2,3,4,4,2,1};

                int lastElement = arr.Length - 1;
                bool isTelescopic = true;
                for (int i = 0; i <= arr.Length / 2; i++)
                {
                    if (arr[i] != arr[lastElement - i])
                    {
                        isTelescopic = false;
                        break;
                    }
                }
                Console.WriteLine(isTelescopic);
            }
        }
    }
}
