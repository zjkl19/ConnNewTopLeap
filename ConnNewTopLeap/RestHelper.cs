using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;
using System.Text;
using System.Threading.Tasks;

namespace ConnNewTopLeap
{
    public class RestHelper
    {
        
        public static string GetContent(ReadOnlyCollection<Cookie> cookieCollection)
        {
            RestClient client;
            RestRequest request;
            IRestResponse resp = null;

            client = new RestClient("http://192.168.5.204");

            request = new RestRequest($"/OASYS/OperationData/UCXM/PGProjectList.aspx" +
                $"?ModuleId=XM017CX&CurrMenuID=M004_019&TopId=M004&GetUnit=02&GetDept=02CB&FH=%3E=&FCSJ=_&FHSJ=_", Method.GET);
            foreach(var c in cookieCollection)
            {
                request.AddCookie(c.Name, c.Value);
            }
            
            try
            {
                resp = client.Execute(request);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return resp.Content;
        }
    }
}
