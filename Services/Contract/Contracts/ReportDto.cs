using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class ReportDto
    {
        public string Location { get; set; }
        public int NumberOfContacts { get; set; }
        public int NumberOfPhones { get; set; }
    }
}
