namespace Medicines.DataProcessor
{
    using Cadastre.DataProcessor.ImportDtos.xml;
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ImportDtos.json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Linq;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var patientsDtos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PatientDto>>(jsonString);
            var importedPatients = new List<Patient>();

            foreach (var patientDto in patientsDtos)
            {
                var isValidAgeGroup = Enum.TryParse(patientDto.AgeGroup, out AgeGroup ageGroup);
                var isValidGender = Enum.TryParse(patientDto.Gender, out Gender gender);
                var isFullNameValid = !string.IsNullOrWhiteSpace(patientDto.FullName) && patientDto.FullName.Length <= 100;
                bool hasDuplicateMedicines = patientDto.Medicines.Count != patientDto.Medicines.Distinct().Count();

                if (!IsValid(patientDto) || !isValidAgeGroup || !isValidGender || !isFullNameValid || hasDuplicateMedicines)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var patient = new Patient
                {
                    FullName = patientDto.FullName,
                    AgeGroup = ageGroup,
                    Gender = gender,
                    PatientsMedicines = new List<PatientMedicine>()
                };

                foreach (var medicineId in patientDto.Medicines.Distinct())
                {
                    var medicine = context.Medicines.Find(medicineId);
                    if (medicine != null)
                    {
                        patient.PatientsMedicines.Add(new PatientMedicine
                        {
                            MedicineId = medicine.Id,
                            Patient = patient
                        });
                    }
                    else
                    {
                        sb.AppendLine($"Error: Medicine with ID {medicineId} does not exist for patient {patientDto.FullName}.");
                    }
                }

                importedPatients.Add(patient);
                sb.AppendLine(string.Format(SuccessfullyImportedPatient, patientDto.FullName, patient.PatientsMedicines.Count));
            }

            context.Patients.AddRange(importedPatients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var xmlDoc = XDocument.Parse(xmlString);

            var pharmacies = xmlDoc.Element("Pharmacies").Elements("Pharmacy")
                .Select(pharmacy => new PharmacyDto
                {
                    IsNonStop = pharmacy.Attribute("non-stop")?.Value,
                    Name = pharmacy.Element("Name")?.Value,
                    PhoneNumber = pharmacy.Element("PhoneNumber")?.Value,
                    Medicines = pharmacy
                        .Element("Medicines")
                        .Elements("Medicine")
                        .Select(medicine => new MedicineDTo
                        {
                            Category = medicine.Attribute("category")?.Value,
                            Name = medicine.Element("Name")?.Value,
                            Price = medicine.Element("Price")?.Value,
                            ProductionDate = medicine.Element("ProductionDate")?.Value,
                            ExpiryDate = medicine.Element("ExpiryDate")?.Value,
                            Producer = medicine.Element("Producer")?.Value
                        })
                        .ToList()
                })
                .ToList();

            foreach (var pharmacyInfo in pharmacies)
            {
                if (!bool.TryParse(pharmacyInfo.IsNonStop, out bool isNonStop) || !IsValid(pharmacyInfo))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var pharmacy = new Pharmacy
                {
                    Name = pharmacyInfo.Name,
                    PhoneNumber = pharmacyInfo.PhoneNumber,
                    IsNonStop = isNonStop,
                    Medicines = new List<Medicine>()
                };

                foreach (var medicineInfo in pharmacyInfo.Medicines)
                {
                    if (!IsValid(medicineInfo)
                        || !decimal.TryParse(medicineInfo.Price, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price)
                        || !DateTime.TryParseExact(medicineInfo.ProductionDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime productionDate)
                        || !DateTime.TryParseExact(medicineInfo.ExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiryDate)
                        || !Enum.TryParse(medicineInfo.Category, out Category category)
                        || productionDate >= expiryDate
                        || pharmacy.Medicines.Any(m => m.Name == medicineInfo.Name && m.Producer == medicineInfo.Producer))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var medicine = new Medicine
                    {
                        Name = medicineInfo.Name,
                        Price = price,
                        ProductionDate = productionDate,
                        ExpiryDate = expiryDate,
                        Category = category,
                        Producer = medicineInfo.Producer
                    };

                    pharmacy.Medicines.Add(medicine);
                }

                context.Pharmacies.Add(pharmacy);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy, pharmacy.Name, pharmacy.Medicines.Count));
            }

            context.SaveChanges();
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
