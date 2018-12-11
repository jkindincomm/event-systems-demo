using System;
using System.IO;
using FileHelpers;

public class File
{
    public int TotalRecords;
    public string FilePath
    {
        get;
        set;
    }
    public DateTime LastWriteTimeUtc
    {
        get;
        set;
    }

    public DateTime ProcessTimeUtc
    {
        get;
        set;
    }

}