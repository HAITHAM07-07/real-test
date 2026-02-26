using DATABASE_DVLD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_DVLD
{
    public class clsLocalDrivingLicenseApplications_View
    {
        public int LocalDrivingLicenseApplicationid { get; set; }
        public int passedtestcount { get;set;}
        clsLocalDrivingLicenseApplications_View(int localDrivingLicenseApplicationid    ,int passedtestcount)
        {
            this.passedtestcount = passedtestcount;
            this.LocalDrivingLicenseApplicationid=localDrivingLicenseApplicationid;
        }

        static public clsLocalDrivingLicenseApplications_View Find(int localappid)
        {


            int passedtestcount = 0;
            if(DATALocalDrivingLicenseApplications_Viewcs.Find(localappid,ref passedtestcount) == true)
            {
                return new clsLocalDrivingLicenseApplications_View(localappid, passedtestcount);

            }
            else
            {
                return null;
            }
        }
       

    }
}
