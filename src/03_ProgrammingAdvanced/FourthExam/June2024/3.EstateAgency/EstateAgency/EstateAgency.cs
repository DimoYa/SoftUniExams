using System.Text;

namespace EstateAgency
{
    public class EstateAgency
    {
        public int Capacity { get; set; }
        public int Count => RealEstates.Count;
        public List<RealEstate> RealEstates { get; set; }

        public EstateAgency(int capacity)
        {
            Capacity = capacity;
            RealEstates = new List<RealEstate>();
        }

        public void AddRealEstate(RealEstate realEstate)
        {
            if (Capacity > RealEstates.Count)
            {
                var isAdded = this.RealEstates.FirstOrDefault(x => x.Address == realEstate.Address);

                if (isAdded is null)
                {
                    RealEstates.Add(realEstate);
                }
            }
        }

        public bool RemoveRealEstate(string address)
        {

            var estate = this.RealEstates.FirstOrDefault(x => x.Address == address);

            if (estate is not null)
            {
                RealEstates.Remove(estate);

                return true;
            }

            return false;
        }

        public List<RealEstate> GetRealEstates(string postalCode)
        {
            return RealEstates.Where(x => x.PostalCode == postalCode).ToList();
        }

        public RealEstate GetCheapest()
        {
            return RealEstates.OrderBy(x => x.Price).FirstOrDefault();
        }

        public double GetLargest()
        {
            return RealEstates.Max(x=> x.Size);
        }

        public string EstateReport()
        {
            var result = new StringBuilder();
            result.AppendLine("Real estates available:");
            foreach (var item in RealEstates)
            {
                result.AppendLine(item.ToString());
            }

            return result.ToString();
        }
    }
}
