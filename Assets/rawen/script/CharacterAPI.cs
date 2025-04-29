using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CharacterAPI : MonoBehaviour
{
    private string url = "http://localhost:8000/api/v1/items/add/";
    private string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0b2tlbl90eXBlIjoiYWNjZXNzIiwiZXhwIjoxNzQ1OTIxOTQzLCJpYXQiOjE3NDU5MTgzNDMsImp0aSI6ImEzZjVkNmY5YTVjZjQ3ZGI4MjNlYWU2NTQwM2QxZjNkIiwidXNlcl9pZCI6MywidXNlciI6eyJpZCI6MywiZW1haWwiOiJib3RpQGdtYWlsLmNvbSIsImlzX3N0YWZmIjpmYWxzZX19.pHxSEOZNs--LOazlyvUz2J-tckgwGHZSQRf9s_m7E0E";

    // Method to send the request with the item's name
    public void AddItem(string itemName)
    {
        StartCoroutine(SendAddItemRequest(itemName));
    }

    private IEnumerator SendAddItemRequest(string itemName)
    {
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
