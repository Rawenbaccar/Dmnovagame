using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CharacterAPI : MonoBehaviour
{
    private string url = "http://51.255.29.221:8866/api/v1/items/add/";

    // Method to send the request with the item's name
    public void AddItem(string itemName)
    {
        StartCoroutine(SendAddItemRequest(itemName));
    }

    private IEnumerator SendAddItemRequest(string itemName)
    {
        string accessToken = AuthManager.Instance.GetAccessToken();

        // Check if token is valid
        if (string.IsNullOrEmpty(accessToken))
        {
            Debug.LogWarning("Access token is not available. Cannot send request.");
            yield break;
        }
        // Create the JSON body
        string jsonBody = "{\"name\":\"" + itemName + "\"}";

        // Create a UnityWebRequest to the provided URL
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            // Set the request body
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();

            // Set the content type header to indicate it's JSON
            request.SetRequestHeader("Content-Type", "application/json");
            // Add the Authorization header with the Bearer token
            request.SetRequestHeader("Authorization", "Bearer " + accessToken);

            // Send the request and wait for the response
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Item added successfully: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error while adding item: " + request.error);
            }
        }
    }
}
