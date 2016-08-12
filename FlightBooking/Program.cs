using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;


namespace FlightBooking
{
    public class MainClass
    {
        public static int Main(string[] args)
        {
            MulticellBuffer mb = new MulticellBuffer();
            //create a multicell buffer object

            mb.setFree();
            //set all the cells of multicell buffer to true so that order is written to it

            AirLine air = new AirLine(mb);
            //create an airline object by passing the multicell buffer


            TravelAgency agent = new TravelAgency(mb, air);
            //create a travelagency object and pass the buffer object and airline object

            Thread[] ta = new Thread[5];
            //create travelagent thread array

            Thread flight = new Thread(new ThreadStart(air.PricingModel));
            //create a airline thread flight and call the PricingModel method on starting the thread

            flight.Start();

            for (int i = 0; i < 5; i++)
            {
                ta[i] = new Thread(new ThreadStart(agent.travelFunc));
                ta[i].Name = (i + 1).ToString();
                ta[i].Start();
            }
            //create five travel agency threads and assign a name to each thread which can be used as a id 
            return 0;

        }
    }
}