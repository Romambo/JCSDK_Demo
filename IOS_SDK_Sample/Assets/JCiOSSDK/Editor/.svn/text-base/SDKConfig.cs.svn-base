using UnityEngine;


namespace JCiOSSDK.Editor
{
    public enum LogLevel : int
    {
        AllClose = 1,
        JCLog = 2,
        JC_AD_Log = 3,
        JC_AD_DATA_Log = 4,

    }

    [CreateAssetMenu(menuName = "JCiOS/Config", fileName = "JCiOSConfig")]
    public class SDKConfig : ScriptableObject
    {

        [HideInInspector]
        public string SdkPath = @"/Volumes/MY PASSPORT/SDKFile";


        [Header("后台配置的 APP ID")]
        public string AppId;
        [Header("渠道 ID")]
        public string ChannelId = "IOS";

        [Header("热云 App ID")]
        public string ReYunAppID;
        [Header("热云 Channel ID")]
        public string ReYunChannelID;

        [Header("树树 App ID")]
        public string ShuShuAppID;

        [Header("TalkingData App ID")]
        public string TalkingDataAppID;

        [Header("友盟 App ID")]
        public string UmengAppID;

        [Header("Kochava App ID")]
        public string KochavaAppID;

        [Header("Tenjin App ID")]
        public string TenJinAppID;

        [Header("首次打开是否显示开屏广告")]
        public bool ShowSplashFirst;

        [Header("Log等级")]
        public LogLevel LogLevel = LogLevel.AllClose;

        
        [Header("使用旧版本SDK路径")]
        public bool IsUseOldPath;
    }
}