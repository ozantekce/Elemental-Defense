using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenManagerNS
{
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

        private UpdateTextMethod _updateText;
        /*
        private void Awake()
        {
            ScreenManager.ExtendedTexts.Add(this.name, this);
        }*/

        private void Start()
        {
            if (_updateText != null && _updateType == UpdateType.UpdateOnVisible)
            {
                Text = _updateText();
            }
        }

        private void OnEnable()
        {
            if (_updateText != null && _updateType == UpdateType.UpdateOnVisible)
            {
                Text = _updateText();
            }
        }

        private int _frameCounter = 0;
        private float _timeCounter = 0;
        private void Update()
        {
            if (_updateType == UpdateType.UpdateInFrame_)
            {
                _frameCounter++;
                if (_updateText != null && _frameCounter >= updateFrame)
                {
                    Text = _updateText();
                    _frameCounter = 0;
                }
            }
            else if (_updateType == UpdateType.UpdatePerFrame)
            {
                if (_updateText != null)
                {
                    Text = _updateText();
                }
            }
            else if (_updateType == UpdateType.UpdateTime_)
            {
                _timeCounter += Time.deltaTime;
                if (_updateText != null && _timeCounter >= updateTime)
                {
                    Text = _updateText();
                    _timeCounter = 0;
                }
            }

        }


        public static void UpdateText(string textName)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return;

            ExtendedText extendedText = ScreenManager.ExtendedTextDictionary[textName];
            if (extendedText._updateText != null)
            {
                extendedText.Text = extendedText._updateText();
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


        public static void SetText(string textName, UpdateTextMethod update)
        {
            if (!ScreenManager.ExtendedTextDictionary.ContainsKey(textName)) return;
            ScreenManager.ExtendedTextDictionary[textName]._updateText = update;
        }


        public delegate string UpdateTextMethod();

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





    }

}