using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusMasterFramework
{
    public enum DisplayFormat
    {
        Binary,
        Hex,
        Integer,
        Float,
        InvFloat
    }

    public enum CommunicationMode
    {
        TCP,
        UDP,
        RTU
    }

    public enum Function
    {
        CoilStatus,
        InputStatus,
        HoldingRegister,
        InputRegister
    }
}
