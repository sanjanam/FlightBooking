using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace FlightBooking
{
   
    /// The multicellbuffer class contain methods for adding and receiving orders from buffer
    

    public delegate void orderBuffer();
    //create a buffer order event which notifies the airline class when the order is added
    public class MulticellBuffer
    {
        static string[] bCell = new string[2];
        //Declare a buffer with two cells

        public event orderBuffer Orderbuff;
        //create the event

        public static Semaphore emptysem = new Semaphore(2, 2);
        public static Semaphore fullsem = new Semaphore(0, 2);
        //create two semaphores so that adding order and receiving orders is done in synchronised manner

        public static bool[] free = new bool[2];
        //create a boolean array which tells if the cell is free or not

        object obj = new object();
        public int current = 0;
        //it helps in pointing to the next cell which is full

        public void setFree()
        {
            for (int i = 0; i < 2; i++)
            {
                free[i] = true;
            }

        }
        //create an array which sets the corresponding values to true when it is writable

        public void setOneCell(string s)
        {
            emptysem.WaitOne();
            //Thread enters when sem count is above 0 or waits  when it is less than 0
            lock (obj)
                for (int m = 0; m < 2; m++)
                {

                    if (free[m])//checks if empty or not
                    {
                        bCell[m] = s;//place the encoded order string into buffercell
                        Console.WriteLine("Travel Agency " + Thread.CurrentThread.Name + "has added order at" + DateTime.Now);
                        free[m] = false;//Make the cell not writable 
                        break;
                    }
                }
            fullsem.Release();
            //Release the full sem semaphore so that the order is received from the buffer

            if (Orderbuff != null)
            {
                Orderbuff();
            }//Notify the airline class whenever order is placed
        }

        public string getOneCell()
        {
            string order = "";
            fullsem.WaitOne();
            //Wait on fullsem
            lock (obj)
            {
                for (int n = current; n < 2; n++)
                {
                    current = n + 1;
                    //make the current pointer poin to next full cell

                    if (!free[n])
                    //check if full 
                    {

                        order = bCell[n];
                        //Retreive the order from buffer

                        free[n] = true;
                        //Set the cell to true so that we can write it into it next time

                        if (current >= 2)
                        {
                            current = 0;
                        }
                        break;
                    }
                    if (current == 2)
                    {
                        n = -1;
                    }
                    if (n == 2)
                    {
                        break;
                    }

                }
            }

            emptysem.Release();
            //release on the emptysem

            return order;
        }

    }

}
