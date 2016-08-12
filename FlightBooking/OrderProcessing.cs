using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking
{
    /// orderprocessing class will receive orders from Airline class and processes them and prints the total charge
    
    public class OrderProcessing
    {
        OrderClass obj;
        Int32 amount;
        Int32 locationCharge = 40;
        //initialising location charge

        public OrderProcessing(OrderClass ord)
        {
            this.obj = ord;
            this.amount = ord.Amount;

        }

        public bool creditcheck(double creditcardnumber)
        {
            if ((creditcardnumber >= 5000 && creditcardnumber <= 7000))
            {
                return true;
            }
            else
                return false;
        }
        //validates the credit card

        public double calculatetotcharge(Int32 price, Int32 noofTickets)
        {
            Double charge;
            charge = price * noofTickets + locationCharge + 0.05 * (price * noofTickets);
            return charge;
        }//calculates the total charge 

        public void Processorder(Int32 price)
        {
            double amountofcharge;
            if (creditcheck(obj.cardno))
            {
                amountofcharge = calculatetotcharge(price, obj.Amount);
                Console.WriteLine("Total charge is {0} for the travel agency having SenderID :: {1} Price :: {2} CardNumber :: {3} No of Tickets :: {4} TIME OF ORDER :: {5} ", amountofcharge, obj.SenderId, price, obj.cardno, obj.Amount, obj.time);

            }

        }
    }
}
