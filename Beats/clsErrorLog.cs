using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


class clsErrorLog
{
    private string fileName;
    private string pathname;
    private string errorMessage;
    private int errorFlag;

    StreamWriter sw = null;
    StreamReader sr = null;

    public clsErrorLog(string msg)
    {
        errorMessage = msg;
        errorFlag = 0;
        fileName = "ErrorLog.txt";
    }
    public string FileName
    {
        get
        {
            return fileName;
        }
        set
        {
            if (value.Length > 0)
                fileName = value;
        }
    }
    public string Message
    {
        get
        {
            return errorMessage;
        }
        set
        {
            if (value.Length > 0)
                errorMessage = value;
        }
    }
    public string PathName
    {
        get
        {
            return pathname;
        }
        set
        {
            if (value.Length > 0)
                pathname = value;
        }
    }
    public string ReadErrorLog()
    {
        string buff;
        try
        {
            string pfn = Path.Combine(pathname, fileName);
            if (File.Exists(pfn) == true)
            {
                sr = new StreamReader(pfn);

                buff = sr.ReadToEnd();
                return buff;
            }
        }
        catch
        {
            return "";
        }
        return "";
    }
    public int WriteErrorLog()
    {
        errorFlag = 0;
        DateTime currentDT = DateTime.Now;

        try
        {
            if (errorMessage.Length != 0 && pathname.Length != 0 && fileName.Length != 0)
            {
                sw = new StreamWriter(Path.Combine(pathname, fileName), true);
                sw.WriteLine(errorMessage);
                sw.Close();
            }
            else
            {
                errorFlag = 1;
            }
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            errorFlag = 1;
        }
        return errorFlag;

    }
    public int WriteErrorLog(string msg)
    {
        errorMessage = msg;
        errorFlag = WriteErrorLog();
        return errorFlag;
    }

}