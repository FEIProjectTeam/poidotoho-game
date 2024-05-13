﻿using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Managers
{
    public static class NetworkManager
    {
        public static IEnumerator SubmitGameSessionData(string nickname, int score, int timeLeft)
        {
            var postData = new GameSessionData
            {
                nickname = nickname,
                score = score,
                time_left = timeLeft
            };
            var postRequest = CreateRequest(
                "localhost:8000/api/gaming-sessions/create",
                RequestType.POST,
                postData
            );
            yield return postRequest.SendWebRequest();
            if (postRequest.result != UnityWebRequest.Result.Success)
                Debug.LogError(postRequest.error);
            else
                GameManager.Instance.UpdateGameState(GameManager.GameState.MainMenu);
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
        public int score;
        public int time_left;
    }
}