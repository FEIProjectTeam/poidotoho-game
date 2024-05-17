using System.Collections.Generic;
using Managers;
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

        private VisualElement _leaderboardBody;
        private Button _firstPageBtn;
        private Button _prevPageBtn;
        private Button _nextPageBtn;
        private uint _offset;

        private void Start()
        {
            var root = _document.rootVisualElement;
            root.Clear();
            root.styleSheets.Add(_styleSheet);

            var container = Utils.Create(addTo: root, "container");
            Utils.Create<Label>(addTo: container, "title").text = "POI do toho!";
            var btnContainer = Utils.Create(addTo: container);

            var startButton = Utils.Create<Button>(addTo: btnContainer, "menu-btn");
            startButton.text = "SPUSTIŤ HRU";
            startButton.clicked += () =>
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.StartPlaying);
            };

            var leaderboardBtn = Utils.Create<Button>(addTo: btnContainer, "menu-btn");
            leaderboardBtn.text = "REBRÍČEK";
            leaderboardBtn.clicked += OpenLeaderboard;
        }

        private void OpenLeaderboard()
        {
            var root = _document.rootVisualElement;
            var overlay = Utils.Create(addTo: root, "leaderboard-overlay");
            var popup = Utils.Create(addTo: overlay, "leaderboard-popup");

            var leaderboardHeader = Utils.Create(addTo: popup, "leaderboard-header", "h-15pe");
            Utils.Create<Label>(addTo: leaderboardHeader, "w-10pe").text = "#";
            Utils.Create<Label>(addTo: leaderboardHeader, "w-50pe").text = "Prezývka";
            Utils.Create<Label>(addTo: leaderboardHeader, "w-10pe").text = "Skóre";
            Utils.Create<Label>(addTo: leaderboardHeader, "w-30pe").text = "Zostávajúci čas";

            _leaderboardBody = Utils.Create(addTo: popup, "flex-col", "h-70pe");

            var leaderboardFooter = Utils.Create(
                addTo: popup,
                "flex-row",
                "justify-between",
                "h-15pe",
                "px-40px"
            );
            var footerLeft = Utils.Create(addTo: leaderboardFooter, "my-auto");
            var footerRight = Utils.Create(addTo: leaderboardFooter, "flex-row", "my-auto");

            var closeBtn = Utils.Create<Button>(addTo: footerLeft, "leaderboard-close-btn");
            closeBtn.text = "Zavrieť";
            closeBtn.clicked += () =>
            {
                root.Remove(overlay);
            };

            _firstPageBtn = Utils.Create<Button>(addTo: footerRight, "leaderboard-page-btn");
            _firstPageBtn.text = "<<";
            _firstPageBtn.SetEnabled(false);
            _firstPageBtn.clicked += () =>
            {
                _offset = 0;
                GetLeaderboardData();
            };

            _prevPageBtn = Utils.Create<Button>(addTo: footerRight, "leaderboard-page-btn");
            _prevPageBtn.text = "<";
            _prevPageBtn.SetEnabled(false);
            _prevPageBtn.clicked += () =>
            {
                if (10 <= _offset)
                    _offset -= 10;
                else
                    _offset = 0;
                GetLeaderboardData();
            };

            _nextPageBtn = Utils.Create<Button>(addTo: footerRight, "leaderboard-page-btn");
            _nextPageBtn.text = ">";
            _nextPageBtn.SetEnabled(false);
            _nextPageBtn.clicked += () =>
            {
                _offset += 10;
                GetLeaderboardData();
            };

            _offset = 0;
            GetLeaderboardData();
        }

        private void UpdateLeaderboard(LeaderboardPaginated leaderboardPaginated)
        {
            _leaderboardBody.Clear();
            var order = _offset;
            foreach (Leaderboard l in leaderboardPaginated.items)
            {
                order++;
                var item = Utils.Create(addTo: _leaderboardBody, "leaderboard-item");
                Utils.Create<Label>(addTo: item, "w-10pe", "border-r-2").text = order.ToString();
                Utils.Create<Label>(addTo: item, "w-50pe", "border-r-2").text = l.nickname;
                Utils.Create<Label>(addTo: item, "w-10pe", "border-r-2").text = l.score.ToString();
                Utils.Create<Label>(addTo: item, "w-30pe").text = ScoreTimeManager.FormatTime(
                    l.time_left
                );
            }

            if (_offset + 10 < leaderboardPaginated.count)
                _nextPageBtn.SetEnabled(true);
            else
                _nextPageBtn.SetEnabled(false);
            if (_offset > 0)
            {
                _firstPageBtn.SetEnabled(true);
                _prevPageBtn.SetEnabled(true);
            }
            else
            {
                _firstPageBtn.SetEnabled(false);
                _prevPageBtn.SetEnabled(false);
            }
        }

        private void GetLeaderboardData()
        {
            StartCoroutine(NetworkManager.GetLeaderboardList(_offset, UpdateLeaderboard));
        }
    }
}
