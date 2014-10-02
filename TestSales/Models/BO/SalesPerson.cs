using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestSales.Models.DA;

namespace TestSales.Models.BO
{
    [Serializable]
    public class SalesPerson
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public Dictionary<string,int> sales {get; set;}
    }
}