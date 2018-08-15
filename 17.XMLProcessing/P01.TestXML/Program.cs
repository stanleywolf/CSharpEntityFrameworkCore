using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace P01.TestXML
{
    class Program
    {
        static void Main(string[] args)
        {
            var library = new Library()
            {
                Id = 1,
                Name = "Samokov Library"
            };

            XmlSerializer serializer = new XmlSerializer(typeof(Library));

            using (var writer = new StreamWriter("library.xml"))        
            {
                serializer.Serialize(writer,library);
            }
            //deserialize
            //var library = new Library();

            //XmlSerializer serializer = new XmlSerializer(typeof(Library));

            //using (var reader = new StreamReader("library.xml"))
            //{
            //    library = (Library)serializer.Deserialize(reader);
            //}
            string xmlString;
            using (var reader = new StreamReader("library.xml"))
            {
                XDocument document = XDocument.Load(reader);
                var title = document.Root.Element("books")
                    .Elements("book")
                    .Where(x => x.Element("author").Value == "Stephen King")
                    .Select(x => x.Element("bookTitle").Value)
                    .ToList();

                Console.WriteLine(string.Join(Environment.NewLine, title));
            }
        }
    }
}
