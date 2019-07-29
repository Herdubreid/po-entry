using System.Collections.Generic;
using System.Linq;

namespace PoEntry.Data
{
    public class W43101Row
    {
        public string z_UOM2_134 { get; set; }
        public string z_DSC1_44 { get; set; }
        public string z_DL01_79 { get; set; }
        public int z_AN8_77 { get; set; }
        public string z_UOM3_42 { get; set; }
        public string z_UITM_35 { get; set; }
        public decimal z_UORGE1_36 { get; set; }
        public string z_UOM_38 { get; set; }
    }
    public class W43101Response : W43032CResponse
    {
        public Celin.AIS.Form<Celin.AIS.FormData<W43101Row>> fs_P43101_W43101B { get; set; }
    }
    public class W43101BRequest : Celin.AIS.FormRequest
    {
        public W43101BRequest(string bp, List<V4102XPIRow> rows)
        {
            outputType = "GRID_DATA";
            formServiceAction = "U";
            formName = "P43101_W43101B";
            version = "ZJDE0001";
            formActions = new Celin.AIS.Action[]
            {
                new Celin.AIS.FormAction
                {
                    controlID = "28",
                    value = bp,
                    command = "SetControlValue"
                },
                new Celin.AIS.GridAction
                {
                    gridAction = new Celin.AIS.GridInsert
                    {
                        gridID = "1",
                        gridRowInsertEvents = rows.Select((r, i) =>
                        new Celin.AIS.RowEvent
                        {
                            rowNumber = i,
                            gridColumnEvents = new[]
                            {
                                new Celin.AIS.ColumnEvent
                                {
                                    command = "SetGridCellValue",
                                    value = r.F4102_LITM,
                                    columnID = "35"
                                },
                                new Celin.AIS.ColumnEvent
                                {
                                    command = "SetGridCellValue",
                                    value = r.Qty.ToString(),
                                    columnID = "36"
                                }
                            }
                        })
                    }
                },
                new Celin.AIS.FormAction
                {
                    controlID = "12",
                    command = "DoAction"
                }
            };
            stopOnWarning = "FALSE";
        }
    }
}
