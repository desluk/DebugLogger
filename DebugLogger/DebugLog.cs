using System.Runtime.InteropServices;

namespace CardSearchApi.Debug;

public static class DebugLog
{
    // Consistants
    private static string fileName = "LogFile_";
    private static string LinuxPath = "/home/";
    private static string linuxDocument = "/Documents/";
    private static string WindowsPath = "c:\\Documents\\";
    
    // Private Variables
    private static string finalLogFileName = String.Empty;
    private static string userName = Environment.UserName;

    //Private Methods
    private static string CreateFileName()
    {
        if (string.CompareOrdinal(finalLogFileName, String.Empty) == 0)
        {
            DateTime dateTime = DateTime.Today;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                finalLogFileName = LinuxPath +userName+ linuxDocument + fileName + dateTime.Date.Day +"_"+ dateTime.Date.Month+"_"+dateTime.Date.Year;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                finalLogFileName = WindowsPath + fileName + dateTime.Date;
            }
        }

        return finalLogFileName;
    }

    private static void CreateLogFile()
    {
        if (!CheckIfLogFileAlreadyExisting())
        {
            using FileStream file = File.Create(finalLogFileName);
            
            file.Flush();
            file.Close();
        }
        else if (System.Diagnostics.Debugger.IsAttached)
        {
            File.Delete(finalLogFileName);
            
            using FileStream file = File.Create(finalLogFileName);
            
            file.Flush();
            file.Close();
        }
    }
    
    private static void CreateLogFile(string fileLocation)
    {
        finalLogFileName = fileLocation.Trim();
        DateTime dateTime = DateTime.Today;
        finalLogFileName = finalLogFileName + fileName + dateTime.Date.Day + "_" + dateTime.Date.Month + "_" +
                           dateTime.Date.Year;
                           
        if (!File.Exists(finalLogFileName))
        {
            using FileStream file = File.Create(finalLogFileName);
            
            file.Flush();
            file.Close();
        }
        else if (System.Diagnostics.Debugger.IsAttached)
        {
            File.Delete(finalLogFileName);
            
            using FileStream file = File.Create(finalLogFileName);
            
            file.Flush();
            file.Close();
        }
    }
    
    private static void CreateLogFile(string fileLocation, string fileName)
    {
        finalLogFileName = fileLocation.Trim() + fileName.Trim();
        if (!File.Exists(finalLogFileName))
        {
            using FileStream file = File.Create(finalLogFileName);
            
            file.Flush();
            file.Close();
        }
        else if (System.Diagnostics.Debugger.IsAttached)
        {
            File.Delete(finalLogFileName);
            
            using FileStream file = File.Create(finalLogFileName);
            
            file.Flush();
            file.Close();
        }
    }
    
    private static void WriteLog(string logToWrite)
    {
        if (string.CompareOrdinal(finalLogFileName, String.Empty) != 0)
        {
            using StreamWriter writer = new StreamWriter(finalLogFileName);
            writer.WriteLine(logToWrite);
            writer.WriteLine("<==============================================>");
            writer.Flush();
            writer.Close();
        }
    }
    
    private static bool CheckIfLogFileAlreadyExisting()
    {
        try
        {
            return File.Exists(CreateFileName());
        }
        catch
        {
            return false;        
        }
    }

    //Public Methods
    /// <summary>
    /// This method will write the log given to the following default locations
    /// Windows: c:\\Documents\\LogFile_Day_Month_Year
    /// Linux: /home/username/Documents/LogFile_Day_Month_Year
    /// </summary>
    /// <param name="logToWrite">This is the string that will be written to the log file</param>
    public static void WriteDebugLog(string logToWrite)
    {
        CreateLogFile();
        WriteLog(logToWrite);
    }

    /// <summary>
    /// This method will write the log to a given location with the following default file name
    /// File Name: LogFile_Day_Month_Year
    /// </summary>
    /// <param name="logToWrite">This is the string that will be written to the log file</param>
    /// <param name="fileLocation"></param>
    public static void WriteDebugLog(string logToWrite, string fileLocation)
    {
        CreateLogFile(fileLocation);
        WriteLog(logToWrite);
    }



    /// <summary>
    /// This method will write the log to a given location with the given file name and location
    /// </summary>
    /// <param name="logToWrite"></param>
    /// <param name="fileLocation"></param>
    /// <param name="fileName"></param>
    public static void WriteDebugLog(string logToWrite, string fileLocation, string fileName)
    {
        CreateLogFile(fileLocation,fileName);
        WriteLog(logToWrite);
    }
}