using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AGR.Apps.Core.Interfaces;
using AGR.Apps.Core.Services;
using AGR.DataAccess.EFCore.Interfaces;
using AGR.DataAccess.EFCore.Repositories;

namespace AGR.Apps.Core.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        IRepository repository;
        public UnitOfWork(string connString, string sharedScreet = "")
        {
            repository = new EFRepository(connString);
            IdentityService = new IdentityService(connString, repository);
            SystemConfigurationService = new SystemConfigurationService(connString, repository);
            EncryptionService = new AesEncryptionService(sharedScreet);
        }

        public IEncryptionService EncryptionService { get; private set; }

        public IIdentityService IdentityService { get; private set; }

        public ISystemConfigurationService SystemConfigurationService { get; private set; }
    }
}