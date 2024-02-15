namespace Cadastre.DataProcessor
{
    using Cadastre.Data;
    using Cadastre.Data.Enumerations;
    using Cadastre.Data.Models;
    using Cadastre.DataProcessor.ImportDtos;
    using Cadastre.DataProcessor.ImportDtos.json;
    using Cadastre.DataProcessor.ImportDtos.xml;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Linq;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid Data!";
        private const string SuccessfullyImportedDistrict =
            "Successfully imported district - {0} with {1} properties.";
        private const string SuccessfullyImportedCitizen =
            "Succefully imported citizen - {0} {1} with {2} properties.";

        public static string ImportDistricts(CadastreContext dbContext, string xmlDocument)
        {
            var sb = new StringBuilder();

            var xmlDoc = XDocument.Parse(xmlDocument);

            var districts = xmlDoc.Element("Districts").Elements("District")
                .Select(districtElement => new DistrictDto
                {
                    Region = districtElement.Attribute("Region").Value,
                    Name = districtElement.Element("Name").Value,
                    PostalCode = districtElement.Element("PostalCode").Value,
                    Properties = districtElement.Elements("Properties").Elements("Property")
                        .Select(propertyElement => new PropertyDto
                        {
                            PropertyIdentifier = propertyElement.Element("PropertyIdentifier").Value,
                            Area = int.Parse(propertyElement.Element("Area").Value),
                            Details = propertyElement.Element("Details")?.Value,
                            Address = propertyElement.Element("Address").Value,
                            DateOfAcquisition = propertyElement.Element("DateOfAcquisition").Value
                        }).ToList()
                }).ToList();


            foreach (var districtInfo in districts)
            {

                var isValidRegion = Enum.TryParse(districtInfo.Region, out Region region);
                var existingDistrict = dbContext.Districts.FirstOrDefault(d => d.Name == districtInfo.Name);

                if (!IsValid(districtInfo) || !isValidRegion || (existingDistrict is not null))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var district = new District
                {
                    Region = region,
                    Name = districtInfo.Name,
                    PostalCode = districtInfo.PostalCode,
                    Properties = new List<Property>()
                };

                foreach (var propertyInfo in districtInfo.Properties)
                {
                    DateTime dateOfAcquisition;
                    var isDateValid = DateTime.TryParseExact(propertyInfo.DateOfAcquisition, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfAcquisition);

                    if (!IsValid(propertyInfo)
                        || !isDateValid
                        || dbContext.Properties.Any(p => p.PropertyIdentifier == propertyInfo.PropertyIdentifier)
                        || district.Properties.Any(p => p.PropertyIdentifier == propertyInfo.PropertyIdentifier)
                        || dbContext.Properties.Any(p => p.Address == propertyInfo.Address)
                        || district.Properties.Any(p => p.Address == propertyInfo.Address)
                        )
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var property = new Property
                    {
                        PropertyIdentifier = propertyInfo.PropertyIdentifier,
                        Area = propertyInfo.Area,
                        Details = propertyInfo.Details,
                        Address = propertyInfo.Address,
                        DateOfAcquisition = dateOfAcquisition
                    };

                    district.Properties.Add(property);
                }

                dbContext.Districts.Add(district);
                sb.AppendLine(string.Format(SuccessfullyImportedDistrict, districtInfo.Name, district.Properties.Count));
            }

            dbContext.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {
            var sb = new StringBuilder();

            var citizenDtos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CitizenDto>>(jsonDocument);

            var importedCitizens = new List<Citizen>();

            foreach (var citizenDto in citizenDtos)
            {
                var isValidMaritalStatus = Enum.TryParse(citizenDto.MaritalStatus, out MaritalStatus maritalStatus);

                DateTime birthDate;
                var isDateValid = DateTime.TryParseExact(citizenDto.BirthDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);

                if (!IsValid(citizenDto) || !isValidMaritalStatus || !isDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var citizen = new Citizen
                {
                    FirstName = citizenDto.FirstName,
                    LastName = citizenDto.LastName,
                    BirthDate = birthDate,
                    MaritalStatus = maritalStatus
                };

                foreach (var propertyId in citizenDto.Properties)
                {
                    var property = dbContext.Properties.FirstOrDefault(p => p.Id == propertyId);
                    if (property is not null)
                    {
                        citizen.PropertiesCitizens.Add(new PropertyCitizen { Property = property });
                    }
                }

                importedCitizens.Add(citizen);
                sb.AppendLine(string.Format(SuccessfullyImportedCitizen, citizenDto.FirstName, citizenDto.LastName, citizenDto.Properties.Count));
            }

            dbContext.Citizens.AddRange(importedCitizens);
            dbContext.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
