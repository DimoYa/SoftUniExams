namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.DataProcessor.ExportDtos;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using System.Text;
    using System.Text.Json;
    using System.Xml;
    using System.Xml.Linq;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            var parsedDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var patientsWithMedicine = context.Patients
                .Where(p => p.PatientsMedicines
                    .Any(pm => pm.Medicine.ProductionDate > parsedDate)) // at least one medicine produced after the given date
                .Select(p => new
                {
                    Name = p.FullName,
                    AgeGroup = p.AgeGroup.ToString(),
                    Gender = p.Gender.ToString().ToLower(),
                    Medicines = p.PatientsMedicines
                        .Where(pm => pm.Medicine.ProductionDate > parsedDate)
                        .Select(pm => new
                        {
                            Name = pm.Medicine.Name,
                            Price = pm.Medicine.Price,
                            Category = pm.Medicine.Category.ToString().ToLower(),
                            Producer = pm.Medicine.Producer,
                            ExpiryDate = pm.Medicine.ExpiryDate
                        })
                        .OrderByDescending(m => m.ExpiryDate)
                        .ThenBy(m => m.Price)
                        .ToList()
                })
                .Where(p => p.Medicines.Any())
                .OrderByDescending(p => p.Medicines.Count)
                .ThenBy(p => p.Name)
                .ToList();

            // Build XML
            var xPatients = new XElement("Patients",
                from patient in patientsWithMedicine
                select new XElement("Patient",
                    new XAttribute("Gender", patient.Gender),
                    new XElement("Name", patient.Name),
                    new XElement("AgeGroup", patient.AgeGroup),
                    new XElement("Medicines",
                        from med in patient.Medicines
                        select new XElement("Medicine",
                            new XAttribute("Category", med.Category),
                            new XElement("Name", med.Name),
                            new XElement("Price", med.Price.ToString("F2", CultureInfo.InvariantCulture)),
                            new XElement("Producer", med.Producer),
                            new XElement("BestBefore", med.ExpiryDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                        )
                    )
                )
            );

            // Serialize with indentation and proper encoding
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = false,
                Indent = true,
                Encoding = Encoding.Unicode,
                NewLineHandling = NewLineHandling.Entitize
            };

            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    xPatients.Save(xmlWriter);
                }

                return stringWriter.ToString().TrimEnd();
            }
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {
            var medicinesCategory = context.Medicines
                     .Where(m => (int)m.Category == medicineCategory && m.Pharmacy.IsNonStop)
                     .OrderBy(m => m.Price) 
                     .ThenBy(m => m.Name) 
                     .Select(med => new
                     {
                         med.Name,
                         Price = med.Price.ToString("F2", CultureInfo.InvariantCulture), 
                         Pharmacy = new
                         {
                             med.Pharmacy.Name,
                             med.Pharmacy.PhoneNumber
                         }
                     })
                     .ToList();

            string jsonString = JsonSerializer.Serialize(medicinesCategory, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            return jsonString;
        }
    }
}
