using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using ScreenManagerNS;

[CustomPropertyDrawer(typeof(DropdownMenuTextAttribute))]
public class DropdownMenuTextDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        if (ScreenManager.ExtendedTexts != null)
        {
            List<string> extendedTexts = new List<string>();
            for (int i = 0; i < ScreenManager.ExtendedTexts.Length; i++)
            {
                if (ScreenManager.ExtendedTexts[i] != null)
                    extendedTexts.Add(ScreenManager.ExtendedTexts[i].name);

            }
            extendedTexts.Sort();

            int selectedIndex = extendedTexts.IndexOf(property.stringValue);
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, extendedTexts.ToArray());

            if (selectedIndex >= 0 && selectedIndex < extendedTexts.Count)
            {
                property.stringValue = extendedTexts[selectedIndex];
            }
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}


public class DropdownMenuTextAttribute : PropertyAttribute
{
}