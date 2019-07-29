using System.Linq;

namespace PoEntry.Data
{
    public class F0006Row
    {
        public string F0006_DL01 { get; set; }
        public string F0006_STYL { get; set; }
        public string F0006_LDM { get; set; }
        public string F0006_MCU { get; set; }
        public string F0006_CO { get; set; }
        public V4102XPIResponse v4102XPIResponse { get; set; }
    }
    public class F0006Response : Celin.AIS.Response
    {
        public Celin.AIS.Form<Celin.AIS.FormData<F0006Row>> fs_DATABROWSE_F0006 { get; set; }
    }
    public class F0006Browser : Celin.AIS.DatabrowserRequest
    {
        public F0006Browser(string[] mcus)
        {
            outputType = "GRID_DATA";
            dataServiceType = "BROWSE";
            targetName = "F0006";
            targetType = "table";
            returnControlIDs = "MCU|STYL|LDM|CO|DL01";
            query = new Celin.AIS.Query
            {
                matchType = "MATCH_ALL",
                condition = new[]
                {
                    new Celin.AIS.Condition
                    {
                        controlId = "F0006.MCU",
                        @operator = "LIST",
                        value = mcus.Select(
                            mcu => new Celin.AIS.Value { content = mcu, specialValueId = "LITERAL" })
                            .ToArray()
                    }
                }
            };
        }
    }
}
