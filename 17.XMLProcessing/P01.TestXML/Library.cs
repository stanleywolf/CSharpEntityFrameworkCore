using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P01.TestXML
{
    [XmlRoot(ElementName = "library")]
    public class Library
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("LibraryName")]
        public string Name { get; set; }

        [XmlArray(ElementName = "books")]
        public Book[] Books { get; set; } = new Book[2]
        {
            new Book(){Author = "Stephen King",Title = "It",Isbn = "Horror"}, 
            new Book(){Author = "S.Nikolov",Title = "Learn to be good coder",Isbn = "Fantastic"}
        };
    }
}
