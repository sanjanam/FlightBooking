using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;


namespace FlightBooking
{
    
    /// The class travel Agency will listen to the price cut event and each travel agent thread will create a order on the event 
    /// It also sends the order to encoder and sends the encrypted order to multicell buffer
    
    public class TravelAgency
    {
        MulticellBuffer cell;
        AirLine flight;
        public int noofTickets;
        public string ID;
        public Random r = new Random();
        public int CardNumber;
        public int i = 0;
         public TravelAgency(MulticellBuffer multi, AirLine flights)
        {
            cell = multi;
            flight = flights;
            flight.PriceCut += pricecuthandler;
        }//Constructor

        public void pricecuthandler(Int32 price)
        {
            Console.WriteLine("Price cut has occured AirLine price{0}", price);
        }//pricecut handler 

        public void travelFunc()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(2000);

                OrderClass ord = new OrderClass();
                //create a new order

                ord.SenderId = Thread.CurrentThread.Name;
                //sender id assign to thread name

                Int32 pr = flight.getPrice();
                //get the price from airline class

                noofTickets = r.Next(1,20);
                ord.Amount = noofTickets;
                CardNumber = r.Next(5000, 7000);
                ord.cardno = CardNumber;
                ord.time = DateTime.UtcNow;
                //initialising amount,cardnumber,time

                Encoder e = new Encoder();
                string ordstr = e.encoder(ord);
                //Send the order to the encoder and receive the encrypted string

                cell.setOneCell(ordstr);
                //Send the encoded string to the buffer
            }


        }

    }


}
