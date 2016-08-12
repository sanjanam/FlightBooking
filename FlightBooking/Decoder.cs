using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;
using System.Threading;

namespace FlightBooking
{
    
    /// create decoder class which uses deserialisation
    
    class Decoder
    {
        public OrderClass decoder(String s)
        {

            OrderClass tempObject = default(OrderClass);
            //create temporary orderobject

            XmlSerializer xs = new XmlSerializer(typeof(OrderClass));
            //create xml serialiser

            StringReader encodedstring = new StringReader(s);
            //Read the received encoded string

            tempObject = (OrderClass)xs.Deserialize(encodedstring);
            //decode and save it on to the temporary object

            return tempObject;
        }
    }
}
