using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    private static string name;
    private static int gold;
    private static int level;

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

    public static int Level
    {
        get
        {
            return PlayerPrefs.GetInt("level", 1);
        }
        set
        {
            PlayerPrefs.SetInt("level", value);
        }
    }


}
