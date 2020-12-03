using UnityEngine;
using UnityEditor;


namespace JCiOSSDK.Editor
{

    [CustomEditor(typeof(SDKConfig), true)]
    public class SDKConfigInspector : UnityEditor.Editor
    {
        SDKConfig cfg;

        void OnEnable()
        {
            cfg = target as SDKConfig;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("自定义SDK路径:");
            EditorGUILayout.LabelField(cfg.SdkPath);
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("浏览..."))
            {
                BrownSDKFolder();
            }
            GUILayout.Space(30);
            if (GUILayout.Button("清除"))
            {
                cfg.SdkPath = "";
                EditorUtility.SetDirty(cfg);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            GUILayout.Space(30);

            base.OnInspectorGUI();

            //GUILayout.Space(30);
            //EditorGUILayout.
            //cfg.IsUseOldPath = EditorGUILayout.Toggle("使用旧版本路径",cfg.IsUseOldPath);
        }

        private void BrownSDKFolder()
        {
            var path = EditorUtility.OpenFolderPanel("Select SDK Folder", "", "");
            if (string.IsNullOrEmpty(path)) return;

            cfg.SdkPath = path;
            EditorUtility.SetDirty(cfg);
        }

    }

}
