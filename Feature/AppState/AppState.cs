using BlazorState;
using PoEntry.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PoEntry.Feature.AppState
{
    public partial class AppState : State<AppState>
    {
        public Celin.AIS.AuthResponse AuthResponse { get; set; }
        public List<F0006Row> BPs { get; set; }
        public F0006Row CurrentBP { get; set; }
        public List<V4102XPIRow> CurrentBPRows { get; } = new List<V4102XPIRow>();
        public List<W43032CRow> PoResponse { get; set; } = new List<W43032CRow>();
        public List<W43101Row> PoLines { get; set; }
        public List<Celin.AIS.ErrorWarning> Error { get; set; }
        public string BusyMessage { get; set; }
        protected override void Initialize() { }
        public AppState() { }
    }
}
