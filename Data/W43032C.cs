namespace PoEntry.Data
{
    public class W43032CRow
    {
        public int z_DOCO_6 { get; set; }
        public string z_MCU_9 { get; set; }
        public string z_KCOO_8 { get; set; }
        public string z_DCTO_7 { get; set; }
        public int z_AN8_10 { get; set; }
        public string z_DRQJ_11 { get; set; }
    }
    public class W43032CResponse : Celin.AIS.FormResponse
    {
        public Celin.AIS.Form<Celin.AIS.FormData<W43032CRow>> fs_P43032_W43032C { get; set; }
    }
}
