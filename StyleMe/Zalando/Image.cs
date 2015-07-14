using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleMe.Zalando
{
    public class Image
    {
        public int orderNumber { get; set; }
        public string type { get; set; }
        public string thumbnailHdUrl { get; set; }
        public string smallUrl { get; set; }
        public string smallHdUrl { get; set; }
        public string mediumUrl { get; set; }
        public string mediumHdUrl { get; set; }
        public string largeUrl { get; set; }
        public string largeHdUrl { get; set; }
    }
}
