using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ApiHelper : MonoBehaviour
{
    // send request to api add to header authorization(token)
    public static IEnumerator SendRequest(UnityWebRequest request, Action<UnityWebRequest> onComplete)
    {
        bool shouldRetry;
        do
        { 
            shouldRetry = false;
            
            // Add authorization header (add token)
            request.SetRequestHeader("Authorization", AuthManager.Instance.GetAuthorizationHeader());
            //envoi a serveur
            yield return request.SendWebRequest();

            // Check if we need to refresh the token
            if (AuthManager.Instance.NeedsTokenRefresh(request))
            {
                bool refreshSuccess = false;
                yield return AuthManager.Instance.RefreshTokenCoroutine((success) => refreshSuccess = success);

                if (refreshSuccess)
                {
                    // Retry the original request
                    request.Dispose();// envoi data with new autheraziation (token )
                    request = new UnityWebRequest(request.url, request.method);
                    shouldRetry = true;
                    continue;
                }
            }
//prend la reposnse succes or error
            onComplete?.Invoke(request);
            
        } while (shouldRetry);
    }

    // Helper method for GET requests
    public static IEnumerator Get(string url, Action<UnityWebRequest> onComplete)
    { //  envoi request get et return data
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return SendRequest(www, onComplete);
        }
    }

    // Helper method for POST requests
    public static IEnumerator Post(string url, string jsonData, Action<UnityWebRequest> onComplete)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            yield return SendRequest(www, onComplete);//pour autherazation et anvoi requet aux serveur
        }
    }

    // envoi data type formulaire aussi fichier
    public static IEnumerator PostForm(string url, WWWForm formData, Action<UnityWebRequest> onComplete)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(url, formData))
        {
            yield return SendRequest(www, onComplete);
        }
    }
} 