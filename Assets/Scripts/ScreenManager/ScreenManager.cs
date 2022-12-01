using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{

    private static ScreenManager instance;


    [SerializeField]
    private Screen[] _screens;
    [SerializeField]
    private PopUp[] _popUps;

    private Dictionary<string, Screen> _screenDictionary;
    private Dictionary<string, PopUp> _popUpDictionary;

    public Screen initialScreen;
    private Screen _currentScreen;
    private PopUp _currentPopUp;


    private List<Command> _commands = new List<Command>();
    private void Awake()
    {
        _screenDictionary = new Dictionary<string, Screen>();
        foreach (Screen s in _screens)
        {
            _screenDictionary.Add(s.name, s);
            _screenDictionary[s.name].gameObject.SetActive(false);
        }

        _popUpDictionary = new Dictionary<string, PopUp>();
        foreach (PopUp s in _popUps)
        {
            _popUpDictionary.Add(s.name, s);
            _popUpDictionary[s.name].gameObject.SetActive(false);
        }
        MakeSingleton();

        LoadScreen(initialScreen.name);

    }



    private Command _currentCommand;
    private void Update()
    {

        if (_commands.Count <= 0)
            return;
        if (_currentCommand != null && !_currentCommand.Terminated())
            return;

        _currentCommand = _commands[0];
        _currentCommand.Execute();
        _commands.RemoveAt(0);

    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScreen(string screenName)
    {

        if (_currentPopUp != null)
            _commands.Add(new CloseCommand(_currentPopUp));
        if(_currentScreen != null)
            _commands.Add(new CloseCommand(_currentScreen));
        _currentScreen =_screenDictionary[screenName];
        _commands.Add(new OpenCommand(_currentScreen));
    }


    public void OpenPopUp(string popUpName)
    {
        if (_currentPopUp != null)
            _commands.Add(new CloseCommand(_currentPopUp));
        _currentPopUp = _popUpDictionary[popUpName];
        _commands.Add(new OpenCommand(_currentPopUp));
    }

    public void ClosePopUp()
    {
        if(_currentPopUp!=null)
            _commands.Add(new CloseCommand(_currentPopUp));
    }

    public void QuitApplication()
    {
        Application.Quit();
    }




    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #region GetterSetter

    public static ScreenManager Instance { get => instance; }
    public Screen CurrentScreen { get => _currentScreen; }
    public PopUp CurrentPopUp { get => _currentPopUp;}


    #endregion




    private abstract class Command
    {
        protected IScreenElement element;

        public Command(IScreenElement element)
        {
            this.element = element;
        }

        public abstract void Execute();

        public abstract bool Terminated();
    }


    private class OpenCommand : Command
    {
        public OpenCommand(IScreenElement element) : base(element)
        {
        }

        public override void Execute()
        {
            element.Open();
        }

        public override bool Terminated()
        {
            return element.Opened;
        }
    }

    private class CloseCommand : Command
    {
        public CloseCommand(IScreenElement element) : base(element)
        {
        }

        public override void Execute()
        {
            element.Close();
        }

        public override bool Terminated()
        {
            return !element.Opened;
        }


    }


}


