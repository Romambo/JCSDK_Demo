using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
#if UNITY_IOS || UNITY_IPHONE
using UnityEditor.iOS.Xcode;
using JCiOSSDK.Editor;


public static class JCiOSSDKPostprocess
{
    /// <summary>
    /// 要添加的系统框架
    /// </summary>
    static string[] systemFrameworks = new string[]
    {
        "Accelerate.framework",
        "AdSupport.framework",
        "AVFoundation.framework",
        "CoreGraphics.framework",
        "CoreLocation.framework",
        "CoreMedia.framework",
        "CoreTelephony.framework",
        "iAd.framework",
        "MessageUI.framework",
        "SafariServices.framework",
        "Security.framework",
        "SystemConfiguration.framework",
        "UIKit.framework",
        "VideoToolbox.framework",
        "WebKit.framework",
        "AppTrackingTransparency.framework",
        "AudioToolbox.framework",
    };


    /// <summary>
    /// SDK复制到IOS中的路径
    /// </summary>
    public static string SDKDestPath = "Classes";


    [PostProcessBuild(999)]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
    {

        //#if UNITY_IOS || UNITY_IPHONE
        if (buildTarget == BuildTarget.iOS)
        {
            string pbxprojPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

            var pbxProject = new PBXProject();
            pbxProject.ReadFromFile(pbxprojPath);

#if UNITY_2019_3_OR_NEWER
            //unity 2019版使用
            string target = pbxProject.GetUnityMainTargetGuid();
            string uTarget = pbxProject.GetUnityFrameworkTargetGuid();
#else
            //unity 2018，2017版可使用
            string target = pbxProject.TargetGuidByName("Unity-iPhone");
            string uTarget = target;
#endif
            pbxProject.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

            pbxProject.SetBuildProperty(uTarget, "ENABLE_BITCODE", "NO");

#if UNITY_2019_3_OR_NEWER
            pbxProject.SetBuildProperty(uTarget, "SUPPORTS_MACCATALYST", "NO");
#endif

            //pbxProject.SetBuildProperty(target, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");
            //pbxProject.SetBuildProperty(target, "GCC_C_LANGUAGE_STANDARD", "gnu99");

            //设置Capability
            AddCapability(pbxProject, target, path);
            // 添加系统框架
            AddSystemFramework(pbxProject, uTarget);
            // 添加外部框
            AddExternalFramework(pbxProject, target, uTarget, path);
            //添加Run Script -> 解决快手x86问题
            AddShellScriptForKUAISHOU(pbxProject, target);            
            // 设置SDK参数
            SDKConfigEditor.SetJCiOSSDKConfig(path);

            pbxProject.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
            pbxProject.AddBuildProperty(uTarget, "OTHER_LDFLAGS", "-ObjC");
            //pbxProject.AddBuildProperty(target, "OTHER_LDFLAGS", "-fobjc-arc");

            pbxProject.AddFileToBuild(uTarget, pbxProject.AddFile("usr/lib/libbz2.tbd", "Libraries/libbz2.tbd", PBXSourceTree.Sdk));
            pbxProject.AddFileToBuild(uTarget, pbxProject.AddFile("usr/lib/libz.tbd", "Libraries/libz.tbd", PBXSourceTree.Sdk));
            pbxProject.AddFileToBuild(uTarget, pbxProject.AddFile("usr/lib/libxml2.tbd", "Libraries/libxml2.tbd", PBXSourceTree.Sdk));
            pbxProject.AddFileToBuild(uTarget, pbxProject.AddFile("usr/lib/libsqlite3.tbd", "Libraries/libsqlite3.tbd", PBXSourceTree.Sdk));
            pbxProject.AddFileToBuild(uTarget, pbxProject.AddFile("usr/lib/libc++.tbd", "Libraries/libc++.tbd", PBXSourceTree.Sdk));
            pbxProject.AddFileToBuild(uTarget, pbxProject.AddFile("usr/lib/libresolv.9.tbd", "Libraries/libresolv.9.tbd", PBXSourceTree.Sdk));

            //添加本地化程序名等配置
            AddLocaleConfig(pbxProject, target, path);

            pbxProject.WriteToFile(pbxprojPath);

            //修改InfoPlist配置
            EditInfoPlist(path);

            //修改Unity启动代码以适配广告SDK
            UnityAppControllerModifier.ModfiyCode(path);
        }
        //#endif
    }

    /// <summary>
    /// 添加本地化程序名等配置
    /// </summary>  
    static void AddLocaleConfig(PBXProject pbxProject, string targetGuid, string projectPath)
    {

        string localePath = projectPath + "/Unity-iPhone";

        var cfg = BundleLocaleConfig.ReadConfig();
        var dirs = cfg.CreateFiles(localePath);

        foreach (var dir in dirs)
        {
            var guid = pbxProject.AddFolderReference(dir, Path.GetFileName(dir));
            pbxProject.AddFileToBuild(targetGuid, guid);
        }
    }

    /// <summary>
    /// 修改InfoPlist
    /// </summary>
    static void EditInfoPlist(string prjPath)
    {
        var plistPath = Path.Combine(prjPath, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        //Unity 项目常规要做的2项修改
        plist.root.SetBoolean("ITSAppUsesNonExemptEncryption", false);
        string exitsOnSuspendKey = "UIApplicationExitsOnSuspend";
        if (plist.root.values.ContainsKey(exitsOnSuspendKey))
        {
            plist.root.values.Remove(exitsOnSuspendKey);
        }

        //Google id for admob
        plist.root.SetString("GADApplicationIdentifier", "ca-app-pub-9488501426181082/7319780494");
        plist.root.SetBoolean("GADIsAdManagerApp", true);

        //Some channels use the Get Location feature internally
        plist.root.SetString("NSLocationWhenInUseUsageDescription", "The app needs to get your location");
        //Get IDFA permission configuration ，iOS14 support
        plist.root.SetString("NSUserTrackingUsageDescription", "This identifier will be used to deliver personalized ads to you");

        //使用SKAdNetwork跟踪转化：
        var elementArray = plist.root.CreateArray("SKAdNetworkItems");
        //google admob
        var dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "cstr6suwn9.skadnetwork");
        //Pangle
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "238da6jt44.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "22mmun2rn5.skadnetwork");
        //IronSource
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "SU67R6K2V3.skadnetwork");
        //UnityAds
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "4DZT52R2T5.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "bvpn9ufa9b.skadnetwork");
        //AdColony
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "4PFYVQ9L8R.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "YCLNXRL5PM.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "V72QYCH5UU.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "TL55SBB4FM.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "T38B2KH725.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "PRCB7NJMU6.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "PPXM28T8AP.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "MLMMFZH3R3.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "KLF5C3L5U5.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "HS6BDUKANM.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "C6K4G5QG8M.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "9T245VHMPL.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "9RD848Q2BZ.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "8S468MFL3Y.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "7UG5ZH24HU.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "4FZDC2EVR5.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "4468KM3ULZ.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "3RD42EKR43.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "2U9PT9HC89.skadnetwork");
        //Mintegral
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "KBD757YWX3.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "wg4vff78zm.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "737z793b9f.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "ydx93a7ass.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "prcb7njmu6.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "7UG5ZH24HU.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "44jx6755aq.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "2U9PT9HC89.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "W9Q455WK68.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "YCLNXRL5PM.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "TL55SBB4FM.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "8s468mfl3y.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "GLQZH8VGBY.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "c6k4g5qg8m.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "mlmmfzh3r3.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "4PFYVQ9L8R.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "av6w8kgt66.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "6xzpu9s2p8.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "hs6bdukanm.skadnetwork");
        //Sigmob
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "58922NB4GD.skadnetwork");
        //Maio
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "V4NXQHLYQP.skadnetwork");
        //Vungle
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "GTA9LK7P23.skadnetwork");
        //Facebook
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "v9wttpbfk9.skadnetwork");
        dict = elementArray.AddDict();
        dict.SetString("SKAdNetworkIdentifier", "n38lu8286q.skadnetwork");

        plist.WriteToFile(plistPath);
    }

    /// <summary>
    /// 添加Run Script -> 解决快手x86问题
    /// </summary>
    private static void AddShellScriptForKUAISHOU(PBXProject pbxProject, string targetGuid)
    {
        var filePath = Path.Combine(UnityEngine.Application.dataPath, "JCiOSSDK/runscript2KUAISHO.txt");
        if (!File.Exists(filePath))
        {
            UnityEngine.Debug.LogError("Lose For KUAISHOU Run Script File!" + filePath);
        }

        var scriptTxt = File.ReadAllText(filePath);
        if (!string.IsNullOrEmpty(scriptTxt))
        {
            var guid = pbxProject.AddShellScriptBuildPhase(targetGuid, "Run Script 2", "/bin/sh", scriptTxt);
        }
    }

    /// <summary>
    /// 设置iOS Capability
    /// </summary>  
    private static void AddCapability(PBXProject pbxProject, string targetGuid, string path)
    {
        var product = string.Empty;
#if UNITY_2019_3_OR_NEWER
        product = UnityEngine.Application.identifier;
        var index = product.LastIndexOf('.');
        product = product.Substring(index + 1);
#else
        product = pbxProject.GetBuildPropertyForAnyConfig(targetGuid, "PRODUCT_NAME"); 
#endif
        var rentitlementFilePath = $"Unity-iPhone/{product}.entitlements";
        var fullPath = Path.Combine(path, rentitlementFilePath);

        var doc = new PlistDocument();
        doc.root.SetBoolean("com.apple.developer.networking.wifi-info", true);
        doc.WriteToFile(fullPath);

        pbxProject.AddCapability(targetGuid, PBXCapabilityType.AccessWiFiInformation, rentitlementFilePath);
    }

 
    /// <summary>
    /// 添加系统Framework
    /// </summary>
    static void AddSystemFramework(PBXProject pbxProject, string targetGuid)
    {
        foreach (var framework in systemFrameworks)
        {
            pbxProject.AddFrameworkToProject(targetGuid, framework, false);
        }
    }

    /// <summary>
    /// 添加外部Framework
    /// </summary>  
    static void AddExternalFramework(PBXProject pbxProject, string targetGuid, string unityTargetGuid, string prjPath)
    {
        var cfg = SDKConfigEditor.ReadLocalConfig();
        string sdkPath;

        if (cfg.IsUseOldPath)
        {
            sdkPath = Path.GetFullPath(SDKCopy.OldSDKPath);
        }
        else
        {
            sdkPath = Path.GetFullPath(SDKCopy.SDKPath);

            if (!string.IsNullOrEmpty(cfg.SdkPath))
            {
                sdkPath = cfg.SdkPath;
            }
        }

        if (!Directory.Exists(sdkPath))
        {
            UnityEngine.Debug.LogError("SDK Folder is not exists!");
            return;
        }


        UnityEngine.Debug.Log("Use SDK Path: " + sdkPath);

        SDKCopy.CopySDKDirectoryToXCode(pbxProject, targetGuid, unityTargetGuid, sdkPath, prjPath, SDKDestPath);
    }
}
#endif