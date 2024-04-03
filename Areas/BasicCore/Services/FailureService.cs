using ClosedXML.Excel;
using FiyiStore.Areas.BasicCore;
using System.Data;
using CsvHelper;
using System.Globalization;
using FiyiStore.Areas.BasicCore.Interfaces;
using FiyiStore.Areas.BasicCore.Entities;

namespace FiyiStore.Areas.CMSCore.Services
{
    public class FailureService : IFailureService
    {
        protected readonly FiyiStoreContext _context;

        public FailureService(FiyiStoreContext context)
        {
            _context = context;
        }

        #region Exportation
        public DateTime ExportAsPDF()
        {
            var Renderer = new HtmlToPdf();
            DateTime Now = DateTime.Now;
            string RowsAsHTML = "";
            List<Failure> lstFailure = [];

            lstFailure = _context.Failure.ToList();

            foreach (Failure row in lstFailure)
            {
                RowsAsHTML += $@"{row.ToStringOnlyValuesForHTML()}";
            }

            Renderer.RenderHtmlAsPdf($@"<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""88%"" style=""width: 88% !important; min-width: 88%; max-width: 88%;"">
    <tr>
    <td align=""left"" valign=""top"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#1a1a1a"" style=""font-size: 52px; line-height: 55px; font-weight: 300; letter-spacing: -1.5px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #1a1a1a; font-size: 52px; line-height: 55px; font-weight: 300; letter-spacing: -1.5px;"">Mikromatica</span>
        </font>
        <div style=""height: 25px; line-height: 25px; font-size: 23px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#4c4c4c"" style=""font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of User</span>
        </font>
        <div style=""height: 35px; line-height: 35px; font-size: 33px;"">&nbsp;</div>
    </td>
    </tr>
</table>
<br>
<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""width: 100% !important; min-width: 100%; max-width: 100%;"">
    <tr>
        <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Active&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeCreation&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeLastModification&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserCreationId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserLastModificationId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">FantasyName&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Email&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Password&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RoleId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RegistrationToken&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th>
    </tr>
    {RowsAsHTML}
</table>
<br>
<font face=""'Source Sans Pro', sans-serif"" color=""#868686"" style=""font-size: 17px; line-height: 20px;"">
    <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #868686; font-size: 17px; line-height: 20px;"">Printed on: {Now}</span>
</font>
").SaveAs($@"wwwroot/Uploads/FiyiStore/PDFFiles/Failure/Failure_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel()
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            DataTable dtFailure = new();
            dtFailure.TableName = "Failure";

            //We define another DataTable dtFailureCopy to avoid issue related to DateTime conversion
            DataTable dtFailureCopy = new();
            dtFailureCopy.TableName = "Failure";

            #region Define columns for dtFailureCopy
            DataColumn dtColumnFailureIdFordtFailureCopy = new DataColumn();
            dtColumnFailureIdFordtFailureCopy.DataType = typeof(string);
            dtColumnFailureIdFordtFailureCopy.ColumnName = "FailureId";
            dtFailureCopy.Columns.Add(dtColumnFailureIdFordtFailureCopy);

            DataColumn dtColumnActiveFordtFailureCopy = new DataColumn();
            dtColumnActiveFordtFailureCopy.DataType = typeof(string);
            dtColumnActiveFordtFailureCopy.ColumnName = "Active";
            dtFailureCopy.Columns.Add(dtColumnActiveFordtFailureCopy);

            DataColumn dtColumnDateTimeCreationFordtFailureCopy = new DataColumn();
            dtColumnDateTimeCreationFordtFailureCopy.DataType = typeof(string);
            dtColumnDateTimeCreationFordtFailureCopy.ColumnName = "DateTimeCreation";
            dtFailureCopy.Columns.Add(dtColumnDateTimeCreationFordtFailureCopy);

            DataColumn dtColumnDateTimeLastModificationFordtFailureCopy = new DataColumn();
            dtColumnDateTimeLastModificationFordtFailureCopy.DataType = typeof(string);
            dtColumnDateTimeLastModificationFordtFailureCopy.ColumnName = "DateTimeLastModification";
            dtFailureCopy.Columns.Add(dtColumnDateTimeLastModificationFordtFailureCopy);

            DataColumn dtColumnUserCreationIdFordtFailureCopy = new DataColumn();
            dtColumnUserCreationIdFordtFailureCopy.DataType = typeof(string);
            dtColumnUserCreationIdFordtFailureCopy.ColumnName = "UserCreationId";
            dtFailureCopy.Columns.Add(dtColumnUserCreationIdFordtFailureCopy);

            DataColumn dtColumnUserLastModificationIdFordtFailureCopy = new DataColumn();
            dtColumnUserLastModificationIdFordtFailureCopy.DataType = typeof(string);
            dtColumnUserLastModificationIdFordtFailureCopy.ColumnName = "UserLastModificationId";
            dtFailureCopy.Columns.Add(dtColumnUserLastModificationIdFordtFailureCopy);

            DataColumn dtColumnMessageFordtFailureCopy = new DataColumn();
            dtColumnMessageFordtFailureCopy.DataType = typeof(string);
            dtColumnMessageFordtFailureCopy.ColumnName = "Message";
            dtFailureCopy.Columns.Add(dtColumnMessageFordtFailureCopy);

            DataColumn dtColumnEmergencyLevelFordtFailureCopy = new DataColumn();
            dtColumnEmergencyLevelFordtFailureCopy.DataType = typeof(string);
            dtColumnEmergencyLevelFordtFailureCopy.ColumnName = "EmergencyLevel";
            dtFailureCopy.Columns.Add(dtColumnEmergencyLevelFordtFailureCopy);

            DataColumn dtColumnStackTraceFordtFailureCopy = new DataColumn();
            dtColumnStackTraceFordtFailureCopy.DataType = typeof(string);
            dtColumnStackTraceFordtFailureCopy.ColumnName = "StackTrace";
            dtFailureCopy.Columns.Add(dtColumnStackTraceFordtFailureCopy);

            DataColumn dtColumnSourceFordtFailureCopy = new DataColumn();
            dtColumnSourceFordtFailureCopy.DataType = typeof(string);
            dtColumnSourceFordtFailureCopy.ColumnName = "Source";
            dtFailureCopy.Columns.Add(dtColumnSourceFordtFailureCopy);

            DataColumn dtColumnCommentFordtFailureCopy = new DataColumn();
            dtColumnCommentFordtFailureCopy.DataType = typeof(string);
            dtColumnCommentFordtFailureCopy.ColumnName = "Comment";
            dtFailureCopy.Columns.Add(dtColumnCommentFordtFailureCopy);


            #endregion

            dtFailure = GetAllInDataTable();

            foreach (DataRow DataRow in dtFailure.Rows)
            {
                dtFailureCopy.Rows.Add(DataRow.ItemArray);
            }

            var Sheet = Book.Worksheets.Add(dtFailureCopy);

            Sheet.ColumnsUsed().AdjustToContents();

            Book.SaveAs($@"wwwroot/Uploads/FiyiStore/ExcelFiles/Failure/Failure_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");

            return Now;
        }

        public DateTime ExportAsCSV()
        {
            DateTime Now = DateTime.Now;
            List<Failure> lstFailure = [];

            lstFailure = _context.Failure.ToList();

            using (var Writer = new StreamWriter($@"wwwroot/Uploads/FiyiStore/CSVFiles/Failure/Failure_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstFailure);
            }

            return Now;
        } 
        #endregion

        public DataTable GetAllInDataTable()
        {
            throw new NotImplementedException();
        }
    }
}
