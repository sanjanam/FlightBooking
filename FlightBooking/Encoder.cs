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
    
    /// create encoder class which uses serialisation for encoding
    
    class Encoder
    {
        public string encoder(OrderClass o)
        {
            StringWriter s = new StringWriter();
            //create string writer object 

            XmlSerializer x = new XmlSerializer(o.GetType());
            //create xmlserialiser object to get the type of object

            x.Serialize(s, o);
            //serialise the object and save as string

            return s.ToString();
        }
    }
}
