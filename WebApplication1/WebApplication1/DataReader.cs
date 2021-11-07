using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public interface ICustomer
    {
        [Display(Name = "Customer Identity Number")]
        int Id { get; set; }
        [Display(Name = "First Name")]
        string FirstName { get; set; }
        [Display(Name = "Date of birth")]
        DateTime DataOfBirth { get; set; }
        [Display(Name = "Last Name")]
        string LastName { get; set; }
        [Display(Name = "Member card number")]
        string MemberCardNumber { get; set; }
        [Display(Name = "Policy number")]
        string PolicyNumber { get; set; }
    }


    public class Customer : ICustomer
    {
        [JsonProperty("id")]
        [Display(Name = "Customer Identity Number")]
        public int Id { get; set; }

        [JsonProperty("firstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [JsonProperty("dataOfBirth")]
        [Display(Name = "Date of birth")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime DataOfBirth { get; set; }

        [JsonProperty("lastName")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [JsonProperty("memberCardNumber")]
        [Display(Name = "Member card number")]
        public string MemberCardNumber { get; set; }

        [JsonProperty("policyNumber")]
        [Display(Name = "Policy number")]
        public string PolicyNumber { get; set; }
    }

    public interface IDataReader
    {
        IEnumerable<ICustomer> GetAllCustomers();
    }

    /// <summary>
    /// Usage of interface is to be able to use Factory pattern for flexibity. For instance if we need to use different data source like excel or remote claud db or in the case we have different customer implementation
    /// </summary>
    public class DataReader : IDataReader
    {
        private readonly string _filePath;
        public DataReader(string filePath)
        {
            this._filePath = filePath;
        }
        public IEnumerable<ICustomer> GetAllCustomers()
        {
            using (StreamReader r = new StreamReader(_filePath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Customer>>(json);
            }
        }
    }

 
}
