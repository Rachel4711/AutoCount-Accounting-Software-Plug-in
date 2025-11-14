using AutoCount.Authentication;
using AutoCount.Invoicing;
using AutoCount.Invoicing.Sales;
using AutoCount.Invoicing.Sales.Invoice;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace PlugIn_1.Entity
{
    class TransferDetailInfo
    {
        public string item_code { get; set; }
        public string Uom { get; set; }
        public decimal Qty { get; set; }
    }
        
    public class SalesInvoice
    {
        private UserSession session;

        public SalesInvoice(UserSession userSession)
        {
            session = userSession;
        }

        internal bool CreateUpdateSalesInvoice(string debtor_code, string full_transfer_doc)
        {
            string doc_no = "TI-001";

            InvoiceCommand command = InvoiceCommand.Create(session, session.DBSetting);
            Invoice doc = command.Edit(doc_no);

            bool transferSuccess = false;

            if (doc == null)
            {
                doc = command.AddNew();
                doc.DocNo = doc_no;
                doc.DebtorCode = debtor_code;
                doc.DocDate = DateTime.Today.Date;
                doc.Description = "Generated full transfer document from SO.";

                transferSuccess = doc.FullTransfer(
                    new string[] { full_transfer_doc },
                    TransferFrom.SalesOrder,
                    FullTransferOption.FullDetails);



                doc.Save();
            }

            return transferSuccess;
        }

        internal void CreateUpdateSalesInvoice_PartialSalesOrder(string debtor_code, string doc_no, List<TransferDetailInfo> transferDetailInfos)
        {
            InvoiceCommand command = InvoiceCommand.Create(session, session.DBSetting);
            Invoice doc = command.Edit(doc_no);

            if (doc == null)
            {
                doc = command.AddNew();
                doc.DocNo = doc_no;
                doc.DebtorCode = debtor_code;
                doc.DocDate = DateTime.Today.Date;
                doc.Description = "Generated full transfer document from SO.";

                transferDetailInfos.ForEach(info =>
                    doc.PartialTransfer(TransferFrom.SalesOrder, doc_no, info.item_code, info.Uom, info.Qty, 0)
                    );

                doc.Save();
            }

        }
    }
}
