using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AGR.Apps.Core.Interfaces;

namespace AGR.Apps.Core.Services
{
    public static class EncryptionService
    {
        //public static IEncryptionService CreateEncryptionService(string key) => new AesEncryptionService(key);
        public static IEncryptionService CreateEncryptionService(string key)
        {
            return new AesEncryptionService(key);
        }

    }
}