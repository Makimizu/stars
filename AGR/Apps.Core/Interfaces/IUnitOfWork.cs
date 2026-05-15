using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGR.Apps.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IIdentityService IdentityService { get; }
        IEncryptionService EncryptionService { get; }
        ISystemConfigurationService SystemConfigurationService { get; }
    }
}
