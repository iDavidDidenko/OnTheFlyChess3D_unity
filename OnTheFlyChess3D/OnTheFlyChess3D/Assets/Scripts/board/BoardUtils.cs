using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardUtils
{
    public static bool[] FIRST_COLUM = initColum(0);
    public static bool[] SECOND_COLUM = initColum(1);
    public static bool[] THIRD_COLUM = initColum(2);
    public static bool[] FOURTH_COLUM = initColum(3);
    public static bool[] FIVE_COLUM = initColum(4);
    public static bool[] SIXTH_COLUM = initColum(5);
    public static bool[] SEVEN_COLUM = initColum(6);
    public static bool[] EIGHT_COLUM = initColum(7);

    public static bool[] FIRST_ROW = initRow(0);
    public static bool[] SECOND_ROW = initRow(8);
    public static bool[] THIRD_ROW = initRow(16);
    public static bool[] FOURTH_ROW = initRow(24);
    public static bool[] FIVE_ROW = initRow(32);
    public static bool[] SIXTH_ROW = initRow(40);
    public static bool[] SEVEN_ROW = initRow(48);
    public static bool[] EIGTH_ROW = initRow(56);


    private static bool[] initColum(int i_colPosition)
    {
        bool[] o_colum = new bool[64];
        do
        {
            o_colum[i_colPosition] = true;
            i_colPosition += 8;

        } while (i_colPosition < 64);

        return o_colum;
    }

    private static bool[] initRow(int i_rowPosition)
    {
        bool[] o_row = new bool[64];

        do
        {
            o_row[i_rowPosition] = true;
            i_rowPosition++;

        }while (i_rowPosition % 8 != 0);

        return o_row;  
    }

    public static bool isValidTile(int i_futureDestination)
    {
        return i_futureDestination >= 0 && i_futureDestination < 64;
    }



}
