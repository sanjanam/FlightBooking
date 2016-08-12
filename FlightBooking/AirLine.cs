using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace FlightBooking
{
    //AirLine class
    //create an airline class that creates a price cut and notifies the travel agencies
    

    public delegate void priceCutEvent(Int32 pr);
    //Declaring a price cut event 

    public class AirLine
    {

        public static MulticellBuffer m = new MulticellBuffer();
        Random rnd = new Random();
        //Decalring a random variable
        public AirLine(MulticellBuffer multi)
        {

            m = multi;
            m.Orderbuff += orderadded;
            //Subscribe to the multicellbuffer event when the order is added to the buffer
        }
        public static int p = 0;
        //Declare p to 0 
        public event priceCutEvent PriceCut;
        // Defining the  event
        public static Int32 flightPrice = 900;
        //Initialize the flightprice

        public Int32 getPrice()
        {
            return flightPrice;
        }
        //return the current flight price
        public void PricingModel()
        {
            Thread.Sleep(500);
            while (p < 20)
            {
                p++;//increment p value whenever there is a price cut
                Int32 oldprice = flightPrice;
                changePrice(flightPrice);

            }
        }
        //Calls the pricing model method when airline thread gets started

        public void changePrice(Int32 pr)
        {
            Int32 price;
            price = rnd.Next(100, 900);
            //generating a random number for flight price between 100 to 900 
            if (price < pr)
            { // a price cut occured 
                if (PriceCut != null)
                {// there is at least a subscriber

                    Console.WriteLine("old price is {0} new {1}", flightPrice, price);

                    PriceCut(price); // emit event to subscribers
                    flightPrice = price;

                    Thread.Sleep(2000);
                }
            }
        }
        public void orderadded()
        {
            Console.WriteLine("Order received from {0}", Thread.CurrentThread.Name);
            string s = m.getOneCell();
            //retrieve the order from buffer cell

            Decoder d = new Decoder();
            OrderClass o = d.decoder(s);
            //decode the string and save the object

            OrderProcessing op = new OrderProcessing(o);
            op.Processorder(flightPrice);
            //send the object for processing

        }
        //call this method when ever airline is notified by the MulticellBuffer
    }

}
