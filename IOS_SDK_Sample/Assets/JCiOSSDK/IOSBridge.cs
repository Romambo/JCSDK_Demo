//#define SHOW_IOS_ADSTATE_LOG
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace JCiOSSDK
{
    //--------------------------------------------------
    //----------This file is called and used by IOS Bridge----------
    //--------------------------------------------------

    public partial class IOSBridge
    {
        /// <summary>
        /// The final display status of the Banner
        /// If the banner is not loaded during ShowBanner, keep this state
        /// When the callback is successfully loaded, determine whether to display according to this status
        /// </summary>
        private static bool BannerLastState;

        /// <summary>
        /// Interstitial playback time interval
        /// </summary>
        public static int InterShowPacing
        {
            get
            {
                return Inter_showpacingCallBack();
            }
        }

        /// <summary>
        /// Intersitial-->it's usable or not
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
        /// Intersitial-->show
        /// </summary>
        public static void ShowIntersitial()
        {
            showIntersitial();
        }

        /// <summary>
        /// Intersitial-->Call the display if available, and return to the available state at the same time
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
        /// rewardVideo-->it's usable or not
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
        /// rewardVideo-->show
        /// </summary>
        public static void ShowRewardVideo()
        {
            showRewardVideo();
        }

        /// <summary>
        /// rewardVideo-->Call the display if available, and return to the available state at the same time
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
        /// Banner->show
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
        /// Banner->remove
        /// </summary>
        public static void RemoveBanner()
        {
            BannerLastState = false;
            removeBannerView();
        }

        /// <summary>
        /// Send buried point data
        /// </summary>
        public static void SendEvent(string eventName)
        {
            sendEvent(eventName, null);
        }

        /// <summary>
        /// Send buried point data
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