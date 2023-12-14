using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Utilities
{
    public class HeaderData
    {
        [JsonProperty("token")]
        public string? Token { get; set; }
    }
}
