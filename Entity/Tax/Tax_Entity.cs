using AutoCount.Authentication;
using AutoCount.Tax.TaxEntityMaintenance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlugIn_1.PlugInMain;

namespace PlugIn_1.Entity
{
    internal class Tax_Entity
    {
        public Tax_Entity() { }

        public bool CreateNewTaxEntity(UserSession userSession)
        {
            AutoCount.Tax.TaxEntityMaintenance.TaxEntityCommand cmd =
                AutoCount.Tax.TaxEntityMaintenance.TaxEntityCommand.Create(userSession);
            AutoCount.Tax.TaxEntityMaintenance.TaxEntity taxEntity = cmd.New();

            taxEntity.TaxCategory = AutoCount.Tax.TaxCategory.Private;
            taxEntity.TIN = "001200335";
            //Business Reg. No = IdentityNo
            taxEntity.IdentityNo = "11122333445";
            taxEntity.MSICCode = "00000";
            taxEntity.BusinessActivityDesc = "Trading";
            taxEntity.Name = "Company A";
            taxEntity.TradeName = "Trader A";
            taxEntity.Phone = "12345678";
            taxEntity.EmailAddress = "test@test.com";
            taxEntity.Address = "test1";
            taxEntity.PostCode = "12345";
            taxEntity.City = "12345";
            taxEntity.StateCode = "12";
            taxEntity.CountryCode = "123";

            return taxEntity.Save();
        }

        public TaxEntity GetTaxEntity(UserSession session, TaxEntityData taxEntityData)
        {
            int taxEntityID = (int)AutoCount.Tax.TaxEntityHelper.GetTaxEntityID(session.DBSetting, taxEntityData.TIN, taxEntityData.IdentityNo, taxEntityData.Name);

            TaxEntityCommand cmd = TaxEntityCommand.Create(Program.session);
            return cmd.Edit(taxEntityID);
        }

        public TaxEntityData GetTaxEntityData(string tin, string identityNo, string name)
        {
            UserSession session = Program.session;

            int? taxEntityID = AutoCount.Tax.TaxEntityHelper.GetTaxEntityID(session.DBSetting, tin, identityNo, name);

            AutoCount.Tax.TaxEntityMaintenance.TaxEntityCommand cmd =
               AutoCount.Tax.TaxEntityMaintenance.TaxEntityCommand.Create(session);

            if (taxEntityID != null)
            {
                DataRow row = AutoCount.Tax.TaxEntityHelper.GetTaxEntityRecord(session.DBSetting, taxEntityID.Value);

                TaxEntityData tax = new TaxEntityData
                {
                    TaxEntityID = row.Field<int>("TaxEntityID"),
                    Name = row.Field<string>("Name"),
                    IdentityNo = row.Field<string>("IdentityNo"),
                    TaxRegisterNo = row.Field<string>("TaxRegisterNo"),
                    //FullTIN = row.Field<string>("FullTIN"),
                    TIN = row.Field<string>("TIN"),
                    TaxBranchID = row.Field<string>("TaxBranchID"),
                    SSTRegisterNo = row.Field<string>("SSTRegisterNo")
                };

                return tax;
            }
            else
            {
                throw new Exception("The data cannot be found, please try with another value.");
            }
        }

        public class FormCustomTaxEntityView : FormMYTaxEntityEdit
        {
            //namespace of TaxEntity is AutoCount.Tax.TaxEntityMaintenance
            //base class arguments: TaxEntityFormAction action, TaxEntity entity, bool disableDefaultTin
            public FormCustomTaxEntityView(TaxEntity taxEntity) : base(TaxEntityFormAction.View, taxEntity, true)
            {
            }
        }

        public class TaxEntityData
        {
            public int TaxEntityID { get; set; }
            public string Name { get; set; }
            public string IdentityNo { get; set; }
            public string TaxRegisterNo { get; set; }
            public string TIN { get; set; }
            public string TaxBranchID { get; set; }
            public string SSTRegisterNo { get; set; }
        }
    }
}
