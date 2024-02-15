using Cadastre.Data;
using System.Globalization;
using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;
using Cadastre.DataProcessor.ExportDtos.xml;
using System.Xml.Linq;
using System.Text;

namespace Cadastre.DataProcessor
{
    public class Serializer
    {
        public static string ExportPropertiesWithOwners(CadastreContext dbContext)
        {
            var propertiesWithOwners = dbContext.Properties
             .Where(p => p.DateOfAcquisition >= new DateTime(2000, 1, 1))
             .OrderByDescending(p => p.DateOfAcquisition)
             .ThenBy(p => p.PropertyIdentifier)
             .Select(property => new
             {
                 property.PropertyIdentifier,
                 property.Area,
                 property.Address,
                 DateOfAcquisition = property.DateOfAcquisition.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                 Owners = property.PropertiesCitizens
                     .OrderBy(owner => owner.Citizen.LastName)
                     .Select(owner => new
                     {
                         owner.Citizen.LastName,
                         MaritalStatus = owner.Citizen.MaritalStatus.ToString()
                     })
             })
             .ToList();

            string jsonString = JsonSerializer.Serialize(propertiesWithOwners, new JsonSerializerOptions
            {
                WriteIndented = true // Optional: Makes the JSON output more readable
            });

            return jsonString;
        }

        public static string ExportFilteredPropertiesWithDistrict(CadastreContext dbContext)
        {
            var propertiesWithDistrict = dbContext.Properties
             .Where(p => p.Area >= 100)
             .OrderByDescending(p => p.Area)
             .ThenBy(p => p.DateOfAcquisition)
             .Select(property => new PropertyItemDto
             {
                 PostalCode = property.District.PostalCode,
                 PropertyIdentifier = property.PropertyIdentifier,
                 Area = property.Area,
                 DateOfAcquisition = property.DateOfAcquisition.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
             })
             .ToList();

            var xProperties = new XElement("Properties",
        from property in propertiesWithDistrict
        select new XElement("Property",
            new XAttribute("postal-code", property.PostalCode),
            new XElement("PropertyIdentifier", property.PropertyIdentifier),
            new XElement("Area", property.Area),
            new XElement("DateOfAcquisition", property.DateOfAcquisition)
        )
    );

            // Serialize to XML with the XML declaration, without xmlns:xsi and xmlns:xsd with indentation
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = false, // Include the XML declaration
                Indent = true,
                NewLineHandling = NewLineHandling.Entitize,
                Encoding = Encoding.Unicode // Specify utf-16 encoding
            };

            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    xProperties.Save(xmlWriter);
                }
                return stringWriter.ToString();
            }
        }
    }
}
