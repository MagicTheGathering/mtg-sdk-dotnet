using System.Text.Json.Serialization;

namespace MtgApiManager.Lib.Test.Utility
{
    public class DtoTestObject
    {
        public string Property1
        {
            get;
            set;
        }

        [JsonPropertyName("jsonProperty2")]
        public string Property2
        {
            get;
            set;
        }
    }
}