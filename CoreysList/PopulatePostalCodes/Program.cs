using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreysList.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Xml;
using System.Xml.XPath; 
using System.Xml.Linq;
namespace PopulatePostalCodes
{
    class Program
    {
        static void Main(string[] args)
        {
            //connect to databas
            CoreysListEntities db = new CoreysListEntities();
            //get all cities
            List<City> allCities = db.Cities.OrderBy(c => c.CityName).ToList();

            //create a counter to track when to sleep thread due to api calls limit per sec
            int counter = 0;

            //for each city
            foreach (City c in allCities)
            {
                //if api call = 5 sleep the thread
                if (counter == 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("---Waiting on API------");
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(5000);
                    counter = 0;
                }

                //call the get postal code function
                string postalCode = GetPostalCode(c.CityName.Trim(), c.State.StateName);

                //get a reference for the correct city to update
                City updateCity = db.Cities.FirstOrDefault(u => u.CityID == c.CityID);

                //set the new value
                updateCity.PostalCode = postalCode;

                //save 
                db.SaveChanges(); 
                Console.WriteLine(c.CityName+ " " + c.State.StateName+ ": " + postalCode + " Record Updated");
                counter++;
            }

            //pause application
            Console.WriteLine();
            Console.WriteLine("Updates Finished.....");
            Console.ReadLine();
        }

        static string GetPostalCode(string cityName, string stateName)
        {
            //get lat and long of city within state
            var address = ( cityName + ", " + stateName);
            //format the url
            var requestUriForLatLong = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));
            //create the request
            var requestForLatLng = WebRequest.Create(requestUriForLatLong);
            //get the response
            var response = requestForLatLng.GetResponse();
            //stream the resonse into an xdoc
            var xdocLatLng = XDocument.Load(response.GetResponseStream());
            //get the result element
            var resultLatLng = xdocLatLng.Element("GeocodeResponse").Element("result");

            if (resultLatLng != null)
            {
                //get the location element 
                var locationElement = resultLatLng.Element("geometry").Element("location");

                //extract lat and lng
                string lat = locationElement.Element("lat").Value;
                string lng = locationElement.Element("lng").Value;

                //set up new request to get information by sending lat and long
               // var result2 = new System.Net.WebClient().DownloadString("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + lng + "&sensor=true");

                XPathDocument xpathDoc = new XPathDocument("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lat + "," + lng + "&sensor=true");
                XPathNavigator xpathNav = xpathDoc.CreateNavigator();

                XPathExpression xpathExpr = xpathNav.Compile("/GeocodeResponse/result[1]/address_component[last()]/long_name");
                XPathNodeIterator xpathIter = xpathNav.Select(xpathExpr);
                xpathIter.MoveNext();
                XPathNavigator nav2 = xpathIter.Current.Clone();
                string postalCode = nav2.Value; 
                
                ////create xml doc
                //XmlDocument doc = new XmlDocument();
                ////load the result
                //doc.LoadXml(result2);

                ////go down each node in the tree
                //XmlNode responseNode = doc.LastChild;
                //XmlNode resultSiblingNode = responseNode.FirstChild;
                //XmlNode resultNode = resultSiblingNode.NextSibling;
                //XmlNode tempNode = resultNode.LastChild;
                //XmlNode addressNode = resultNode.LastChild;

                ////loop through the sibling nodes
                //for (int i = 0; i < 4; i++)
                //{
                //    addressNode = tempNode.PreviousSibling;
                //    tempNode = addressNode.PreviousSibling;
                //    i++;
                //}

                ////some have less sibling nodes so this will return to the previous node
                //if (addressNode.FirstChild.InnerText == "United States") 
                //{
                //    tempNode = addressNode.NextSibling;
                //    addressNode = tempNode; 
                //}

                //extract the postal code and return
                //string postalCode = addressNode.FirstChild.InnerText;
                return postalCode;
            }
            else
            {
                return "null";
            }
            
        }
    }
}
