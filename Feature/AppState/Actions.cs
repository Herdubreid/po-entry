using MediatR;
using PoEntry.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEntry.Feature.AppState
{
    public class ClearErrorAction : IRequest<AppState> { }
    public class CreatePoAction : IRequest<AppState> { }
    public class ToggleSelectIBPAction : IRequest<AppState>
    {
        public string LITM { get; set; }
    }
    public class SelectBPAction : IRequest<AppState>
    {
        public string BP { get; set; }
    }
    public class V4102XPIAction : IRequest<AppState>
    {
        public string BP { get; set; }
    }
    public class AuthResponseAction : IRequest<AppState>
    {
        public Celin.AIS.AuthResponse AuthResponse { get; set; }
    }
    public class F4102BPsAction : IRequest<AppState> { }
}
