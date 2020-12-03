using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using UnityEngine;

namespace JCiOSSDK.Editor
{
    public static class ExtensionName
    {

        public const string META = ".meta";
        public const string RTF = ".rtf";
        public const string ARCHIVE = ".a";
        public const string FRAMEWORK = ".framework";
        public const string BUNDLE = ".bundle";

    }

    public class SDKCopy
    {
        static string sdkPath = Application.dataPath + "/../../SDKFile";
        //旧版本时，使用相对路径
        static string v1sdkPath = Application.dataPath + "/../../JCSDK";

        public static string SDKPath { get { return sdkPath; } }
        public static string OldSDKPath { get { return v1sdkPath; } }

        /// <summary>
        /// 要进行嵌入的外部框架
        /// </summary>
        static List<string> embedFrameworkList = new List<string>()
        {
            "KSAdSDK.framework",
            "KochavaAdNetwork.framework",
            "KochavaCore.framework",
            "KochavaTracker.framework",
        };


        /// <summary>
        /// 将SDK复制到XCode项目
        /// </summary>
        /// <param name="pbxProject">PBXProject</param>
        /// <param name="targetGuid">Target Guid</param>
        /// <param name="unityTargetGuid">UnityFramework Guid, since 2019.3</param>
        /// <param name="sourcePath">SDK路径</param>
        /// <param name="destPath">XCode项目路径</param>
        /// <param name="destRelativePath">XCode项目用于复制SDK的相对路径</param>
        /// <param name="isAddBuild">是否加入Build</param>
        public static void CopySDKDirectoryToXCode(PBXProject pbxProject, string targetGuid, string unityTargetGuid, string sourcePath, string destPath, string destRelativePath, bool isAddBuild = true)
        {
            var files = Directory.GetFiles(sourcePath);
            var buildPhaseId = pbxProject.AddSourcesBuildPhase(targetGuid);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                if (fileName[0] == '.') continue;

                string fileExtension = Path.GetExtension(file);

                if (fileExtension == ExtensionName.META || fileExtension == ExtensionName.RTF)
                {
                    continue;
                }
                else if (fileExtension == ExtensionName.ARCHIVE)
                {
                    pbxProject.AddBuildProperty(unityTargetGuid, "LIBRARY_SEARCH_PATHS", "$(PROJECT_DIR)/" + destRelativePath);
                }

                string copyPath = Path.Combine(destPath, destRelativePath);

                if (!Directory.Exists(copyPath))
                {
                    Directory.CreateDirectory(copyPath);
                }

                File.Copy(file, Path.Combine(copyPath, fileName), true);

                if (isAddBuild)
                {
                    var buildPath = Path.Combine(destRelativePath, fileName);
                    var newGuid = pbxProject.AddFile(buildPath, buildPath, PBXSourceTree.Source);


#if UNITY_2019_3_OR_NEWER

                    var ext = Path.GetExtension(fileName);
                    if (ext.Equals(".h", StringComparison.CurrentCultureIgnoreCase) || ext.Equals(".m", StringComparison.CurrentCultureIgnoreCase))
                    {

                        pbxProject.AddFileToBuildSection(unityTargetGuid, buildPhaseId, newGuid);
                    }
                    else
                    {
                        if (fileName.Equals("JCiOSConfig.plist"))
                        {
                            pbxProject.AddFileToBuild(targetGuid, newGuid);
                        }
                        else
                        {
                            pbxProject.AddFileToBuild(unityTargetGuid, newGuid);
                        }
                    }
#else
                        pbxProject.AddFileToBuild(unityTargetGuid, newGuid);
#endif
                }
            }

            var dirs = Directory.GetDirectories(sourcePath);

            foreach (var dir in dirs)
            {
                var dirName = Path.GetFileName(dir);
                if (dirName.StartsWith(".")) continue;

                bool needAddBuild = isAddBuild;

                if (dirName.EndsWith(ExtensionName.FRAMEWORK) || dirName.EndsWith(ExtensionName.BUNDLE))
                {
                    var buildPath = Path.Combine(destRelativePath, dirName);
                    var newGuid = pbxProject.AddFile(buildPath, buildPath, PBXSourceTree.Source);
                    pbxProject.AddFileToBuild(unityTargetGuid, newGuid);
                    pbxProject.AddBuildProperty(unityTargetGuid, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/" + destRelativePath);
                    needAddBuild = false;

                    if (embedFrameworkList.Contains(dirName))
                    {
                        PBXProjectExtensions.AddFileToEmbedFrameworks(pbxProject, targetGuid, newGuid);
                    }
                }

                CopySDKDirectoryToXCode(pbxProject, targetGuid, unityTargetGuid, dir, destPath, Path.Combine(destRelativePath, dirName), needAddBuild);
            }

        }



        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="sourcePath">源路径</param>
        /// <param name="destPath">目标路径</param>
        public static void DirectoryCopy(string sourcePath, string destPath)
        {
            var dirInfo = new DirectoryInfo(sourcePath);

            if (!dirInfo.Exists) return;

            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            var files = dirInfo.GetFiles();

            foreach (var file in files)
            {
                //if (file.FullName.EndsWith(".meta")) continue;

                string tempPath = Path.Combine(destPath, file.Name);
                file.CopyTo(tempPath, false);
            }

            var subDirs = dirInfo.GetDirectories();
            foreach (var d in subDirs)
            {
                string tempPath = Path.Combine(destPath, d.Name);
                DirectoryCopy(d.FullName, tempPath);
            }
        }


        //--------iOS需要将所有的文件都Add Guid到项目中并且要指定Search路径，下列方法并不适用---------       

        ///// <summary>
        ///// 读取Framework Bundle的相对路径
        ///// </summary>
        ///// <param name="sdkPath">SDK所在路径</param>
        //public static List<string> GetFrameworkBundleRelativePath()
        //{
        //    var dir = new DirectoryInfo(sdkPath);
        //    var fullPath = dir.FullName;
        //    var pathHeadLength = fullPath.Length + 1;

        //    var nameList = new List<string>();
        //    GetFrameworkBundleRelativePath(fullPath, nameList);

        //    for (int i = 0; i < nameList.Count; i++)
        //    {
        //        nameList[i] = nameList[i].Substring(pathHeadLength);
        //    }

        //    return nameList;
        //}

        ///// <summary>
        ///// 复制SDK
        ///// </summary>
        ///// <param name="destPath">目标路径</param>
        //public static void CopySdkTo(string destPath)
        //{
        //    DirectoryCopy(sdkPath, destPath);
        //}

        //private static void GetFrameworkBundleRelativePath(string path, List<string> nameList)
        //{
        //    var dir = new DirectoryInfo(path);

        //    if (!dir.Exists) return;

        //    var subDirs = dir.GetDirectories();

        //    foreach (var d in subDirs)
        //    {
        //        if (d.Name.EndsWith(".framework", StringComparison.CurrentCultureIgnoreCase) ||
        //            d.Name.EndsWith(".bundle", StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            nameList.Add(d.FullName);
        //        }
        //        else
        //        {
        //            GetFrameworkBundleRelativePath(d.FullName, nameList);
        //        }
        //    }
        //}

    }
}