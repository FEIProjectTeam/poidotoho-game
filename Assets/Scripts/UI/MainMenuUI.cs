using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField]
        private UIDocument _document;

        [SerializeField]
        private StyleSheet _styleSheet;

        private void Start()
        {
            var root = _document.rootVisualElement;
            root.styleSheets.Add(_styleSheet);

            var container = Utils.Create(addTo: root, "container");

            var startButton = Utils.Create<Button>(addTo: container, "start-button");
            startButton.text = "ZAČAŤ HRU";
            startButton.clicked += () =>
            {
                LevelManager.LoadScene("DemoScene");
            };
        }
    }
}
