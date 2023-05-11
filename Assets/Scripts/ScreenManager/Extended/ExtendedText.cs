using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenManagerNS
{
    public delegate string UpdateTextMethod();
    public delegate int UpdateTextMethodInt();
    public delegate float UpdateTextMethodFloat();
    public class ExtendedText : MonoBehaviour
    {

        public UpdateType _updateType;
        [ShowInEnum("_updateType", "UpdateInFrame_"), Range(1, 299)]
        public int updateFrame = 1;
        [ShowInEnum("_updateType", "UpdateTime_"), Range(0, 299)]
        public float updateTime = 1;

        public TextType textType = TextType.TextMeshPro;
        [ShowInEnum("textType", "TextMeshPro")]
        public TextMeshProUGUI textMeshPro;
        [ShowInEnum("textType", "UnityText")]
        public Text text;

        private UpdateTextMethod _updateTextMethod;

        private string _usingFormat;

        private void Start()
        {
            if (_updateTextMethod != null && _updateType == UpdateType.UpdateOnVisible)
            {
                Text = _updateTextMethod();
            }
        }

        private void OnEnable()
        {
            if (_updateTextMethod != null && _updateType == UpdateType.UpdateOnVisible)
            {
                Text = _updateTextMethod();
            }
        }

        private int _frameCounter = 0;
        private float _timeCounter = 0;
        private void Update()
        {
            if (_updateType == UpdateType.UpdateInFrame_)
            {
                _frameCounter++;
                if (_updateTextMethod != null && _frameCounter >= updateFrame)
                {
                    Text = _updateTextMethod();
                    _frameCounter = 0;
                }
            }
            else if (_updateType == UpdateType.UpdatePerFrame)
            {
                if (_updateTextMethod != null)
                {
                    Text = _updateTextMethod();
                }
            }
            else if (_updateType == UpdateType.UpdateTime_)
            {
                _timeCounter += Time.deltaTime;
                if (_updateTextMethod != null && _timeCounter >= updateTime)
                {
                    Text = _updateTextMethod();
                    _timeCounter = 0;
                }
            }

        }


        public static void UpdateTextWithMethod(string textName)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return;

            ExtendedText extendedText = ScreenManager.ExtendedTextDictionary[textName];
            if (extendedText._updateTextMethod != null)
            {
                extendedText.Text = extendedText._updateTextMethod();
            }
        }

        public static void SetText(string textName, string text)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return;
            ScreenManager.ExtendedTextDictionary[textName].Text = text;
        }

        public static string GetText(string textName)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return "";
            return ScreenManager.ExtendedTextDictionary[textName].Text;
        }


        public static void SetTextMethod(string textName, UpdateTextMethod update)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return;
            ScreenManager.ExtendedTextDictionary[textName]._updateTextMethod = update;
        }

        public static void SetTextMethod(string textName,string format, UpdateTextMethodFloat update)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return;
            UpdateTextMethod updateTextMethod = () =>
            {
                return format.ExecuteFormat(update());
            };
            ScreenManager.ExtendedTextDictionary[textName]._updateTextMethod = updateTextMethod;
        }

        public static void SetTextMethod(string textName, string format, UpdateTextMethodInt update)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return;
            UpdateTextMethod updateTextMethod = () =>
            {
                return format.ExecuteFormat(update());
            };
            ScreenManager.ExtendedTextDictionary[textName]._updateTextMethod = updateTextMethod;
        }




        public enum TextType { UnityText, TextMeshPro }

        public enum UpdateType
        {
            None, UpdatePerFrame, UpdateOnVisible
                , UpdateInFrame_, UpdateTime_
        }

        public string Text
        {
            get
            {
                if (textType == TextType.TextMeshPro)
                {
                    return textMeshPro.text;
                }
                else if (textType == TextType.UnityText)
                {
                    return text.text;
                }
                return string.Empty;
            }
            set
            {
                if (textType == TextType.TextMeshPro)
                {
                    textMeshPro.text = value;
                }
                else if (textType == TextType.UnityText)
                {
                    text.text = value;
                }
            }
        }

        public UpdateTextMethod UpdateMethod { get => _updateTextMethod; set => _updateTextMethod = value; }
    }

    public static class ExtendedTextExtensions
    {
        public static void UpdateTextWithMethod(this string textName)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return;

            ExtendedText extendedText = ScreenManager.ExtendedTextDictionary[textName];
            if (extendedText.UpdateMethod != null)
            {
                extendedText.Text = extendedText.UpdateMethod();
            }
        }

        public static void SetText(this string textName, string text)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return;
            ScreenManager.ExtendedTextDictionary[textName].Text = text;
        }

        public static string GetText(this string textName)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return "";
            return ScreenManager.ExtendedTextDictionary[textName].Text;
        }


        public static void SetTextMethod(this string textName, UpdateTextMethod update)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return;
            ScreenManager.ExtendedTextDictionary[textName].UpdateMethod = update;
        }

        public static void SetTextMethod(this string textName, string format, UpdateTextMethodFloat update)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return;
            UpdateTextMethod updateTextMethod = () =>
            {
                return format.ExecuteFormat(update());
            };
            ScreenManager.ExtendedTextDictionary[textName].UpdateMethod = updateTextMethod;
        }

        public static void SetTextMethod(this string textName, string format, UpdateTextMethodInt update)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return;
            UpdateTextMethod updateTextMethod = () =>
            {
                return format.ExecuteFormat(update());
            };
            ScreenManager.ExtendedTextDictionary[textName].UpdateMethod = updateTextMethod;
        }

    }

}