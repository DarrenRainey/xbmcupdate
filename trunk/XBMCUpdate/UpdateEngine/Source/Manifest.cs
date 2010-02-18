using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace XbmcUpdate.UpdateEngine.Source
{
    [Serializable]
    public class Manifest
    {
        [XmlAttribute]
        public DateTime LastUpdated { get; set; }

        [XmlElement]
        public List<String> AltManifest { get; set; }
       
        public List<SourceInfo> Sources { get; set; }



    }
}


