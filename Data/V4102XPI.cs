using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEntry.Data
{
    public class V4102XPIRow
    {
        public string F4102_MCU { get; set; }
        public string F4102_LITM { get; set; }
        public string F4101_DSC1 { get; set; }
        public string F4101_DSC2 { get; set; }
        public bool Selected { get; set; }
        public int? Qty { get; set; }
    }
    public class V4102XPIResponse : Celin.AIS.Response
    {
        public Celin.AIS.Form<Celin.AIS.FormData<V4102XPIRow>> fs_DATABROWSE_V4102XPI { get; set; }
    }
    public class V4102XPIBrowser : Celin.AIS.DatabrowserRequest
    {
        public V4102XPIBrowser(string bp)
        {
            outputType = "GRID_DATA";
            dataServiceType = "BROWSE";
            targetName = "V4102XPI";
            targetType = "view";
            returnControlIDs = "F4102.LITM|MCU|DSC1|DSC2";
            maxPageSize = "5000";
            query = new Celin.AIS.Query
            {
                matchType = "MATCH_ALL",
                condition = new[]
                {
                    new Celin.AIS.Condition
                    {
                        controlId = "F4102.MCU",
                        @operator = "EQUAL",
                        value = new[]
                        {
                            new Celin.AIS.Value
                            {
                                content = bp,
                                specialValueId = "LITERAL"
                            }
                        }
                    }
                }
            };
        }
    }
}
