using UnityEngine;
using UnityEngine.UIElements;

public class StartSceen : MonoBehaviour
{
    [SerializeField] private UIDocument _document;
    [SerializeField] private StyleSheet _styleSheet;

    public Texture2D backgroundImage;

    private void Start()
    {
        var root = _document.rootVisualElement;
        root.styleSheets.Add(_styleSheet);

        var background = new Image();
        background.image = backgroundImage;
        background.scaleMode = ScaleMode.ScaleAndCrop;
        root.Add(background);


        var startButton = new Button(GameStart);
        startButton.AddToClassList("start-button");
        startButton.text = "ZAČAŤ HRU";
        root.Add(startButton);
    }

    void GameStart()
    {
        LevelManager.LoadScene("DemoScene");
    }
}
