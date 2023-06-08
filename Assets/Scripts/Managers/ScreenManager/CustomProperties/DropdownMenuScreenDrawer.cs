using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using ScreenManagerNS;




#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(DropdownMenuScreenAttribute))]
public class DropdownMenuScreenDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        if (ScreenManager.Screens != null)
        {
            List<string> screenNames = new List<string>();
            for (int i = 0; i < ScreenManager.Screens.Length; i++)
            {
                if (ScreenManager.Screens[i] != null)
                    screenNames.Add(ScreenManager.Screens[i].name);

            }
            screenNames.Sort();

            int selectedIndex = screenNames.IndexOf(property.stringValue);
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, screenNames.ToArray());

            if (selectedIndex >= 0 && selectedIndex < screenNames.Count)
            {
                property.stringValue = screenNames[selectedIndex];
            }
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
#else
    // Runtime code here
#endif




public class DropdownMenuScreenAttribute : PropertyAttribute
{
}