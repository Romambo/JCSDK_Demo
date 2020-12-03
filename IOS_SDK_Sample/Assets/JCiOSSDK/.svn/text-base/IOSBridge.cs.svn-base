//#define SHOW_IOS_ADSTATE_LOG
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace JCiOSSDK
{
    //--------------------------------------------------
    //----------此文件为IOS Bridge 调用、使用文件----------
    //--------------------------------------------------

    public partial class IOSBridge
    {
        /// <summary>
        /// Banner最后的显示状态
        /// 如果ShowBanner时Banner未加载好，保留此状态
        /// 在回调加载成功时，依据此状态来决定是否显示
        /// </summary>
        private static bool BannerLastState;

        /// <summary>
        /// 插屏播放时间间隔
        /// </summary>
        public static int InterShowPacing
        {
            get
            {
                return Inter_showpacingCallBack();
            }
        }

        /// <summary>
        /// 插屏-->是否可用
        /// </summary>
        public static bool IsIntersitialReady()
        {
            var value = isReadyIntersitial();
#if SHOW_IOS_ADSTATE_LOG
            Debug.Log("-----> IsInterReady:" + value);
#endif
            return value;
        }

        /// <summary>
        /// 插屏-->调用显示
        /// </summary>
        public static void ShowIntersitial()
        {
            showIntersitial();
        }

        /// <summary>
        /// 插屏-->如可用则调用显示，同时返回可用状态
        /// </summary>
        public static bool TryShowIntersitial()
        {
            var value = isReadyIntersitial();
            if (value)
            {
                showIntersitial();
            }

            return value;
        }

        /// <summary>
        /// 激励视频-->是否可用
        /// </summary>
        public static bool IsRewardVideoReady()
        {
            var value = isReadyRewardVideo();
#if SHOW_IOS_ADSTATE_LOG
            Debug.Log("-----> isReadyRewardV:" + value);
#endif
            return value;
        }

        /// <summary>
        /// 激励视频-->调用显示
        /// </summary>
        public static void ShowRewardVideo()
        {
            showRewardVideo();
        }

        /// <summary>
        /// 激励视频-->如可用则调用显示，同时返回可用状态
        /// </summary>
        public static bool TryShowRewardVideo()
        {
            var value = isReadyRewardVideo();
            if (value)
            {
                showRewardVideo();
            }

            return value;
        }

        /// <summary>
        /// Banner->调用显示
        /// </summary>
        public static void ShowBanner()
        {
            BannerLastState = true;

            var value = isReadyBanner();
#if SHOW_IOS_ADSTATE_LOG
            Debug.Log("-----> isReadyBanner:" + value);
#endif
            if (value)
            {
                showBannerView();
            }
        }

        /// <summary>
        /// Banner->移除
        /// </summary>
        public static void RemoveBanner()
        {
            BannerLastState = false;
            removeBannerView();
        }

        /// <summary>
        /// 发送埋点数据
        /// </summary>
        public static void SendEvent(string eventName)
        {
            sendEvent(eventName, null);
        }

        /// <summary>
        /// 发送埋点数据
        /// </summary>
        public static void SendEvent(string eventName, Dictionary<string, string> eventData)
        {
            if (eventData != null && eventData.Count > 0)
            {
                sendEvent(eventName, DictionaryToJson(eventData));
            }
            else
            {
                sendEvent(eventName, null);
            }
        }

        static string DictionaryToJson(Dictionary<string, string> dict)
        {
            var builder = new StringBuilder("{");
            foreach (KeyValuePair<string, string> kv in dict)
            {
                builder.AppendFormat("\"{0}\":\"{1}\",", kv.Key, kv.Value);
            }
            builder[builder.Length - 1] = '}';
            return builder.ToString();
        }
    }
}