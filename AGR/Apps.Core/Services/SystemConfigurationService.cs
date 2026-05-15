using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AGR.Apps.Core.Interfaces;
using AGR.Apps.Core.Models;
using AGR.DataAccess.EFCore.Interfaces;
using AGR.DataAccess.EFCore.Repositories;

namespace AGR.Apps.Core.Services
{
    public partial class SystemConfigurationService : EFRepository, ISystemConfigurationService
    {
        #region Private Variables
        private readonly IRepository repository;
        #endregion                
        public SystemConfigurationService(string connString, IRepository repository) : base(connString)
        {
            this.repository = repository;
        }

        public string GetSysconfigValue(string sysCat, string subCat, string sysCode = "")
        {
            string query = string.Format(@"SELECT SystemCategory, SystemSubCategory, SystemCode, SystemValue 
                                            FROM MARKETING.dbo.MasterSystemConfig 
                                            WHERE SystemCategory = @sysCat 
                                            AND SystemSubCategory = @subCat
                                            AND SystemCode = @syscode");
            var param = new Dictionary<string, object>
            {
                {"@sysCat", sysCat },
                {"@subCat", subCat },
                {"@sysCode", sysCode },
            };

            var sysValue = (repository.QueryList<SystemConfigModel>(query, param, false)).FirstOrDefault().SystemValue.ToString();
            return sysValue;
        }
    }
}