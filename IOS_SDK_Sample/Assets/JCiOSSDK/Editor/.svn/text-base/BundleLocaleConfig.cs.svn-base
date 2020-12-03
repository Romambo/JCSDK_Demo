using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace JCiOSSDK.Editor
{

    [CreateAssetMenu(menuName = "JCiOS/Locale Config", fileName = "BundleLocaleConfig")]
    public class BundleLocaleConfig : ScriptableObject
    {
        string folderSuffix = ".lproj";
        string fileName = "InfoPlist.strings";

        string key0 = "CFBundleDisplayName";
        string key1 = "NSLocationWhenInUseUsageDescription";
        string key2 = "NSUserTrackingUsageDescription";

        string format = "\"{0}\" = \"{1}\";";

        Dictionary<ELocale, string> localNameDict = new Dictionary<ELocale, string>()
    {
        {ELocale.en, "en"},
        {ELocale.ja, "ja"},
        {ELocale.zh_Hans, "zh-Hans"},
        {ELocale.zh_Hant, "zh-Hant"},
        {ELocale.zh_HK, "zh-HK"},
    };

        [Header("ProductName | 程序名")]
        public LocaleValues productName;

        //[Header("LocationWhenInUseUsageDescription")]
        [HideInInspector]
        public LocaleValues LocationDesc = LocaleValues.DefaultLOCA();

        //[Header("UserTrackingUsageDescription")]
        [HideInInspector]
        public LocaleValues TrackingDesc = LocaleValues.DefaultIDFA();



        public List<string> CreateFiles(string path)
        {
            var strB = new StringBuilder();
            var folders = new List<string>();
            foreach (var item in localNameDict)
            {
                strB.Clear();

                strB.AppendFormat(format, key0, productName.GetValue(item.Key));
                strB.AppendLine();
                strB.AppendFormat(format, key1, LocationDesc.GetValue(item.Key));
                strB.AppendLine();
                strB.AppendFormat(format, key2, TrackingDesc.GetValue(item.Key));
                strB.AppendLine();

                var dir = CreateLocaleFolder(item.Key, path);
                folders.Add(dir);

                SaveToFile(dir, strB.ToString());
            }

            return folders;
        }

        private void SaveToFile(string path, string content)
        {
            var filePath = Path.Combine(path, fileName);
            using (var fs = new FileStream(filePath, FileMode.CreateNew))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(content);
                    sw.Flush();
                }
            };
        }


        private string CreateLocaleFolder(ELocale locale, string path)
        {
            var name = localNameDict[locale] + folderSuffix;
            var fullName = Path.Combine(path, name);

            if (Directory.Exists(fullName))
            {
                Directory.Delete(fullName, true);
            }

            Directory.CreateDirectory(fullName);

            return fullName;
        }

        public static BundleLocaleConfig ReadConfig()
        {
            var configPath = "Assets/JCiOSSDK/BundleLocaleConfig.asset";
            return AssetDatabase.LoadAssetAtPath<BundleLocaleConfig>(configPath);
        }

        //[UnityEditor.MenuItem("Test/t1")]
        //static void Test()
        //{
        //    string testPath = @"/Users/magicpower/Desktop/UnityPrj/Locale2/Assets/BundleLocale/aaaa";
        //    var cfg = ReadConfig();

        //    cfg.CreateFiles(testPath);
        //}

    }


    public enum ELocale
    {
        en,
        ja,
        zh_Hans,
        zh_Hant,
        zh_HK,
    }


    [Serializable]
    public class LocaleValues
    {
        const string default_en_IDFA = "The application needs to access your IDFA to track the source of advertising and provide you with more accurate advertising services";
        //const string default_en_LOCA = "The application needs to get your location";
        const string default_en_LOCA = "The application needs to access your location to provide you with more accurate advertising services";

        const string default_cn_IDFA = "应用需要访问您的IDFA用来追踪广告来源，便于向您提供更精准的广告服务";
        const string default_cn_LOCA = "应用程序需要访问您的位置，用来向您提供更精准的广告服务";

        [Header("英语")]
        public string EN;
        [Header("日语")]
        public string JA;
        [Header("中文(简)")]
        public string ZH_Hans;
        [Header("中文(繁)")]
        public string ZH_Hant;
        [Header("中文(香港)")]
        public string ZH_HK;

        public void SetValue(string en, string cn)
        {
            EN = en;
            JA = en;
            ZH_Hans = cn;
            ZH_Hant = cn;
            ZH_HK = cn;
        }

        public static LocaleValues DefaultIDFA()
        {
            var obj = new LocaleValues();
            obj.SetValue(default_en_IDFA, default_cn_IDFA);
            return obj;
        }

        public static LocaleValues DefaultLOCA()
        {
            var obj = new LocaleValues();
            obj.SetValue(default_en_LOCA, default_cn_LOCA);
            return obj;
        }

        public string GetValue(ELocale locale)
        {
            switch (locale)
            {
                case ELocale.en:
                    return EN;
                case ELocale.ja:
                    return JA;
                case ELocale.zh_Hans:
                    return ZH_Hans;
                case ELocale.zh_Hant:
                    return ZH_Hant;
                case ELocale.zh_HK:
                    return ZH_HK;
                default:
                    return EN;
            }
        }
    }
}