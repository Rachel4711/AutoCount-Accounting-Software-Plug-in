using AutoCount.Authentication;
using AutoCount.Invoicing.Purchase.PurchaseInvoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugIn_1.Entity
{
    public class PurchaseInvoices
    {
        private UserSession session { get; set; }
        
        public PurchaseInvoices(UserSession userSession)
        {
            session = userSession;
        }

        internal string CreatePurchaseInvoice()
        {
            PurchaseInvoiceCommand command = PurchaseInvoiceCommand.Create(session, session.DBSetting);

            PurchaseInvoice doc;
            PurchaseInvoiceDetail dtl;

            doc = command.AddNew();
            doc.CreditorCode = "400-B001";
            doc.Description = "Description";

            dtl = doc.AddDetail();
            dtl.ItemCode = "DIGI10";
            dtl.Qty = 10;
            dtl.AddSerialNumberTransactionRecord("sn01", "sn10");
            dtl.SerialNoList = "sn01 => sn10";

            doc.Save();
            return doc.DocNo;
        }
    }
}
