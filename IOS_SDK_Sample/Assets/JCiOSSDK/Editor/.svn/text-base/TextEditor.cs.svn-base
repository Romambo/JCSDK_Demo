using System.IO;
using UnityEngine;

//参见：https://www.jianshu.com/p/366110eb1610

namespace JCiOSSDK.Editor
{
    /// <summary>
    /// 用于修改Unity导出的mm文件
    /// </summary>
    public class TextEditor
    {
        private string filePath;

        public TextEditor(string fPath)
        {
            filePath = fPath;
            if (!File.Exists(filePath))
            {
                Debug.LogError(filePath + "路径下文件不存在");
                return;
            }
        }

        /// <summary>
        /// 查找对应标识，再之后添加代码
        /// </summary> 
        public void WriteBelow(string below, string text)
        {
            var streamReader = new StreamReader(filePath);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();

            int beginIndex = text_all.IndexOf(below);
            if (beginIndex == -1)
            {
                Debug.LogError(filePath + "中没有找到标识:" + below);
                return;
            }

            int endIndex = text_all.LastIndexOf("\n", beginIndex + below.Length);

            text_all = text_all.Substring(0, endIndex) + "\n" + text + "\n" + text_all.Substring(endIndex);

            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(text_all);
            streamWriter.Close();
        }

        /// <summary>
        /// 替换代码
        /// </summary>  
        public void Replace(string below, string newText)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string text_all = streamReader.ReadToEnd();
            streamReader.Close();

            int beginIndex = text_all.IndexOf(below);
            if (beginIndex == -1)
            {
                Debug.LogError(filePath + "中没有找到标识:" + below);
                return;
            }

            text_all = text_all.Replace(below, newText);
            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(text_all);
            streamWriter.Close();
        }
    }
}