using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;

namespace HH_TL.Server
{
    public class CSVFile
    {
        public bool OpenCSVFile(DataTable mycsvdt, string filepath)
        {
            string strpath = filepath; //csv文件的路径

            try
            {
                bool blnFlag = true;

                DataColumn mydc;
                DataRow mydr;

                string strline;
                string[] aryline;
                StreamReader mysr = new StreamReader(strpath, System.Text.Encoding.Default);

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new char[] { ',' });
                    //第一行是列的名字，给datatable加上列名,
                    if (blnFlag)
                    {
                        blnFlag = false;
                        foreach (var item in aryline)
                        {
                            if (!mycsvdt.Columns.Contains(item))
                            {
                                mydc = new DataColumn(item);
                                mycsvdt.Columns.Add(mydc);
                            }
                        }
                        continue;
                    }
                    //填充数据并加入到datatable中
                    mydr = mycsvdt.Rows.Add(aryline);
                }
                mysr.Close();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable OpenCSVFile()
        {
            DataTable dt = new DataTable();
            string path = GetImportCsvFilePath();
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            if (OpenCSVFile(dt, path))
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        public string GetImportCsvFilePath()
        {
            // Displays an OpenFileDialog and shows the read/only files.

            OpenFileDialog dlgOpenFile = new OpenFileDialog();
            dlgOpenFile.ShowReadOnly = true;
            dlgOpenFile.Filter = "CSV (*.CSV)|*.csv";
            dlgOpenFile.FilterIndex = 1;
            dlgOpenFile.RestoreDirectory = true;
            dlgOpenFile.Title = "Choose csv File";
            if (dlgOpenFile.ShowDialog() == true)
            {
                //文件路径 和文件名字  
                return dlgOpenFile.FileName;
                // fileName = Path.GetFileName(filePath);
                // 获取文件后缀  
                //fileExtension = Path.GetExtension(filePath);
            }
            return "";
        }

        /// <summary>
        /// 将datatable中的数据保存到csv中
        /// </summary>
        /// <param name="dt">数据来源</param>
        /// <param name="savaPath">保存的路径</param>
        /// <param name="strName">保存文件的名称</param>
        public void ExportToSvc(DataTable dt, string savaPath, string strName)
        {
            //给文件名前加上时间
            //newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt;
            //在文件名里加字符
            //saveFileDialog1.FileName.Insert(1,"dameng");
            //System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();//输出文件

            //string strPath = Path.GetTempPath() + strName + ".csv";//保存到本项目文件夹下

            string strPath = savaPath + "\\" + strName;//保存到指定目录下

            if (File.Exists(strPath))
            {
                File.Delete(strPath);
            }
            //先打印标头
            StringBuilder strColu = new StringBuilder();
            StringBuilder strValue = new StringBuilder();
            int i = 0;
            StreamWriter sw = new StreamWriter(new FileStream(strPath, FileMode.CreateNew), Encoding.GetEncoding("GB2312"));
            try
            {
                for (i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    strColu.Append(dt.Columns[i].ColumnName);
                    strColu.Append(",");
                }
                strColu.Remove(strColu.Length - 1, 1);//移出掉最后一个,字符
                sw.WriteLine(strColu);
                foreach (DataRow dr in dt.Rows)
                {
                    strValue.Remove(0, strValue.Length);//移出
                    for (i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        strValue.Append(dr[i].ToString());
                        strValue.Append(",");
                    }
                    strValue.Remove(strValue.Length - 1, 1);//移出掉最后一个,字符
                    sw.WriteLine(strValue);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                sw.Close();
            }
        }
    }
}
