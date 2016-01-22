using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;
using System.Web;

namespace Tools
{
    public class NPOIHelper
    {
        /// <summary>
        /// 导出Excel到文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="SheetName"></param>
        /// <param name="dt"></param>
        /// <param name="exportColList"></param>
        /// <returns></returns>
        public static string CreateXls(string filePath, string SheetName, DataTable dt, List<string[]> exportColList)
        {
            //创建工作薄
            HSSFWorkbook wk = new HSSFWorkbook();

            //创建一个名称为mySheet的表
            ISheet sheet = wk.CreateSheet(SheetName);
            IRow headerRow = sheet.CreateRow(0);
            if (exportColList == null || exportColList.Count <= 0)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    ICell headerCell = headerRow.CreateCell(column.Ordinal);
                    headerCell.SetCellValue(column.ColumnName);
                    sheet.AutoSizeColumn(headerCell.ColumnIndex);
                }

            }
            else if(exportColList.Count==dt.Columns.Count)
            {
                for (int i = 0; i < exportColList.Count; i++)
                {
                    ICell headerCell = headerRow.CreateCell(i);
                    headerCell.SetCellValue(exportColList[i][1]);
                    sheet.AutoSizeColumn(headerCell.ColumnIndex);
                }
            }
            int rowIndex = 1;
            if (exportColList == null || exportColList.Count <= 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in dt.Columns)
                    {
                        ICell cell = dataRow.CreateCell(column.Ordinal);
                        cell.SetCellValue((row[column] ?? "").ToString());
                        ReSizeColumnWidth(sheet, cell);
                    }
                    rowIndex++;
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow dataRow = sheet.CreateRow(i+1);
                    for (int j = 0; j < exportColList.Count; j++)
                    {
                        ICell cell = dataRow.CreateCell(j);
                        cell.SetCellValue((dt.Rows[i][exportColList[j][0]] ?? "").ToString());
                        ReSizeColumnWidth(sheet, cell);
                    }
                }

            }
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            wk.Write(fs);
            fs.Dispose();
            wk = null;
            return filePath;
        }
        /// <summary>
        /// 导出Excel到响应流中
        /// </summary>
        /// <param name="response"></param>
        /// <param name="fileName"></param>
        /// <param name="SheetName"></param>
        /// <param name="dt"></param>
        /// <param name="exportColList">字段重命名列表 string[] 0.数据表字段,1.重命名字段</param>
        /// <returns></returns>
        public static bool CreateXls(HttpResponse response,string fileName, string SheetName, DataTable dt,List<string[]> exportColList)
        {
            try
            {
                //创建工作薄
                HSSFWorkbook wk = new HSSFWorkbook();

                //创建一个名称为mySheet的表
                ISheet sheet = wk.CreateSheet(SheetName);
                IRow headerRow = sheet.CreateRow(0);

                if (exportColList == null || exportColList.Count <= 0)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        ICell headerCell = headerRow.CreateCell(column.Ordinal);
                        headerCell.SetCellValue(column.ColumnName);
                        sheet.AutoSizeColumn(headerCell.ColumnIndex);
                    }
                }
                else {
                    for (int i = 0; i < exportColList.Count; i++)
                    {
                        ICell headerCell = headerRow.CreateCell(i);
                        headerCell.SetCellValue(exportColList[i][1]);
                        sheet.AutoSizeColumn(headerCell.ColumnIndex);
                    }
                }
                int rowIndex = 1;
                if (exportColList == null || exportColList.Count <= 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        foreach (DataColumn column in dt.Columns)
                        {
                            ICell cell = dataRow.CreateCell(column.Ordinal);
                            cell.SetCellValue((row[column] ?? "").ToString());
                            ReSizeColumnWidth(sheet, cell);
                        }
                        rowIndex++;
                    }
                }else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow dataRow = sheet.CreateRow(i+1);
                        for (int j = 0; j < exportColList.Count;j++ )
                        {
                            ICell cell = dataRow.CreateCell(j);
                            cell.SetCellValue((dt.Rows[i][exportColList[j][0]] ?? "").ToString());
                            ReSizeColumnWidth(sheet, cell);
                        }
                    }

                }

                response.Clear();
                response.ContentType = "application/vnd.ms-excel";
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = string.IsNullOrEmpty(dt.TableName) ? "Default" : dt.TableName;
                }
                response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName + ".xls"));
                MemoryStream filetmp = new MemoryStream();
                wk.Write(filetmp);
                filetmp.Close();
                response.BinaryWrite(filetmp.GetBuffer());
                response.End();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        /// <summary>
        /// 根据单元格内容重新设置列宽
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="cell"></param>
        public static void ReSizeColumnWidth(ISheet sheet, ICell cell)
        {
            int cellLength = (Encoding.Default.GetBytes(cell.ToString()).Length + 5) * 256;
            const int maxLength = 255 * 256;
            if (cellLength > maxLength)
            {
                cellLength = maxLength;
            }
            int colWidth = sheet.GetColumnWidth(cell.ColumnIndex);
            if (colWidth < cellLength)
            {
                sheet.SetColumnWidth(cell.ColumnIndex, cellLength);
            }
        }
        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable(Stream excelFileStream, string sheetName, int headerRowIndex)
        {
            IWorkbook workbook = new HSSFWorkbook(excelFileStream);
            ISheet sheet = null;
            int sheetIndex = -1;
            if (int.TryParse(sheetName, out sheetIndex))
            {
                sheet = workbook.GetSheetAt(sheetIndex);
            }
            else
            {
                sheet = workbook.GetSheet(sheetName);
            }

            DataTable table = new DataTable();

            IRow headerRow = sheet.GetRow(headerRowIndex);//获取首行列头
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                if (headerRow.GetCell(i) == null || headerRow.GetCell(i).StringCellValue.Trim() == "")
                {
                    // 如果遇到第一个空列，则不再继续向后读取
                    cellCount = i;
                    break;
                }
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            for (int i = (headerRowIndex + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                //如果遇到某行的第一个单元格的值为空，则不再继续向下读取
                if (row != null && !string.IsNullOrEmpty(row.GetCell(0).ToString()))
                {
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        dataRow[j] = row.GetCell(j).ToString();
                    }

                    table.Rows.Add(dataRow);
                }
            }
            excelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }
        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="excelFilePath">Excel文件路径，为物理路径,可传空值</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable(string excelFilePath, string sheetName, int headerRowIndex)
        {
            if (string.IsNullOrEmpty(excelFilePath))
            {
                return null;
            }

            using (FileStream stream = System.IO.File.OpenRead(excelFilePath))
            {
                return ToDataTable(stream, sheetName, headerRowIndex);
            }
        }
    }
}
