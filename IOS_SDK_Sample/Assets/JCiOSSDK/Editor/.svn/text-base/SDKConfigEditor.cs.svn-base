using System.IO;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace JCiOSSDK.Editor
{
    public class SDKConfigEditor
    {
        static string configName = "Classes/MS_SDK/JCiOSConfig.plist";
        /// <summary>
        /// 旧版本Config路径
        /// </summary>
        static string v1configName = "Classes/JCiOSConfig.plist";
        static string localName = "JCiOSConfig.asset";

        /// <summary>
        /// 设置SDK参数
        /// </summary>
        /// <param name="prjPath"></param>
        public static void SetJCiOSSDKConfig(string prjPath)
        {
            var cfg = ReadLocalConfig();
            if (cfg == null)
            {
                Debug.LogError("JCiOSConfig Load Error!");
                return;
            }

            var fullPath = string.Empty;
            if (cfg.IsUseOldPath)
            {
                fullPath = Path.Combine(prjPath, v1configName);
            }
            else
            {
                fullPath = Path.Combine(prjPath, configName);
            }
            
            var doc = new PlistDocument();
            doc.ReadFromFile(fullPath);

#if SdkDEVELOPER
            doc.root.SetString("appid", "ff48e91e-043e-46fc-8097-eeed0a7f3281");
            doc.root.SetString("channelid", cfg.ChannelId);
            doc.root.SetString("TalkingDataAppID", "CFB7B974233E419DA339C4B0F4DB02CA");
            doc.root.SetString("UmengAppID", "5f6386409abbe8603b9c9e61");

            doc.root.SetBoolean("ShowSplashFirst", cfg.ShowSplashFirst);
            doc.root.SetString("LogLevel", ((int)cfg.LogLevel).ToString());
#else
            doc.root.SetString("appid", cfg.AppId);
            doc.root.SetString("channelid", cfg.ChannelId);
            doc.root.SetString("ReYunAppID", cfg.ReYunAppID);
            doc.root.SetString("ReYunChannelID", cfg.ReYunChannelID);
            doc.root.SetString("ShuShuAppID", cfg.ShuShuAppID);
            doc.root.SetString("TalkingDataAppID", cfg.TalkingDataAppID);
            doc.root.SetString("UmengAppID", cfg.UmengAppID);

            doc.root.SetString("KochavaAppID", cfg.KochavaAppID);
            doc.root.SetString("TenJinAppID", cfg.TenJinAppID);
            doc.root.SetBoolean("ShowSplashFirst", cfg.ShowSplashFirst);
            doc.root.SetString("LogLevel", ((int)cfg.LogLevel).ToString());
#endif

            doc.WriteToFile(fullPath);
        }

        public static SDKConfig ReadLocalConfig()
        {
            var configPath = Path.Combine("Assets/JCiOSSDK", localName);
            return AssetDatabase.LoadAssetAtPath<SDKConfig>(configPath);
        }
    }
}
