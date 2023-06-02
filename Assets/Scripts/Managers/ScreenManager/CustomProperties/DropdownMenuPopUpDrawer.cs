using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using ScreenManagerNS;

[CustomPropertyDrawer(typeof(DropdownMenuPopUpAttribute))]
public class DropdownMenuPopUpDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        if (ScreenManager.PopUps != null)
        {
            List<string> popUpNames = new List<string>();
            for (int i = 0; i < ScreenManager.PopUps.Length; i++)
            {
                if (ScreenManager.PopUps[i] != null)
                    popUpNames.Add(ScreenManager.PopUps[i].name);

            }
            popUpNames.Sort();

            int selectedIndex = popUpNames.IndexOf(property.stringValue);
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, popUpNames.ToArray());

            if (selectedIndex >= 0 && selectedIndex < popUpNames.Count)
            {
                property.stringValue = popUpNames[selectedIndex];
            }
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}


public class DropdownMenuPopUpAttribute : PropertyAttribute
{
}