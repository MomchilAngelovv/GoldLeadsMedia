namespace GoldLeadsMedia.Web.Models.Seeder
{
    using System.Collections.Generic;

    public class CountrySeedModel
    {
        public string Name { get; set; }
        public string Alpha2Code { get; set; }
        public List<string> CallingCodes { get; set; }
    }
}
