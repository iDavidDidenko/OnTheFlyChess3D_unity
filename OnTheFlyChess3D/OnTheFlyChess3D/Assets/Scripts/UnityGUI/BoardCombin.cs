using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCombin
{

    private static List<int[]> virtalBoard = new List<int[]>() { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 0, 3 }, new int[] { 0, 4 }, new int[] { 0, 5 }, new int[] { 0, 6 }, new int[] { 0, 7 },
                                                                 new int[] { 1, 0 }, new int[] { 1, 1 }, new int[] { 1, 2 }, new int[] { 1, 3 }, new int[] { 1, 4 }, new int[] { 1, 5 }, new int[] { 1, 6 }, new int[] { 1, 7 },
                                                                 new int[] { 2, 0 }, new int[] { 2, 1 }, new int[] { 2, 2 }, new int[] { 2, 3 }, new int[] { 2, 4 }, new int[] { 2, 5 }, new int[] { 2, 6 }, new int[] { 2, 7 },
                                                                 new int[] { 3, 0 }, new int[] { 3, 1 }, new int[] { 3, 2 }, new int[] { 3, 3 }, new int[] { 3, 4 }, new int[] { 3, 5 }, new int[] { 3, 6 }, new int[] { 3, 7 },
                                                                 new int[] { 4, 0 }, new int[] { 4, 1 }, new int[] { 4, 2 }, new int[] { 4, 3 }, new int[] { 4, 4 }, new int[] { 4, 5 }, new int[] { 4, 6 }, new int[] { 4, 7 },
                                                                 new int[] { 5, 0 }, new int[] { 5, 1 }, new int[] { 5, 2 }, new int[] { 5, 3 }, new int[] { 5, 4 }, new int[] { 5, 5 }, new int[] { 5, 6 }, new int[] { 5, 7 },
                                                                 new int[] { 6, 0 }, new int[] { 6, 1 }, new int[] { 6, 2 }, new int[] { 6, 3 }, new int[] { 6, 4 }, new int[] { 6, 5 }, new int[] { 6, 6 }, new int[] { 6, 7 },
                                                                 new int[] { 7, 0 }, new int[] { 7, 1 }, new int[] { 7, 2 }, new int[] { 7, 3 }, new int[] { 7, 4 }, new int[] { 7, 5 }, new int[] { 7, 6 }, new int[] { 7, 7 }, };
    public static int[] getPointVirtual(int i_index) { return virtalBoard[i_index]; }

}
