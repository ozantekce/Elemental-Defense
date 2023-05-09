using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace ScreenManagerNS
{

    public class ExtendedButton : MonoBehaviour
    {


        [SerializeField]
        private OnClickData[] onClicks;

        private Button _button;


        private void Awake()
        {
            _button = GetComponent<Button>();
            if (_button == null)
            {
                _button = gameObject.AddComponent<Button>();
            }
            foreach (OnClickData onClick in onClicks)
            {
                _button.onClick.AddListener(() => ButtonHelper.Execute(onClick));
            }
        }


        [System.Serializable]
        private class OnClickData
        {

            public OperationType type;
            [ShowInEnum("type", "LoadScreen"), DropdownMenuScreen]
            public string screenName;
            [ShowInEnum("type", "OpenPopUp", "ClosePopUp"), DropdownMenuPopUp]
            public string popUpName;
            [ShowInEnum("type", "UpdateText"), DropdownMenuText]
            public string textName;

            public float waitToExecute;
            [ShowInEnum("type", "OpenPopUp")]
            public bool closeOtherPopUps;
            public float waitToReuse;

            public UnityEvent onClickEvent;

            private float _nextClickTime = 0f;

            public string ElementName
            {
                get
                {
                    if (type == OperationType.LoadScreen) return screenName;
                    return popUpName;
                }
            }


            public float NextClickTime { get { return _nextClickTime; } set { _nextClickTime = value; } }

        }


        private static class ButtonHelper
        {

            private static Dictionary<OperationType, OperationMethod>
                _typeMethodPairs = new Dictionary<OperationType, OperationMethod>() {
                {OperationType.LoadScreen ,  LoadScreen },
                {OperationType.OpenPopUp  ,  OpenPopUp },
                {OperationType.ClosePopUp ,  ClosePopUp },
                {OperationType.CloseAllPopUps ,  CloseAllPopUps },
                {OperationType.QuitApplication ,  QuitApplication },
                {OperationType.UpdateText ,  UpdateText }
                };

            public static void Execute(OnClickData data)
            {
                if (data.NextClickTime > Time.time) return;
                data.NextClickTime = Time.time + data.waitToReuse;
                _typeMethodPairs[data.type](data);
                data.onClickEvent?.Invoke();
            }

            private static void LoadScreen(OnClickData data)
            {
                ScreenManager.Instance.LoadScreen(data.ElementName
                    , data.waitToExecute);
            }
            private static void OpenPopUp(OnClickData data)
            {
                ScreenManager.Instance.OpenPopUp(data.ElementName
                    , data.waitToExecute
                    , data.closeOtherPopUps);
            }

            private static void ClosePopUp(OnClickData data)
            {
                ScreenManager.Instance.ClosePopUp(data.ElementName
                    , data.waitToExecute);
            }
            private static void CloseAllPopUps(OnClickData data)
            {
                ScreenManager.Instance.CloseAllPopUps(data.waitToExecute);
            }

            private static void QuitApplication(OnClickData data)
            {
                ScreenManager.Instance.QuitApplication(data.waitToExecute);
            }

            private static void UpdateText(OnClickData data)
            {
                ExtendedText.UpdateText(data.textName);
            }


            private delegate void OperationMethod(OnClickData data);

        }

        public enum OperationType
        {
            LoadScreen, OpenPopUp, ClosePopUp, CloseAllPopUps
                , QuitApplication, UpdateText
        }

    }


}

