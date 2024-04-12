using System.Data;
using System.Globalization;
using ClosedXML.Excel;
using CsvHelper;
using FiyiStore.Areas.BasicCore;
using FiyiStore.Areas.FiyiStore.Interfaces;
using FiyiStore.Areas.FiyiStore.Entities;
using FiyiStore.Library;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2024
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStore.Areas.FiyiStore.Services
{
    public class ClientService : IClientService
    {
        protected readonly FiyiStoreContext _context;

        public ClientService(FiyiStoreContext context)
        {
            _context = context;
        }

        #region Exportations
        public DateTime ExportAsPDF(Ajax Ajax, string ExportationType)
        {
            var Renderer = new HtmlToPdf();
            DateTime Now = DateTime.Now;
            string RowsAsHTML = "";
            List<Client> lstClient = [];

            if (ExportationType == "All")
            {
                lstClient = _context.Client.ToList();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    Client Client = _context.Client
                                                .Where(x => x.ClientId == Convert.ToInt32(RowChecked))
                                                .FirstOrDefault();
                    lstClient.Add(Client);
                }
            }

            foreach (Client row in lstClient)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Client</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ClientId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Name&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Age&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">EsCasado&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">BornDateTime&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Height&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Email&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ProfilePicture&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">FavouriteColour&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Password&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">PhoneNumber&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Tags&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">About&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">AboutInTextEditor&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">WebPage&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">BornTime&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Colour&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/FiyiStore/Client/Client_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtClient = new();

                //We define another DataTable dtClientCopy to avoid issue related to DateTime conversion
                DataTable dtClientCopy = new();
                dtClientCopy.TableName = "Client";

                #region Define columns for dtClientCopy
                DataColumn dtColumnClientIdFordtClientCopy = new DataColumn();
                    dtColumnClientIdFordtClientCopy.DataType = typeof(string);
                    dtColumnClientIdFordtClientCopy.ColumnName = "ClientId";
                    dtClientCopy.Columns.Add(dtColumnClientIdFordtClientCopy);

                    DataColumn dtColumnActiveFordtClientCopy = new DataColumn();
                    dtColumnActiveFordtClientCopy.DataType = typeof(string);
                    dtColumnActiveFordtClientCopy.ColumnName = "Active";
                    dtClientCopy.Columns.Add(dtColumnActiveFordtClientCopy);

                    DataColumn dtColumnDateTimeCreationFordtClientCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtClientCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtClientCopy.ColumnName = "DateTimeCreation";
                    dtClientCopy.Columns.Add(dtColumnDateTimeCreationFordtClientCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtClientCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtClientCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtClientCopy.ColumnName = "DateTimeLastModification";
                    dtClientCopy.Columns.Add(dtColumnDateTimeLastModificationFordtClientCopy);

                    DataColumn dtColumnUserCreationIdFordtClientCopy = new DataColumn();
                    dtColumnUserCreationIdFordtClientCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtClientCopy.ColumnName = "UserCreationId";
                    dtClientCopy.Columns.Add(dtColumnUserCreationIdFordtClientCopy);

                    DataColumn dtColumnUserLastModificationIdFordtClientCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtClientCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtClientCopy.ColumnName = "UserLastModificationId";
                    dtClientCopy.Columns.Add(dtColumnUserLastModificationIdFordtClientCopy);

                    DataColumn dtColumnNameFordtClientCopy = new DataColumn();
                    dtColumnNameFordtClientCopy.DataType = typeof(string);
                    dtColumnNameFordtClientCopy.ColumnName = "Name";
                    dtClientCopy.Columns.Add(dtColumnNameFordtClientCopy);

                    DataColumn dtColumnAgeFordtClientCopy = new DataColumn();
                    dtColumnAgeFordtClientCopy.DataType = typeof(string);
                    dtColumnAgeFordtClientCopy.ColumnName = "Age";
                    dtClientCopy.Columns.Add(dtColumnAgeFordtClientCopy);

                    DataColumn dtColumnEsCasadoFordtClientCopy = new DataColumn();
                    dtColumnEsCasadoFordtClientCopy.DataType = typeof(string);
                    dtColumnEsCasadoFordtClientCopy.ColumnName = "EsCasado";
                    dtClientCopy.Columns.Add(dtColumnEsCasadoFordtClientCopy);

                    DataColumn dtColumnBornDateTimeFordtClientCopy = new DataColumn();
                    dtColumnBornDateTimeFordtClientCopy.DataType = typeof(string);
                    dtColumnBornDateTimeFordtClientCopy.ColumnName = "BornDateTime";
                    dtClientCopy.Columns.Add(dtColumnBornDateTimeFordtClientCopy);

                    DataColumn dtColumnHeightFordtClientCopy = new DataColumn();
                    dtColumnHeightFordtClientCopy.DataType = typeof(string);
                    dtColumnHeightFordtClientCopy.ColumnName = "Height";
                    dtClientCopy.Columns.Add(dtColumnHeightFordtClientCopy);

                    DataColumn dtColumnEmailFordtClientCopy = new DataColumn();
                    dtColumnEmailFordtClientCopy.DataType = typeof(string);
                    dtColumnEmailFordtClientCopy.ColumnName = "Email";
                    dtClientCopy.Columns.Add(dtColumnEmailFordtClientCopy);

                    DataColumn dtColumnProfilePictureFordtClientCopy = new DataColumn();
                    dtColumnProfilePictureFordtClientCopy.DataType = typeof(string);
                    dtColumnProfilePictureFordtClientCopy.ColumnName = "ProfilePicture";
                    dtClientCopy.Columns.Add(dtColumnProfilePictureFordtClientCopy);

                    DataColumn dtColumnFavouriteColourFordtClientCopy = new DataColumn();
                    dtColumnFavouriteColourFordtClientCopy.DataType = typeof(string);
                    dtColumnFavouriteColourFordtClientCopy.ColumnName = "FavouriteColour";
                    dtClientCopy.Columns.Add(dtColumnFavouriteColourFordtClientCopy);

                    DataColumn dtColumnPasswordFordtClientCopy = new DataColumn();
                    dtColumnPasswordFordtClientCopy.DataType = typeof(string);
                    dtColumnPasswordFordtClientCopy.ColumnName = "Password";
                    dtClientCopy.Columns.Add(dtColumnPasswordFordtClientCopy);

                    DataColumn dtColumnPhoneNumberFordtClientCopy = new DataColumn();
                    dtColumnPhoneNumberFordtClientCopy.DataType = typeof(string);
                    dtColumnPhoneNumberFordtClientCopy.ColumnName = "PhoneNumber";
                    dtClientCopy.Columns.Add(dtColumnPhoneNumberFordtClientCopy);

                    DataColumn dtColumnTagsFordtClientCopy = new DataColumn();
                    dtColumnTagsFordtClientCopy.DataType = typeof(string);
                    dtColumnTagsFordtClientCopy.ColumnName = "Tags";
                    dtClientCopy.Columns.Add(dtColumnTagsFordtClientCopy);

                    DataColumn dtColumnAboutFordtClientCopy = new DataColumn();
                    dtColumnAboutFordtClientCopy.DataType = typeof(string);
                    dtColumnAboutFordtClientCopy.ColumnName = "About";
                    dtClientCopy.Columns.Add(dtColumnAboutFordtClientCopy);

                    DataColumn dtColumnAboutInTextEditorFordtClientCopy = new DataColumn();
                    dtColumnAboutInTextEditorFordtClientCopy.DataType = typeof(string);
                    dtColumnAboutInTextEditorFordtClientCopy.ColumnName = "AboutInTextEditor";
                    dtClientCopy.Columns.Add(dtColumnAboutInTextEditorFordtClientCopy);

                    DataColumn dtColumnWebPageFordtClientCopy = new DataColumn();
                    dtColumnWebPageFordtClientCopy.DataType = typeof(string);
                    dtColumnWebPageFordtClientCopy.ColumnName = "WebPage";
                    dtClientCopy.Columns.Add(dtColumnWebPageFordtClientCopy);

                    DataColumn dtColumnBornTimeFordtClientCopy = new DataColumn();
                    dtColumnBornTimeFordtClientCopy.DataType = typeof(string);
                    dtColumnBornTimeFordtClientCopy.ColumnName = "BornTime";
                    dtClientCopy.Columns.Add(dtColumnBornTimeFordtClientCopy);

                    DataColumn dtColumnColourFordtClientCopy = new DataColumn();
                    dtColumnColourFordtClientCopy.DataType = typeof(string);
                    dtColumnColourFordtClientCopy.ColumnName = "Colour";
                    dtClientCopy.Columns.Add(dtColumnColourFordtClientCopy);

                    
                #endregion

                #region Create another DataTable to copy
                List<Client> lstClient = _context.Client.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("ClientId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                DataTable.Columns.Add("Age", typeof(string));
                DataTable.Columns.Add("EsCasado", typeof(string));
                DataTable.Columns.Add("BornDateTime", typeof(string));
                DataTable.Columns.Add("Height", typeof(string));
                DataTable.Columns.Add("Email", typeof(string));
                DataTable.Columns.Add("ProfilePicture", typeof(string));
                DataTable.Columns.Add("FavouriteColour", typeof(string));
                DataTable.Columns.Add("Password", typeof(string));
                DataTable.Columns.Add("PhoneNumber", typeof(string));
                DataTable.Columns.Add("Tags", typeof(string));
                DataTable.Columns.Add("About", typeof(string));
                DataTable.Columns.Add("AboutInTextEditor", typeof(string));
                DataTable.Columns.Add("WebPage", typeof(string));
                DataTable.Columns.Add("BornTime", typeof(string));
                DataTable.Columns.Add("Colour", typeof(string));
                

                foreach (Client client in lstClient)
                        {
                            DataTable.Rows.Add(
                                client.ClientId,
                        client.Active,
                        client.DateTimeCreation,
                        client.DateTimeLastModification,
                        client.UserCreationId,
                        client.UserLastModificationId,
                        client.Name,
                        client.Age,
                        client.EsCasado,
                        client.BornDateTime,
                        client.Height,
                        client.Email,
                        client.ProfilePicture,
                        client.FavouriteColour,
                        client.Password,
                        client.PhoneNumber,
                        client.Tags,
                        client.About,
                        client.AboutInTextEditor,
                        client.WebPage,
                        client.BornTime,
                        client.Colour
                        
                                );
                        }
                #endregion

                dtClient = DataTable;

                foreach (DataRow DataRow in dtClient.Rows)
                {
                    dtClientCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtClientCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/FiyiStore/Client/Client_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsClient = new();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtClientCopy to avoid issue related to DateTime conversion
                    DataTable dtClientCopy = new();

                    #region Define columns for dtClientCopy
                    DataColumn dtColumnClientIdFordtClientCopy = new DataColumn();
                    dtColumnClientIdFordtClientCopy.DataType = typeof(string);
                    dtColumnClientIdFordtClientCopy.ColumnName = "ClientId";
                    dtClientCopy.Columns.Add(dtColumnClientIdFordtClientCopy);

                    DataColumn dtColumnActiveFordtClientCopy = new DataColumn();
                    dtColumnActiveFordtClientCopy.DataType = typeof(string);
                    dtColumnActiveFordtClientCopy.ColumnName = "Active";
                    dtClientCopy.Columns.Add(dtColumnActiveFordtClientCopy);

                    DataColumn dtColumnDateTimeCreationFordtClientCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtClientCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtClientCopy.ColumnName = "DateTimeCreation";
                    dtClientCopy.Columns.Add(dtColumnDateTimeCreationFordtClientCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtClientCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtClientCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtClientCopy.ColumnName = "DateTimeLastModification";
                    dtClientCopy.Columns.Add(dtColumnDateTimeLastModificationFordtClientCopy);

                    DataColumn dtColumnUserCreationIdFordtClientCopy = new DataColumn();
                    dtColumnUserCreationIdFordtClientCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtClientCopy.ColumnName = "UserCreationId";
                    dtClientCopy.Columns.Add(dtColumnUserCreationIdFordtClientCopy);

                    DataColumn dtColumnUserLastModificationIdFordtClientCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtClientCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtClientCopy.ColumnName = "UserLastModificationId";
                    dtClientCopy.Columns.Add(dtColumnUserLastModificationIdFordtClientCopy);

                    DataColumn dtColumnNameFordtClientCopy = new DataColumn();
                    dtColumnNameFordtClientCopy.DataType = typeof(string);
                    dtColumnNameFordtClientCopy.ColumnName = "Name";
                    dtClientCopy.Columns.Add(dtColumnNameFordtClientCopy);

                    DataColumn dtColumnAgeFordtClientCopy = new DataColumn();
                    dtColumnAgeFordtClientCopy.DataType = typeof(string);
                    dtColumnAgeFordtClientCopy.ColumnName = "Age";
                    dtClientCopy.Columns.Add(dtColumnAgeFordtClientCopy);

                    DataColumn dtColumnEsCasadoFordtClientCopy = new DataColumn();
                    dtColumnEsCasadoFordtClientCopy.DataType = typeof(string);
                    dtColumnEsCasadoFordtClientCopy.ColumnName = "EsCasado";
                    dtClientCopy.Columns.Add(dtColumnEsCasadoFordtClientCopy);

                    DataColumn dtColumnBornDateTimeFordtClientCopy = new DataColumn();
                    dtColumnBornDateTimeFordtClientCopy.DataType = typeof(string);
                    dtColumnBornDateTimeFordtClientCopy.ColumnName = "BornDateTime";
                    dtClientCopy.Columns.Add(dtColumnBornDateTimeFordtClientCopy);

                    DataColumn dtColumnHeightFordtClientCopy = new DataColumn();
                    dtColumnHeightFordtClientCopy.DataType = typeof(string);
                    dtColumnHeightFordtClientCopy.ColumnName = "Height";
                    dtClientCopy.Columns.Add(dtColumnHeightFordtClientCopy);

                    DataColumn dtColumnEmailFordtClientCopy = new DataColumn();
                    dtColumnEmailFordtClientCopy.DataType = typeof(string);
                    dtColumnEmailFordtClientCopy.ColumnName = "Email";
                    dtClientCopy.Columns.Add(dtColumnEmailFordtClientCopy);

                    DataColumn dtColumnProfilePictureFordtClientCopy = new DataColumn();
                    dtColumnProfilePictureFordtClientCopy.DataType = typeof(string);
                    dtColumnProfilePictureFordtClientCopy.ColumnName = "ProfilePicture";
                    dtClientCopy.Columns.Add(dtColumnProfilePictureFordtClientCopy);

                    DataColumn dtColumnFavouriteColourFordtClientCopy = new DataColumn();
                    dtColumnFavouriteColourFordtClientCopy.DataType = typeof(string);
                    dtColumnFavouriteColourFordtClientCopy.ColumnName = "FavouriteColour";
                    dtClientCopy.Columns.Add(dtColumnFavouriteColourFordtClientCopy);

                    DataColumn dtColumnPasswordFordtClientCopy = new DataColumn();
                    dtColumnPasswordFordtClientCopy.DataType = typeof(string);
                    dtColumnPasswordFordtClientCopy.ColumnName = "Password";
                    dtClientCopy.Columns.Add(dtColumnPasswordFordtClientCopy);

                    DataColumn dtColumnPhoneNumberFordtClientCopy = new DataColumn();
                    dtColumnPhoneNumberFordtClientCopy.DataType = typeof(string);
                    dtColumnPhoneNumberFordtClientCopy.ColumnName = "PhoneNumber";
                    dtClientCopy.Columns.Add(dtColumnPhoneNumberFordtClientCopy);

                    DataColumn dtColumnTagsFordtClientCopy = new DataColumn();
                    dtColumnTagsFordtClientCopy.DataType = typeof(string);
                    dtColumnTagsFordtClientCopy.ColumnName = "Tags";
                    dtClientCopy.Columns.Add(dtColumnTagsFordtClientCopy);

                    DataColumn dtColumnAboutFordtClientCopy = new DataColumn();
                    dtColumnAboutFordtClientCopy.DataType = typeof(string);
                    dtColumnAboutFordtClientCopy.ColumnName = "About";
                    dtClientCopy.Columns.Add(dtColumnAboutFordtClientCopy);

                    DataColumn dtColumnAboutInTextEditorFordtClientCopy = new DataColumn();
                    dtColumnAboutInTextEditorFordtClientCopy.DataType = typeof(string);
                    dtColumnAboutInTextEditorFordtClientCopy.ColumnName = "AboutInTextEditor";
                    dtClientCopy.Columns.Add(dtColumnAboutInTextEditorFordtClientCopy);

                    DataColumn dtColumnWebPageFordtClientCopy = new DataColumn();
                    dtColumnWebPageFordtClientCopy.DataType = typeof(string);
                    dtColumnWebPageFordtClientCopy.ColumnName = "WebPage";
                    dtClientCopy.Columns.Add(dtColumnWebPageFordtClientCopy);

                    DataColumn dtColumnBornTimeFordtClientCopy = new DataColumn();
                    dtColumnBornTimeFordtClientCopy.DataType = typeof(string);
                    dtColumnBornTimeFordtClientCopy.ColumnName = "BornTime";
                    dtClientCopy.Columns.Add(dtColumnBornTimeFordtClientCopy);

                    DataColumn dtColumnColourFordtClientCopy = new DataColumn();
                    dtColumnColourFordtClientCopy.DataType = typeof(string);
                    dtColumnColourFordtClientCopy.ColumnName = "Colour";
                    dtClientCopy.Columns.Add(dtColumnColourFordtClientCopy);

                    
                    #endregion

                    dsClient.Tables.Add(dtClientCopy);

                    #region Create DataTable with data from DB
                        Client client = _context.Client
                                                    .Where(x => x.ClientId == Convert.ToInt32(RowChecked))
                                                    .FirstOrDefault();

                        DataTable DataTable = new();
                        DataTable.Columns.Add("ClientId", typeof(string));
                        DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                DataTable.Columns.Add("Age", typeof(string));
                DataTable.Columns.Add("EsCasado", typeof(string));
                DataTable.Columns.Add("BornDateTime", typeof(string));
                DataTable.Columns.Add("Height", typeof(string));
                DataTable.Columns.Add("Email", typeof(string));
                DataTable.Columns.Add("ProfilePicture", typeof(string));
                DataTable.Columns.Add("FavouriteColour", typeof(string));
                DataTable.Columns.Add("Password", typeof(string));
                DataTable.Columns.Add("PhoneNumber", typeof(string));
                DataTable.Columns.Add("Tags", typeof(string));
                DataTable.Columns.Add("About", typeof(string));
                DataTable.Columns.Add("AboutInTextEditor", typeof(string));
                DataTable.Columns.Add("WebPage", typeof(string));
                DataTable.Columns.Add("BornTime", typeof(string));
                DataTable.Columns.Add("Colour", typeof(string));
                
                        
                        DataTable.Rows.Add(
                                client.ClientId,
                        client.Active,
                        client.DateTimeCreation,
                        client.DateTimeLastModification,
                        client.UserCreationId,
                        client.UserLastModificationId,
                        client.Name,
                        client.Age,
                        client.EsCasado,
                        client.BornDateTime,
                        client.Height,
                        client.Email,
                        client.ProfilePicture,
                        client.FavouriteColour,
                        client.Password,
                        client.PhoneNumber,
                        client.Tags,
                        client.About,
                        client.AboutInTextEditor,
                        client.WebPage,
                        client.BornTime,
                        client.Colour
                        
                                );
                        #endregion

                        dtClientCopy = DataTable;

                        foreach (DataRow DataRow in dtClientCopy.Rows)
                        {
                            dsClient.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                }

                for (int i = 0; i < dsClient.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsClient.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }
                Book.SaveAs($@"wwwroot/ExcelFiles/FiyiStore/Client/Client_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<Client> lstClient = new List<Client> { };

            if (ExportationType == "All")
            {
                lstClient = _context.Client.ToList();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    Client Client = _context.Client
                                            .Where(x => x.ClientId == Convert.ToInt32(RowChecked))
                                            .FirstOrDefault();      
                    lstClient.Add(Client);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/FiyiStore/Client/Client_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstClient);
            }

            return Now;
        }
        #endregion
    }
}