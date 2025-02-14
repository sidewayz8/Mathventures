using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class ProgressManager : MonoBehaviour
{
    private string saveProgressUrl = "http://localhost:3000/users/saveProgress";

    public IEnumerator SaveUserProgress(string userId, int newChapter, int pointsGained)
    {
        // 1) Create a data object to serialize
        ProgressData progressData = new ProgressData {
            userId = userId,
            chapter = newChapter,
            points = pointsGained
        };

        // 2) Convert the object to JSON
        string jsonBody = JsonUtility.ToJson(progressData);

        // 3) Prepare the UnityWebRequest for a POST
        using (UnityWebRequest request = new UnityWebRequest(saveProgressUrl, "POST"))
        {
            byte[] rawJson = Encoding.UTF8.GetBytes(jsonBody);

            // 4) Upload Handler
            request.uploadHandler = new UploadHandlerRaw(rawJson);
            request.uploadHandler.contentType = "application/json";

            // 5) Download Handler (to capture the response)
            request.downloadHandler = new DownloadHandlerBuffer();

            // 6) Send the request
            yield return request.SendWebRequest();

            // 7) Check the result
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Progress saved successfully: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error saving progress: " + request.error);
            }
        }
    }
}

