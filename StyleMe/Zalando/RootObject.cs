using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleMe.Zalando
{
    public class RootObject
    {
        public List<Content> content { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}
