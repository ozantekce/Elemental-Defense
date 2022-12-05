using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    private static string name;
    private static int gold;
    private static int essence;
    private static int rebornPoint;
    private static int wave;
    private static int maxWave;

    public static string Name {
        get { 
            return PlayerPrefs.GetString("name","noname");
        }
        set { 
            PlayerPrefs.SetString("name",value);
        }
    }
    public static int Gold
    {
        get
        {
            return PlayerPrefs.GetInt("gold", 100);
        }
        set
        {
            PlayerPrefs.SetInt("gold", value);
        }
    }

    public static int Wave
    {
        get
        {
            return PlayerPrefs.GetInt("wave", 1);
        }
        set
        {
            PlayerPrefs.SetInt("wave", value);
        }
    }


}
