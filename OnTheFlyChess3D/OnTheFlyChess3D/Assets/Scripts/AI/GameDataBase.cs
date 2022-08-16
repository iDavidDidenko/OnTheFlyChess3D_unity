using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
public class GameDataBase : MonoBehaviour
{
    private string writePath = @"/Users/david/Desktop/t1/OnTheFlyChess3D/DataBase/firstGame.txt";
    // correct syntax
    private string readPath = @"/Users/david/Desktop/t1/OnTheFlyChess3D/DataBase/firstGame.txt";

    private FileStream initFileStream;

    private StreamWriter writeToFile;
    private StreamReader readFromFile;

    // test

    private string str;

    // test

    public void initStreamWriter()
    {
        initFileStream = File.Create(writePath, 2);
        writeToFile = new StreamWriter(initFileStream);
    }
    public void initStreamRead()
    {
        initFileStream = new FileStream(readPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
        readFromFile = new StreamReader(initFileStream);
        //str = System.IO.File.ReadAllText(readPath);

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

        if (readFromFile == null)
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
