using System.Xml.Serialization;

namespace Cadastre.DataProcessor.ExportDtos.xml
{
    [XmlRoot("Properties")]
    public class PropertyDto
    {
        [XmlElement("Property")]
        public List<PropertyItemDto> Properties { get; set; }
    }

    public class PropertyItemDto
    {
        [XmlAttribute("postal-code")]
        public string PostalCode { get; set; }

        public string PropertyIdentifier { get; set; }

        public int Area { get; set; }

        [XmlElement("DateOfAcquisition")]
        public string DateOfAcquisition { get; set; }
    }
}
