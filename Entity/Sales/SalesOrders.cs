using AutoCount.Authentication;
using AutoCount.Invoicing;
using AutoCount.Invoicing.Sales.SalesOrder;
using AutoCount.GeneralMaint.DocumentNoFormat;
using System;
using System.Collections.Generic;
using System.Data;
using AutoCount.Document;

namespace PlugIn_1.Entity
{
    internal class SalesOrders
    {
        private UserSession session;

        internal SalesOrders(UserSession userSession)
        {
            session = userSession;
        }

        internal void CreateUpdateSalesOrder()
        {
            SalesOrderCommand command = SalesOrderCommand.Create(session, session.DBSetting);

            string doc_no = "SO660011";

            SalesOrder doc = command.Edit(doc_no);
            SalesOrderDetail dtl;

            if (doc == null)
            {
                doc = command.AddNew();
                doc.DocNo = doc_no;
                doc.DocDate = DateTime.Today.Date;
                doc.DebtorCode = "300-B001";
            }
            else
            {
                doc.ClearDetails();
            }

            doc.Description = "Generated Sales Order";

            dtl = doc.AddDetail();
            dtl.ItemCode = "Item101";
            dtl.Qty = 1;
            dtl.UnitPrice = 30m;

            doc.Save();
        }

        internal string CreateSalesOrderWithRunNum()
        {
            SalesOrderCommand command = SalesOrderCommand.Create(session, session.DBSetting);

            string doc_no = "SO101101";

            SalesOrder doc = command.Edit(doc_no);
            SalesOrderDetail dtl;

            doc = command.AddNew();
            doc.RefDocNo = doc_no;
            doc.DocDate = DateTime.Today.Date;
            doc.DebtorCode = "300-B001";

            doc.Description = "Generated Sales Order";

            dtl = doc.AddDetail();
            dtl.ItemCode = "Item101";
            dtl.Qty = 1;
            dtl.UnitPrice = 30m;

            doc.Save();

            return doc.DocNo;
        }

        internal string CreateUpdateSalesOrderWithItemPackage()
        {
            SalesOrderCommand command = SalesOrderCommand.Create(session, session.DBSetting);

            string docNoFormatName = CreateSalesOrderNumberFormat();
            SalesOrder doc;
            SalesOrderDetail dtl;
            InvoicingPackageDetailRecord packageDetailRecord;

            doc = command.AddNew();
            doc.DocNoFormatName = docNoFormatName;
            doc.DebtorCode = "300-B001";
            doc.DocDate= DateTime.Today.Date;
            doc.Description = "Generate Sales Order with Item Package";

            dtl = doc.AddDetail();
            dtl.ItemCode = "Item101";
            dtl.Qty = 2;
            dtl.UnitPrice = 30m;

            dtl = doc.AddPackage("PackA");
            dtl.Desc2 = "Package A Item";
            dtl.Qty = 2;
            dtl.Discount = "3%";

            packageDetailRecord = doc.AddPackageDetail(dtl.DtlKey);
            packageDetailRecord.ItemCode = "APPLE MAC PRO 8";
            packageDetailRecord.Qty = 2;
            packageDetailRecord.UnitPrice = 30m;

            doc.AddDiscountDetail("Promotion 10%", 10, "", doc.DetailCount - 1);

            doc.Save();
            return doc.DocNo;
        }

        internal Dictionary<string, string> GetOutstandingSalesOrderDocNo()
        {
            DataTable dataTable = GetOutstandingSalesOrderDataTable(session);
            Dictionary<string, string> dict_DocNo = new Dictionary<string, string>();

            string doc_no, debtor_code;

            foreach (DataRow row in dataTable.Rows)
            {
                doc_no = row.Field<string>("DocNo");
                debtor_code = row.Field<string>("DebtorCode");

                if (!dict_DocNo.ContainsKey(doc_no))
                {
                    dict_DocNo.Add(doc_no, debtor_code);
                }
            }

            return dict_DocNo;
        }

        private DataTable GetOutstandingSalesOrderDataTable(UserSession userSession)
        {
            DataSet ds_OutstandSO = new DataSet("Outstanding SO Data Set");
            DataTable dt_master = new DataTable("Master");
            DataTable dt_detail = new DataTable("Detail");

            ds_OutstandSO.Tables.Add(dt_master);
            ds_OutstandSO.Tables.Add(dt_detail);

            SalesOrderOutstandingReportCommand command = SalesOrderOutstandingReportCommand.Create(userSession);

            SalesOrderOutStandingReportingCriteria criteria = new SalesOrderOutStandingReportingCriteria();

            command.BasicSearch(criteria, "DocNo, DebtorCode, DocDate", ds_OutstandSO, "");

            //ds_OutstandSO.Relations.Add("MasterDetailRelation", dt_master.Columns["DocKey"], dt_detail.Columns["DocKey"], false);

            return dt_master;
        }

        private string CreateSalesOrderNumberFormat()
        {
            string doc_type = DocumentType.SalesOrder;
            string name = "SO Online Sales";
            
            DocumentNoMaintenance command = DocumentNoMaintenance.CreateDocumentNoMaint(session, session.DBSetting);

            command.Load();

            DataTable tblAllDocNO = command.DataTableDocumentNo;
            DataRow nRow = tblAllDocNO.NewRow();

            nRow["Name"] = name;
            nRow["DocType"] = doc_type;
            nRow["NextNumber"] = 1;
            nRow["Format"] = "S{@yyMM}/<00000>";
            nRow["Sample"] = "S2202/00001";
            nRow["IsDefault"] = "F";
            nRow["OneMonthoneSet"] = "F";


            try
            {
                tblAllDocNO.Rows.Add(nRow);
                command.Save(null);
            }
            catch (ConstraintException)
            {

            }

            return name;
        }
    }
}
