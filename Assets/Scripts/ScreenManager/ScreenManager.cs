using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScreenManagerNS
{
    [ExecuteInEditMode]
    public class ScreenManager : MonoBehaviour
    {

        private static ScreenManager instance;

        public Screen[] screens;
        // using for dropdown menu
        public static Screen[] Screens;

        public PopUp[] popUps;
        // using for dropdown menu
        public static PopUp[] PopUps;

        public ExtendedText[] extendedTexts;
        // using for dropdown menu
        public static ExtendedText[] ExtendedTexts;


        private Dictionary<string, Screen> _screenDictionary
            = new Dictionary<string, Screen>();
        private Dictionary<string, PopUp> _popUpDictionary
            = new Dictionary<string, PopUp>();
        private Dictionary<string, ExtendedText> _extendedTextDictionary
            = new Dictionary<string, ExtendedText>();

        public Screen initialScreen;

        private Screen _currentScreen;
        private ISet<PopUp> _openedPopUps;

        private List<string> _forceCloseList;



        private Heap<Command> _commands;

        private void Awake()
        {
            if (!Application.isPlaying) return;

            FindExtendedTexts();
            FindScreens();
            FindPopUps();

            _commands = new Heap<Command>();
            _openedPopUps = new HashSet<PopUp>();

            //_screenDictionary = new Dictionary<string, Screen>();
            foreach (Screen s in screens)
            {
                s.Configurations();
                //_screenDictionary.Add(s.name, s);
                _screenDictionary[s.name].gameObject.SetActive(false);
            }

            //_popUpDictionary = new Dictionary<string, PopUp>();
            foreach (PopUp s in popUps)
            {
                s.Configurations();
                //_popUpDictionary.Add(s.name, s);
                _popUpDictionary[s.name].gameObject.SetActive(false);
            }
            MakeSingleton();



        }

        private void Start()
        {
            if (!Application.isPlaying) return;
            LoadScreen(initialScreen.name);
        }


        //Optional
        private const bool ExecuteOneCommandPerFrame = false;

        private void Update()
        {

            if (!Application.isPlaying)
            {
                FindExtendedTexts();
                extendedTexts = ExtendedTexts;
                FindScreens();
                screens = Screens;
                FindPopUps();
                popUps = PopUps;
                return;
            }

        ExecuteNextCommand:
            if (_commands.IsEmpty() || !_commands.Peek().IsReady)
                return;

            Command command = _commands.Remove();

            if (command.IsReady)
            {
                command.Execute();
                if (!ExecuteOneCommandPerFrame)
                {
                    goto ExecuteNextCommand;
                }
            }

        }


        private void FindExtendedTexts()
        {
            ExtendedText[] extendedTextComponents
                = Resources.FindObjectsOfTypeAll<ExtendedText>();
            foreach (ExtendedText component in extendedTextComponents)
            {
                if (component.gameObject.scene == gameObject.scene)
                {
                    _extendedTextDictionary[component.name] = component;
                }
            }
            ExtendedTexts = extendedTextComponents;
        }

        private void FindScreens()
        {
            Screen[] screens
                = Resources.FindObjectsOfTypeAll<Screen>();
            foreach (Screen component in screens)
            {
                if (component.gameObject.scene == gameObject.scene)
                {
                    _screenDictionary[component.name] = component;
                }
            }
            Screens = screens;
        }

        private void FindPopUps()
        {
            PopUp[] popUps
                = Resources.FindObjectsOfTypeAll<PopUp>();
            foreach (PopUp component in popUps)
            {
                if (component.gameObject.scene == gameObject.scene)
                {
                    _popUpDictionary[component.name] = component;
                }
            }
            PopUps = popUps;
        }


        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadScreen(string screenName) { LoadScreen(screenName, 0); }

        public void LoadScreen(string screenName, float delay)
        {
            Screen nextScreen = _screenDictionary[screenName];
            // nextScreen must be different screen
            if (nextScreen == _currentScreen) return;
            // nextScreen must be Closed
            if (nextScreen.Status != IScreenElement.IScreenStatus.Closed) return;
            // add command
            AddCommand(new LoadScreenCommand(nextScreen, delay));

        }


        public void OpenPopUp(string popUpName) { OpenPopUp(popUpName, 0); }
        public void OpenPopUp(string popUpName, float delay, bool closeOtherPopUps = false)
        {
            AddCommand(new OpenPopUpCommand(_popUpDictionary[popUpName]
                , delay, closeOtherPopUps));
        }

        public void ClosePopUp(string popUpName) { ClosePopUp(popUpName, 0f); }
        public void ClosePopUp(string popUpName, float delay) { AddCommand(new ClosePopUpCommand(_popUpDictionary[popUpName], delay)); }


        public void ForceOpenPopUp(string popUpName)
        {
            PopUp openPopUp = _popUpDictionary[popUpName];

            openPopUp.Status = IScreenElement.IScreenStatus.Opened;
            openPopUp.MonoBehaviour.gameObject.SetActive(true);
            _openedPopUps.Add(openPopUp);
        }

        public void ForceClosePopUp(string popUpName)
        {
            PopUp closePopUp = _popUpDictionary[popUpName];

            closePopUp.Status = IScreenElement.IScreenStatus.Closed;
            closePopUp.MonoBehaviour.gameObject.SetActive(false);
            _openedPopUps.Remove(closePopUp);
        }

        public void CloseAllPopUps(float delay = 0) { AddCommand(new CloseAllPopUpsCommand(delay)); }

        public void CloseAllPopUpWithout(string popUpName)
        {
            AddCommand(new CloseAllPopUpsWithoutCommand(_popUpDictionary[popUpName]));
        }
        public void CloseAllPopUpWithout(string popUpName, float delay)
        {
            AddCommand(new CloseAllPopUpsWithoutCommand(_popUpDictionary[popUpName], delay));
        }

        private void AddCommand(Command command, params Command[] subs)
        {
            command.AddSubCommands(subs);
            _commands.Insert(command);
        }


        public void QuitApplication(float delay)
        {
            Application.Quit();
        }

        #region GetterSetter

        public static ScreenManager Instance { get => instance; }
        public Screen CurrentScreen { get => _currentScreen; }
        public static Dictionary<string, ExtendedText> ExtendedTextDictionary
        { get => Instance._extendedTextDictionary; set => Instance._extendedTextDictionary = value; }


        #endregion

        private void MakeSingleton() { instance = this; }

        private abstract class Command : IComparable<Command>
        {

            protected IScreenElement _element;
            private float _delay;
            private bool _waitUntilTerminated;

            private bool _isTerminated;
            private float _createTime;
            private List<Command> _subCommands = new List<Command>();
            public Command(IScreenElement element, float delay = 0)
            {
                this._element = element;
                this._createTime = Time.time;
                this._delay = delay;
            }

            public Command(float delay = 0)
            {
                _createTime = Time.time;
                this._delay = delay;
            }

            public void AddSubCommands(params Command[] subs) { _subCommands.AddRange(subs); }

            public void Execute()
            {
                for (int i = 0; i < _subCommands.Count; i++) _subCommands[i].Execute();
                Execute_();
            }
            protected abstract void Execute_();
            #region GetterSetter
            public float RemaniderTime { get { return Delay - (Time.time - _createTime); } }
            public float Delay { get => _delay; set => _delay = value; }
            public bool IsReady { get { return Time.time - (_createTime) >= Delay; } }
            public virtual bool IsTerminated { get => _isTerminated; protected set => _isTerminated = value; }
            public bool WaitUntilTerminated { get => _waitUntilTerminated; set => _waitUntilTerminated = value; }
            #endregion
            public int CompareTo(Command other)
            {
                if (this.RemaniderTime > other.RemaniderTime) return +1;
                else if (this.RemaniderTime < other.RemaniderTime) return -1;
                else return 0;
            }

        }


        private class LoadScreenCommand : Command
        {
            private Screen _screen;
            public LoadScreenCommand(Screen screen, float delay = 0) : base(screen, delay) { this._screen = screen; }

            protected override void Execute_()
            {
                if (_element == null || _element.Status != IScreenElement.IScreenStatus.Closed)
                {
                    IsTerminated = true;
                    return;
                }

                //unload current screen
                IScreenElement currentScreen = ScreenManager.Instance.CurrentScreen;
                currentScreen?.Close();
                // some popups must close when screen change
                ClosePopUpsWhenScreenChangeCommand.ExecuteImmediately();

                _element.Open();
                ScreenManager.Instance._currentScreen = _screen;
            }
            public override bool IsTerminated
            {
                get
                {
                    return IsTerminated
                        || _element.Status == IScreenElement.IScreenStatus.Opened;
                }
            }

        }

        private class OpenPopUpCommand : Command
        {

            private PopUp _popUp;
            private bool _closeOtherPopUps;

            public OpenPopUpCommand(PopUp popUp, float delay, bool closeOtherPopUps = false) : base(popUp, delay)
            {
                this._popUp = popUp;
                this._closeOtherPopUps = closeOtherPopUps;
            }

            protected override void Execute_()
            {
                if (_element == null || _element.Status != IScreenElement.IScreenStatus.Closed)
                {
                    IsTerminated = true;
                    return;
                }
                if (_closeOtherPopUps) CloseAllPopUpsWithoutCommand.ExecuteImmediately(_popUp);

                ScreenManager.instance._openedPopUps.Add(_popUp);
                _element.Open();
            }

            public override bool IsTerminated
            {
                get
                {
                    return IsTerminated
                        || _element.Status == IScreenElement.IScreenStatus.Opened;
                }
            }
        }

        private class ClosePopUpCommand : Command
        {
            private PopUp popUp;
            public ClosePopUpCommand(PopUp popUp, float delay = 0) : base(popUp, delay)
            {
                this.popUp = popUp;
            }

            protected override void Execute_()
            {
                if (_element == null || _element.Status != IScreenElement.IScreenStatus.Opened)
                {
                    IsTerminated = true;
                    return;
                }
                ScreenManager.instance._openedPopUps.Remove(popUp);
                _element.Close();
            }

            public override bool IsTerminated
            {
                get
                {
                    return IsTerminated
                        || _element.Status == IScreenElement.IScreenStatus.Closed;
                }
            }

        }

        private class CloseAllPopUpsCommand : Command
        {
            public CloseAllPopUpsCommand(float delay = 0) : base(delay) { }

            protected override void Execute_()
            {
                IsTerminated = true;
                List<string> forceCloseList = ScreenManager.instance._forceCloseList;
                ISet<PopUp> openedPopUps = ScreenManager.instance._openedPopUps;

                if (forceCloseList == null) forceCloseList = new List<string>();
                else forceCloseList.Clear();

                foreach (PopUp popUp in openedPopUps)
                {
                    if (popUp.Status == IScreenElement.IScreenStatus.Opened)
                        ScreenManager.instance.AddCommand(new ClosePopUpCommand(popUp, 0f));
                    else
                        forceCloseList.Add(popUp.name);
                }

                foreach (string popUp in forceCloseList)
                    ScreenManager.instance.ForceClosePopUp(popUp);
            }

            protected static void ExecuteImmediately()
            {
                List<string> forceCloseList = ScreenManager.instance._forceCloseList;
                ISet<PopUp> openedPopUps = ScreenManager.instance._openedPopUps;

                if (forceCloseList == null) forceCloseList = new List<string>();
                else forceCloseList.Clear();

                foreach (PopUp popUp in openedPopUps)
                {
                    if (popUp.Status == IScreenElement.IScreenStatus.Opened)
                        ScreenManager.instance.AddCommand(new ClosePopUpCommand(popUp, 0f));
                    else
                        forceCloseList.Add(popUp.name);
                }

                foreach (string popUp in forceCloseList)
                    ScreenManager.instance.ForceClosePopUp(popUp);
            }

        }

        private class ClosePopUpsWhenScreenChangeCommand : Command
        {
            public ClosePopUpsWhenScreenChangeCommand(float delay = 0) : base(delay) { }

            protected override void Execute_()
            {
                IsTerminated = true;
                List<string> forceCloseList = ScreenManager.instance._forceCloseList;
                ISet<PopUp> openedPopUps = ScreenManager.instance._openedPopUps;

                // some popups must close when screen change
                if (forceCloseList == null) forceCloseList = new List<string>();
                else forceCloseList.Clear();

                foreach (PopUp popUp in openedPopUps)
                    if (popUp.CloseWhenScreenChange) forceCloseList.Add(popUp
                        .MonoBehaviour.gameObject.name);

                foreach (string name in forceCloseList)
                    ScreenManager.instance.ForceClosePopUp(name);
                //
            }

            public static void ExecuteImmediately()
            {
                List<string> forceCloseList = ScreenManager.instance._forceCloseList;
                ISet<PopUp> openedPopUps = ScreenManager.instance._openedPopUps;

                // some popups must close when screen change
                if (forceCloseList == null) forceCloseList = new List<string>();
                else forceCloseList.Clear();

                foreach (PopUp popUp in openedPopUps)
                    if (popUp.CloseWhenScreenChange) forceCloseList.Add(popUp
                        .MonoBehaviour.gameObject.name);

                foreach (string name in forceCloseList)
                    ScreenManager.instance.ForceClosePopUp(name);
                //
            }

        }

        private class CloseAllPopUpsWithoutCommand : Command
        {
            public CloseAllPopUpsWithoutCommand(IScreenElement element, float delay = 0)
                : base(element, delay)
            {

            }

            protected override void Execute_()
            {
                IsTerminated = true;
                List<string> forceCloseList = ScreenManager.instance._forceCloseList;
                ISet<PopUp> openedPopUps = ScreenManager.instance._openedPopUps;

                if (forceCloseList == null) forceCloseList = new List<string>();
                else forceCloseList.Clear();

                foreach (PopUp popUp in openedPopUps)
                {
                    if (popUp.Equals(_element)) continue;
                    if (popUp.Status == IScreenElement.IScreenStatus.Opened)
                        ScreenManager.instance.AddCommand(new ClosePopUpCommand(popUp, 0f));
                    else
                        forceCloseList.Add(popUp.name);
                }

                foreach (string popUp in forceCloseList)
                    ScreenManager.instance.ForceClosePopUp(popUp);

            }

            public static void ExecuteImmediately(PopUp notCloseThis)
            {
                List<string> forceCloseList = ScreenManager.instance._forceCloseList;
                ISet<PopUp> openedPopUps = ScreenManager.instance._openedPopUps;

                if (forceCloseList == null) forceCloseList = new List<string>();
                else forceCloseList.Clear();

                foreach (PopUp popUp in openedPopUps)
                {
                    if (popUp.Equals(notCloseThis)) continue;
                    if (popUp.Status == IScreenElement.IScreenStatus.Opened)
                        ScreenManager.instance.AddCommand(new ClosePopUpCommand(popUp, 0f));
                    else
                        forceCloseList.Add(popUp.name);
                }

                foreach (string popUp in forceCloseList)
                    ScreenManager.instance.ForceClosePopUp(popUp);
            }

        }


        // quit app command

        // load scene command

    }



}

