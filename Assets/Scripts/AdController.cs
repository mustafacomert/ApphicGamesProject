using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdController : MonoBehaviour, IUnityAdsListener
{
    private string placement = "rewardedVideo";
    [SerializeField]
    private HeartController hc; 
    void IUnityAdsListener.OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    void IUnityAdsListener.OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {
            PlayerPrefs.SetInt("currentHeartCount", 3);
            hc.Awake();
        }
    }

    public void ShowAd()
    {
        Advertisement.Show(placement);
    }
    void IUnityAdsListener.OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    void IUnityAdsListener.OnUnityAdsReady(string placementId)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("3610366", true);
    }

   
}
