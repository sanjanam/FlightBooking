using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking
{
      
    /// Create the orderclass with attributes senderid,time,amount,cardnumber
        public class OrderClass
    {
        private int cardNo;
        private int amount;
        private string senderId;
        public DateTime time;

        public int cardno
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        public int Amount
        {
            get { return amount; }
            set { amount = value; }

        }
        public string SenderId
        {
            get { return senderId; }
            set { senderId = value; }
        }

    }//create the private attributes and set them using public method
}
