using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleMe.Zalando
{
    public class Content
    {
        public string id { get; set; }
        public string modelId { get; set; }
        public string name { get; set; }
        public string shopUrl { get; set; }
        public string color { get; set; }
        public bool available { get; set; }
        public string season { get; set; }
        public string seasonYear { get; set; }
        public string activationDate { get; set; }
        public List<object> additionalInfos { get; set; }
        public List<string> genders { get; set; }
        public List<string> ageGroups { get; set; }
        public Media media { get; set; }
    }
}
