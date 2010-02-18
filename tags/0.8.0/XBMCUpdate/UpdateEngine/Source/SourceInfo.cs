using System;
using System.Xml.Serialization;

namespace XbmcUpdate.UpdateEngine.Source
{
    [Serializable]
    public class SourceInfo
    {
        [XmlAttribute]
        public String SourceName { get; set; }  

        [XmlAttribute]
        public Boolean Default { get; set; }

        [XmlElement]
        public String Url { get; set; }

        [XmlElement]
        public String RegEx { get; set; }

        [XmlIgnore]
        public int Index;


    }
}


