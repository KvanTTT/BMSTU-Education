using System;
using System.Xml.Serialization;
using System.IO;

namespace DataBase
{
    public class Book
    {
        #region Fields

        [XmlAttribute]
        public string Name
        {
            get;
            set;
        }

        [XmlAttribute]
        public string ISBN
        {
            get;
            set;
        }

        [XmlElement]
        public string Author
        {
            get;
            set;
        }

        [XmlElement]
        public string Genre
        {
            get;
            set;
        }

        [XmlElement]
        public string Publisher
        {
            get;
            set;
        }

        [XmlElement]
        public string Language
        {
            get;
            set;
        }

        [XmlElement]
        public int? Year
        {
            get;
            set;
        }

        [XmlElement]
        public int? PageCount
        {
            get;
            set;
        }

        [XmlElement]
        public string Description
        {
            get;
            set;
        }

        #endregion

        protected Book()
        {

        }

        public Book(string Name, string ISBN, string Author = "", string Genre = "", string Publisher = "", string Language = "",
            int? Year = null, int? PageCount = null, string Description = "")
        {
            this.Name = Name;
            this.ISBN = ISBN;
            this.Author = Author;
            this.Genre = Genre;
            this.Publisher = Publisher;
            this.Language = Language;
            this.Year = Year;
            this.PageCount = PageCount;
            this.Description = Description;
        }

        public Book(string XMLDescription)
        {
            FromString(XMLDescription);
        }

        public Book(Book B)
        {
            this.Name = B.Name;
            this.ISBN = B.ISBN;
            this.Author = B.Author;
            this.Genre = B.Genre;
            this.Publisher = B.Publisher;
            this.Language = B.Language;
            this.Year = B.Year;
            this.PageCount = B.PageCount;
            this.Description = B.Description;
        }

        public override string ToString()
        {
            XmlSerializer Serializer = new XmlSerializer(typeof(Book));
            MemoryStream Stream  = new MemoryStream();
            Serializer.Serialize(Stream, this);
            string Result = System.Text.Encoding.UTF8.GetString(Stream.ToArray());
            Stream.Close();
            return Result;
        }

        public void FromString(string Str)
        {
            XmlSerializer Serializer = new XmlSerializer(typeof(Book));
            MemoryStream Stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(Str));
            Book B = (Book)Serializer.Deserialize(Stream);
            Stream.Close();
            this.Name = B.Name;
            this.ISBN = B.ISBN;
            this.Author = B.Author;
            this.Genre = B.Genre;
            this.Publisher = B.Publisher;
            this.Language = B.Language;
            this.Year = B.Year;
            this.PageCount = B.PageCount;
            this.Description = B.Description;
        }
    }
}
