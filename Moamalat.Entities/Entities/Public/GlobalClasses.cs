using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BulkSmsService;

namespace Moamalat.Entities
{
    public class ImportData
    {
        public int BeneficiaryNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string EnteredByEmp { get; set; }
    }

    public class ValidatedData
    {
        public int BeneficiaryCodeId { get; set; }
        public int BeneficiaryNo { get; set; }
        public int CurrentCompanyId { get; set; }
        public string CurrentCompanyName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public int TargetCompanyId { get; set; }
        public string TargetComapnyName { get; set; }
        public string TargetComapnyEngName { get; set; }
        public string Status { get; set; }
        public bool Transfer { get; set; }
        public string EnteredByEmp { get; set; }

    }

    public class SmsData
    {
        public string Sms { get; set; }
        public List<string> Numbers { get; set; }=new List<string>();

        //public async Task SendSms()

        //{

        //    ArrayOfString LNumbers = new ArrayOfString();

        //    foreach (var Number in Numbers)
        //    {
        //        switch (Number.Length)
        //        {
        //            case 8:
        //                LNumbers.Add(Number);
        //                break;

        //            case 11:
        //                LNumbers.Add(Number.Substring(3, 8));
        //                break;

        //            default:

        //                break;
        //        }
               
        //    }

        //    BulkSMSSoapClient bulkSMS = new BulkSMSSoapClient(BulkSMSSoapClient.EndpointConfiguration.IBulkSMSSoap);
        //    //===============================================================
        //    //  Save Sent SMS
        //    //===============================================================
        //    // BeneficiaryData.Entities.SmsSent pSms = new BeneficiaryData.Entities.SmsSent();

        //    //pSms.SendDate = DateTimeToNumber(DateTime.Now);
        //    //pSms.Sms = Message;
        //    //pSms.Mobile = Number;

        //    //new BeneficiaryData.Services.SmsSentService().Insert(pSms);

        //    //==============================================================
        //    await bulkSMS.PushMessageAsync("financeweb", "Financeweb@123", Sms, 64, DateTime.Now, LNumbers, 1);

        //}
    }

    public class ApiResult<T>
    {
        public ResponseStatus StatusId { get; set; } = ResponseStatus.Failed;
        public string? Status { get; set; }
        public T? Result { get; set; }
    }
}
