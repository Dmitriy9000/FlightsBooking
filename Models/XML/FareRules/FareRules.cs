using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Serialization;

namespace PhaseTicket.Models.XML.FareRules
{
    /*
     * <FareRules> 
 <Direction> 
     <Header CBD="Avail" CAD="Avail"RBD="Avail" RAD="NotAvail"> 
     <From AirportName="Шереметьево" CityName="Москва" /> 
     <To AirportName="Хитроу" CityName="Лондон" /> 
     </Header> 
    <Rules>1. RULE APPLICATION
    . . . . . . 
    </Rules> 
 </Direction> 
</FareRules>
     */

    public class FareRules
    {
        [XmlElement("Direction", typeof(FareRulesDirection))]
        public FareRulesDirection[] Directions { get; set; }

        public void ReplaceBreakLines()
        {
            foreach (var fareRulesDirection in Directions)
            {
                string result = Regex.Replace(fareRulesDirection.Rules, @"\r\n?|\n", "<br />");
                fareRulesDirection.Rules = result;
            }
        }
    }

    public class FareRulesDirection
    {
        [XmlElement("Header")]
        public FareRulesDirectionHeader Header { get; set; }

        

        [XmlElement("Rules")]
        public string Rules { get; set; }
    }

    public class FareRulesDirectionHeader
    {
        [XmlAttribute("CBD")]
        public bool ChangeBeforeDeparture { get; set; }
        [XmlAttribute("CAD")]
        public bool ChangeAfterDeparture { get; set; }
        [XmlAttribute("RBD")]
        public bool ReturnBeforeDeparture { get; set; }
        [XmlAttribute("RAD")]
        public bool ReturnAfterDeparture { get; set; }
        [XmlElement("Dep")]
        public FareRulesDirectionWay From { get; set; }
        [XmlElement("Arr", typeof(FareRulesDirectionWay))]
        public FareRulesDirectionWay To { get; set; }
    }

    public class FareRulesDirectionWay
    {
        [XmlAttribute("Apt")]
        public string AirportName { get; set; }
        [XmlAttribute("City")]
        public string CityName { get; set; }
        [XmlAttribute("Ctry")]
        public string Country { get; set; }
    }
}