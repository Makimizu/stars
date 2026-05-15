using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HEALTH.OWLEXA_enrollment;

namespace HEALTH.Class
{
    public class TPA_OWLEXA
    {
        
        public static string GetMutationList(string param1, string param2)
        {
            OWLEXA_enrollment.MemberImportDto mbdt = new MemberImportDto();
            OWLEXA_enrollment.EnrollmentWebService ws = new EnrollmentWebService();

            //ws.enrollmentSuspend(

            string result = "";
            return result;
        }
    }

    
}