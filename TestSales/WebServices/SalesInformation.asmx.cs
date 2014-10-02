using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using TestSales.Models.BO;
using TestSales.Models.DA;
using TestSales.Models.DA.dsSalesTableAdapters;

namespace TestSales.WebServices
{
    [WebService(Namespace = "~/WebServices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class SalesInformation : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true, XmlSerializeString = false)]
        public string GetSalesByYear(int Year)
        {

            List<SalesPerson> SalesDetails = new List<SalesPerson>();

            GETSalesPeopleTableAdapter ta = new GETSalesPeopleTableAdapter();
            dsSales.GETSalesPeopleDataTable dt = ta.GetData();


            SalesPerson obj;

            foreach (dsSales.GETSalesPeopleRow row in dt.Rows)
            {
                obj = new SalesPerson();
                obj.Name = row.Name;
                obj.LastName = row.LastName;
                obj.Email = row.Email;
                obj.DateOfBirth = row.BirthDate.ToString("MM-dd-yyyy");
                obj.sales = new Dictionary<string, int>();

                GETSalesBySalesPersonTableAdapter taSales = new GETSalesBySalesPersonTableAdapter();
                dsSales.GETSalesBySalesPersonDataTable dtSales = taSales.GetData(row.iSalesPerson, Year);

                foreach (dsSales.GETSalesBySalesPersonRow SalesRow in dtSales.Rows)
                {
                    obj.sales.Add(SalesRow.Month, SalesRow.Qty);
                }

                SalesDetails.Add(obj);
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(SalesDetails);
                
            //~/Webservices/SalesInformation.asmx/Get2013Sales
        }
    }
}
