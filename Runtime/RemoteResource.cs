using System;
using System.Collections;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class RemoteResource
{
    public string url;
    public int version;

    public bool IsUpToDate(int currentVersion)
    {
        Debug.Log($"Is up to date? currentVersion: {currentVersion} remote.version: {version}");
        return currentVersion >= version;
    }

    public Task<RequestResult<string>> GetRemoteResourceAsync(MonoBehaviour behaviour, int currentVersion = -1)
    {
        var tcs = new TaskCompletionSource<RequestResult<string>>();
        if (IsUpToDate(currentVersion))
        {
            Debug.Log($"Current resource is up to date ({currentVersion} >= ({version})): {url}");
            tcs.SetResult(RequestResult<string>.Failure(null));
            return tcs.Task;
        }

        behaviour.StartCoroutine(HttpGetCoroutine(tcs));
        return tcs.Task;
    }

    private IEnumerator HttpGetCoroutine(TaskCompletionSource<RequestResult<string>> tcs)
    {
        using (var request = UnityWebRequest.Get(url))
        {
            request.timeout = 30;
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log($"Error while downloading {url}: {request.error}");
                tcs.SetResult(RequestResult<string>.Failure(null));
                yield break;
            }

            tcs.SetResult(RequestResult<string>.Success(request.downloadHandler.text));
        }
    }

    public static RemoteResource FromJson(string json) =>
        string.IsNullOrWhiteSpace(json) ? null : JsonUtility.FromJson<RemoteResource>(json);
}
