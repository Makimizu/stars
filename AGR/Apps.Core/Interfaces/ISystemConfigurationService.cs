using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGR.DataAccess.EFCore.Interfaces;

namespace AGR.Apps.Core.Interfaces
{
    public interface ISystemConfigurationService : IRepository
    {
        string GetSysconfigValue(string sysCat, string subCat, string sysCode = "");
    }
}
