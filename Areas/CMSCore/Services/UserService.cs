using ClosedXML.Excel;
using FiyiStore.Areas.BasicCore;
using FiyiStore.Areas.CMSCore.Entities;
using FiyiStore.Areas.CMSCore.Interfaces;
using System.Data;
using CsvHelper;
using System.Globalization;

namespace FiyiStore.Areas.CMSCore.Services
{
    public class UserService : IUserService
    {
        protected readonly FiyiStoreContext _context;

        public UserService(FiyiStoreContext context)
        {
            _context = context;
        }

        #region Exportation
        public DateTime ExportAsPDF()
        {
            var Renderer = new HtmlToPdf();
            DateTime Now = DateTime.Now;
            string RowsAsHTML = "";
            List<User> lstUser = [];

            lstUser = _context.User.ToList();

            foreach (User row in lstUser)
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
").SaveAs($@"wwwroot/Uploads/FiyiStore/PDFFiles/User/User_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel()
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            DataTable dtUser = new();
            dtUser.TableName = "User";

            //We define another DataTable dtUserCopy to avoid issue related to DateTime conversion
            DataTable dtUserCopy = new();
            dtUserCopy.TableName = "User";

            #region Define columns for dtUserCopy
            DataColumn dtColumnUserIdFordtUserCopy = new DataColumn();
            dtColumnUserIdFordtUserCopy.DataType = typeof(string);
            dtColumnUserIdFordtUserCopy.ColumnName = "UserId";
            dtUserCopy.Columns.Add(dtColumnUserIdFordtUserCopy);

            DataColumn dtColumnActiveFordtUserCopy = new DataColumn();
            dtColumnActiveFordtUserCopy.DataType = typeof(string);
            dtColumnActiveFordtUserCopy.ColumnName = "Active";
            dtUserCopy.Columns.Add(dtColumnActiveFordtUserCopy);

            DataColumn dtColumnDateTimeCreationFordtUserCopy = new DataColumn();
            dtColumnDateTimeCreationFordtUserCopy.DataType = typeof(string);
            dtColumnDateTimeCreationFordtUserCopy.ColumnName = "DateTimeCreation";
            dtUserCopy.Columns.Add(dtColumnDateTimeCreationFordtUserCopy);

            DataColumn dtColumnDateTimeLastModificationFordtUserCopy = new DataColumn();
            dtColumnDateTimeLastModificationFordtUserCopy.DataType = typeof(string);
            dtColumnDateTimeLastModificationFordtUserCopy.ColumnName = "DateTimeLastModification";
            dtUserCopy.Columns.Add(dtColumnDateTimeLastModificationFordtUserCopy);

            DataColumn dtColumnUserCreationIdFordtUserCopy = new DataColumn();
            dtColumnUserCreationIdFordtUserCopy.DataType = typeof(string);
            dtColumnUserCreationIdFordtUserCopy.ColumnName = "UserCreationId";
            dtUserCopy.Columns.Add(dtColumnUserCreationIdFordtUserCopy);

            DataColumn dtColumnUserLastModificationIdFordtUserCopy = new DataColumn();
            dtColumnUserLastModificationIdFordtUserCopy.DataType = typeof(string);
            dtColumnUserLastModificationIdFordtUserCopy.ColumnName = "UserLastModificationId";
            dtUserCopy.Columns.Add(dtColumnUserLastModificationIdFordtUserCopy);

            DataColumn dtColumnEmailFordtUserCopy = new DataColumn();
            dtColumnEmailFordtUserCopy.DataType = typeof(string);
            dtColumnEmailFordtUserCopy.ColumnName = "Email";
            dtUserCopy.Columns.Add(dtColumnEmailFordtUserCopy);

            DataColumn dtColumnPasswordFordtUserCopy = new DataColumn();
            dtColumnPasswordFordtUserCopy.DataType = typeof(string);
            dtColumnPasswordFordtUserCopy.ColumnName = "Password";
            dtUserCopy.Columns.Add(dtColumnPasswordFordtUserCopy);

            DataColumn dtColumnRoleIdFordtUserCopy = new DataColumn();
            dtColumnRoleIdFordtUserCopy.DataType = typeof(string);
            dtColumnRoleIdFordtUserCopy.ColumnName = "RoleId";
            dtUserCopy.Columns.Add(dtColumnRoleIdFordtUserCopy);
            #endregion

            dtUser = GetAllInDataTable();

            foreach (DataRow DataRow in dtUser.Rows)
            {
                dtUserCopy.Rows.Add(DataRow.ItemArray);
            }

            var Sheet = Book.Worksheets.Add(dtUserCopy);

            Sheet.ColumnsUsed().AdjustToContents();

            Book.SaveAs($@"wwwroot/Uploads/FiyiStore/ExcelFiles/User/User_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");

            return Now;
        }

        public DateTime ExportAsCSV()
        {
            DateTime Now = DateTime.Now;
            List<User> lstUser = [];

            lstUser = _context.User.ToList();

            using (var Writer = new StreamWriter($@"wwwroot/Uploads/FiyiStore/CSVFiles/User/User_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstUser);
            }

            return Now;
        } 
        #endregion

        public DataTable GetAllInDataTable()
        {
            try
            {
                List<User> lstUser = _context.User.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("UserId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Email", typeof(string));
                DataTable.Columns.Add("Password", typeof(string));
                DataTable.Columns.Add("RoleId", typeof(string));


                foreach (User user in lstUser)
                {
                    DataTable.Rows.Add(
                        user.UserId,
                        user.Active,
                        user.DateTimeCreation,
                        user.DateTimeLastModification,
                        user.UserCreationId,
                        user.UserLastModificationId,
                        user.Email,
                        user.Password,
                        user.RoleId

                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }
    }
}
