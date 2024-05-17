using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Managers
{
    public static class NetworkManager
    {
        public static IEnumerator SubmitGameSessionData(
            string nickname,
            int grade,
            int school_id,
            int score,
            int timeLeft
        )
        {
            var postData = new GameSessionData
            {
                nickname = nickname,
                grade = grade,
                school_id = school_id,
                score = score,
                time_left = timeLeft
            };
            var postRequest = CreateRequest(
                "http://localhost:8000/api/gaming-sessions/create",
                RequestType.POST,
                postData
            );
            yield return postRequest.SendWebRequest();
            if (postRequest.result == UnityWebRequest.Result.Success)
                GameManager.Instance.UpdateGameState(GameManager.GameState.MainMenu);
        }

        public static IEnumerator FilterSchools(string name, Action<School[]> onSuccess)
        {
            var getRequest = CreateRequest($"http://localhost:8000/api/schools?name={name}");
            yield return getRequest.SendWebRequest();

            if (getRequest.result == UnityWebRequest.Result.Success)
            {
                var schools = JsonHelper.FromJson<School>(
                    JsonHelper.FixJson(getRequest.downloadHandler.text)
                );
                onSuccess?.Invoke(schools);
            }
        }

        public static IEnumerator GetLeaderboardList(
            uint offset,
            Action<LeaderboardPaginated> onSuccess
        )
        {
            var getRequest = CreateRequest(
                $"http://localhost:8000/api/leaderboard?limit=10&offset={offset}"
            );
            yield return getRequest.SendWebRequest();

            if (getRequest.result == UnityWebRequest.Result.Success)
            {
                var leaderboardPaginated = JsonUtility.FromJson<LeaderboardPaginated>(
                    getRequest.downloadHandler.text
                );
                onSuccess?.Invoke(leaderboardPaginated);
            }
        }

        private static UnityWebRequest CreateRequest(
            string path,
            RequestType type = RequestType.GET,
            object data = null
        )
        {
            var request = new UnityWebRequest(path, type.ToString());

            if (data != null)
            {
                var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            AttachHeader(request, "Content-Type", "application/json");

            return request;
        }

        private static void AttachHeader(UnityWebRequest request, string key, string value)
        {
            request.SetRequestHeader(key, value);
        }
    }

    public enum RequestType
    {
        GET = 0,
        POST = 1,
        PUT = 2
    }

    [Serializable]
    public class GameSessionData
    {
        public string nickname;
        public int grade;
        public int school_id;
        public int score;
        public int time_left;
    }

    [Serializable]
    public class School
    {
        public int id;
        public string name;
    }

    [Serializable]
    public class LeaderboardPaginated
    {
        public List<Leaderboard> items;
        public int count;
    }

    [Serializable]
    public class Leaderboard
    {
        public string nickname;
        public int score;
        public int time_left;
    }
}
