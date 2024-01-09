using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Library
{

    public class Dispensary
    {
        string _rownum;
        string _fullName;
        string _shortName;
        string _admArea;
        string _district;
        Address _address;
        string _chiefName;
        string _chiefPosition;
        string _chiefGender;
        string _chiefPhone;
        string _email;
        string _closeFlag;
        string _closeReason;
        string _closeDate;
        string _reopenDate;
        string _workingHours;
        string _clarificationOfWorkingHours;
        string _specialization;
        string _beneficialDrugPrescriptions;
        string _extraInfo;
        string _pointX;
        string _pointY;
        string _globalID;

        public string Specialization
        {
            get { return _specialization; }
        }

        public string ChiefPosition
        {
            get { return _chiefPosition; }
        }

        public string AdmArea
        {
            get { return _admArea; }
        }

        public string District
        {
            get { return _district; }
        }
        public Dispensary(string postalCode, string street, string houseNubmer, string phone, string fax,
            string rownum, string fullName, string shortName, string admArea, string district,
            string chiefName, string chiefPosition, string chiefGender, string chiefPhone,
            string email, string closeFlag, string closeReason, string closeDate, string reopenDate,
            string workingHours, string clarificationOfWorkingHours, string specialization,
            string beneficialDrugPrescriptions, string extraInfo, string pointX, string pointY, string globalID)
        {
            if (postalCode == null || street == null || houseNubmer == null || phone == null || fax == null ||
            rownum == null || fullName == null || shortName == null || admArea == null || district == null ||
            chiefName == null || chiefPosition == null || chiefGender == null || chiefPhone == null ||
            email == null || closeFlag == null || closeReason == null || closeDate == null || reopenDate == null ||
            workingHours == null || clarificationOfWorkingHours == null || specialization == null ||
            beneficialDrugPrescriptions == null || extraInfo == null || pointX == null || pointY == null || globalID == null) {
                throw new ArgumentNullException();
            }
            _rownum = rownum;
            _fullName = fullName;
            _shortName = shortName;
            _admArea = admArea;
            _district = district;
            _address = new Address(postalCode, street, houseNubmer, phone, fax);
            _chiefName = chiefName;
            _chiefPosition = chiefPosition;
            _chiefGender = chiefGender;
            _chiefPhone = chiefPhone;
            _email = email;
            _closeFlag = closeFlag;
            _closeReason = closeReason;
            _closeDate = closeDate;
            _reopenDate = reopenDate;
            _workingHours = workingHours;
            _clarificationOfWorkingHours = clarificationOfWorkingHours;
            _specialization = specialization;
            _beneficialDrugPrescriptions = beneficialDrugPrescriptions;
            _extraInfo = extraInfo;
            _pointX = pointX;
            _pointY = pointY;
            _globalID = globalID;
        }

        public Dispensary()
        {
            _rownum = "";
            _fullName = "";
            _shortName = "";
            _admArea = "";
            _district = "";
            _address = new Address();
            _chiefName = "";
            _chiefPosition = "";
            _chiefGender = "";
            _chiefPhone = "";
            _email = "";
            _closeFlag = "";
            _closeReason = "";
            _closeDate = "";
            _reopenDate = "";
            _workingHours = "";
            _clarificationOfWorkingHours = "";
            _specialization = "";
            _beneficialDrugPrescriptions = "";
            _extraInfo = "";
            _pointX = "";
            _pointY = "";
            _globalID = "";
        }
    }
}
