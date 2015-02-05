using System;
using System.Runtime.InteropServices;
namespace EID.Wrapper
{
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("c6b093c6-f568-4962-8955-795fc14f34bb")]
    [ComVisible(true)]
    public interface IWrapper
    {
        ICardData GetCardData();
    }
}
