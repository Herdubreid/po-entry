namespace PoEntry.Data
{
    public class F4102BPsGroupBy
    {
        public string MCU { get; set; }
    }
    public class F4102BPsOutput : Celin.AIS.AggregationOutput<F4102BPsGroupBy>
    {
        public long COUNT { get; set; }
    }
    public class F4102BPsResponse
    {
        public Celin.AIS.AggregationResponse<F4102BPsOutput> ds_F4102;
    }
    public class F4102Databrowser : Celin.AIS.DatabrowserRequest
    {
        public F4102Databrowser()
        {
            outputType = "GRID_DATA";
            targetName = "F4102";
            targetType = "table";
        }
    }
    public class F4102GroupBPsBrowser : F4102Databrowser
    {
        public F4102GroupBPsBrowser()
        {
            dataServiceType = "AGGREGATION";
            aggregation = new Celin.AIS.Aggregation
            {
                aggregations = new[]
                {
                    new Celin.AIS.AggregationItem
                    {
                        aggregation = "COUNT",
                        column = "*"
                    }
                },
                groupBy = new[]
                {
                    new Celin.AIS.AggregationItem
                    {
                        column = "MCU"
                    }
                }
            };
            query = new Celin.AIS.Query
            {
                matchType = "MATCH_ALL",
                condition = new[]
                {
                    new Celin.AIS.Condition
                    {
                        controlId = "F4102.VEND",
                        @operator = "GREATER",
                        value = new[]
                        {
                            new Celin.AIS.Value
                            {
                                content = "0",
                                specialValueId = "LITERAL"
                            }
                        }

                    }
                }
            };
        }
    }
}
