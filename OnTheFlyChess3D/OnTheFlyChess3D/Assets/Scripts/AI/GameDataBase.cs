using System.Collections.Generic;
using System.IO;
using System;

public class GameDataBase 
{
    private string writePath = @"C:\Users\David\Desktop\OnTheFlyChess3D\OnTheFlyChess3D\DataBase\firstGame.txt";
    private string readPath = @"C:\Users\David\Desktop\OnTheFlyChess3D\OnTheFlyChess3D\DataBase\firstGame.txt";

    private FileStream initFileStream;

    private StreamWriter writeToFile;
    private StreamReader readFromFile;

    public void initStreamWriter()
    {
        initFileStream = File.Create(writePath, 2);
        writeToFile = new StreamWriter(initFileStream);
    }
    public void initStreamRead()
    {
        initFileStream = new FileStream(readPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
        readFromFile = new StreamReader(initFileStream);
    }
    public void Write(string i_writeString)
    {
        if(writeToFile == null)
        {
            throw new Exception("Write Exepction");
        }
        writeToFile.WriteLine(i_writeString);
    }
    public List<string> Read()
    {
        List<string> o_Data = new List<string>();

        if(readFromFile == null)
        {
            throw new Exception("Read Exepction");
        }

        while (!readFromFile.EndOfStream)
        {
            o_Data.Add(readFromFile.ReadLine());
        }
        return o_Data;



    }
    public void closeWrite()
    {
        writeToFile.Flush();
        writeToFile.Close();
    }

}
