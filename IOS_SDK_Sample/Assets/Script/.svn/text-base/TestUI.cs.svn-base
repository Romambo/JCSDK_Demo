using JCiOSSDK;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    public Button btnBannerShow;
    public Button btnBannerHide;

    public Button btnIntersitial;
    public Button btnRewardVedio;

    public Button btnSendEvent;
    public Button btnSendEventWithData;
    public InputField inputEventName;
    public List<InputField> inputList; 

    private TestCallBack tcb;

    void Start()
    {
        RegListener();
        SetDefaultEventData();

        btnBannerShow.onClick.AddListener(OnBtnBannerShow);
        btnBannerHide.onClick.AddListener(OnBtnBannerHide);

        btnIntersitial.onClick.AddListener(OnBtnIntersitial);
        btnRewardVedio.onClick.AddListener(OnBtnRewardVedio);

        btnSendEvent.onClick.AddListener(OnBtnSendEvent);
        btnSendEventWithData.onClick.AddListener(OnBtnSendEventWithData);

        Debug.Log("------> InterShowPacing: " + IOSBridge.InterShowPacing);

    }

    private void OnBtnSendEventWithData()
    {
        var eventName = inputEventName.text.Trim();
        if (string.IsNullOrEmpty(eventName))
        {
            Debug.LogError("<----->Event Name is null");
            return;
        }

        IOSBridge.SendEvent(eventName, GetDictData());
    }

    private void OnBtnSendEvent()
    {
        var eventName = inputEventName.text.Trim();
        if (string.IsNullOrEmpty(eventName)) 
        {
            Debug.LogError("<----->Event Name is null");
            return;
        }

        IOSBridge.SendEvent(eventName);
    }

    private Dictionary<string, string> GetDictData()
    {
        var dict = new Dictionary<string, string>();
       
        for (int i = 0, j = 1; i < inputList.Count; i += 2, j += 2)
        {            
            dict.Add( inputList[i].text, inputList[j].text);
        }      

        return dict;
    }

    private void SetDefaultEventData()
    {
        inputEventName.text = "Event_Test_1";

        for (int i = 0, j = 1; i < inputList.Count; i+=2, j += 2)
        {
            inputList[i].text = "Data_Key_" + i;
            if (j % 3 == 0)
            {
                inputList[j].text = j.ToString();
            }
            else
            {
                inputList[j].text = "Data_Value_" + i;
            }            
        }

    }

    private void RegListener()
    {
        tcb = new TestCallBack();

        IOSListener.OnSplashLoadFail += tcb.OnSplashLoadFail;
        IOSListener.OnSplashShow += tcb.OnSplashShow;
        IOSListener.OnSplashClick += tcb.OnSplashClick;
        IOSListener.OnSplashClose += tcb.OnSplashClose;

        IOSListener.OnBannerLoaded += tcb.OnBannerLoaded;
        IOSListener.OnBannerShow += tcb.OnBannerShow;
        IOSListener.OnBannerClick += tcb.OnBannerClick;
        IOSListener.OnBannerTapClose += tcb.OnBannerTapClose;
        IOSListener.OnBannerAutoRefresh += tcb.OnBannerAutoRefresh;

        IOSListener.OnIntersitialLoadFail += tcb.OnIntersitialLoadFail;
        IOSListener.OnIntersitialShow += tcb.OnIntersitialShow;
        IOSListener.OnIntersitialClick += tcb.OnIntersitialClick;
        IOSListener.OnIntersitialClose += tcb.OnIntersitialClose;
        IOSListener.OnIntersitialPlayVideoFail += tcb.OnIntersitialPlayVideoFail;
        IOSListener.OnIntersitialPlayVideoStart += tcb.OnIntersitialPlayVideoStart;
        IOSListener.OnIntersitialPlayVideoEnd += tcb.OnIntersitialPlayVideoEnd;

        IOSListener.OnRewardVedioLoadFail += tcb.OnRewardVedioLoadFail;
        IOSListener.OnRewardVedioClick += tcb.OnRewardVedioClick;
        IOSListener.OnRewardVedioClose += tcb.OnRewardVedioClose;
        IOSListener.OnRewardVedioPlayVideoFail += tcb.OnRewardVedioPlayVideoFail;
        IOSListener.OnRewardVedioPlayVideoStart += tcb.OnRewardVedioPlayVideoStart;
        IOSListener.OnRewardVedioPlayVideoEnd += tcb.OnRewardVedioPlayVideoEnd;        
    }

    private void OnBtnRewardVedio()
    {
        IOSBridge.TryShowRewardVideo();
        Debug.Log("<-----> OnBtnRewardVedio, Main Thread ID: " + System.Threading.Thread.CurrentThread.ManagedThreadId);
    }

    private void OnBtnIntersitial()
    {
        IOSBridge.TryShowIntersitial();
    }

    private void OnBtnBannerHide()
    {
        IOSBridge.RemoveBanner();
    }

    private void OnBtnBannerShow()
    {
        IOSBridge.ShowBanner();
    }
}
