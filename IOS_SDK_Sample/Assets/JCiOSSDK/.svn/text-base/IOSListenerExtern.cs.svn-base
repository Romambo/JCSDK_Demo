//#define SHOW_IOS_LISTENER_LOG
using AOT;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace JCiOSSDK
{
    //--------------------------------------------------
    //----------此文件为IOS Listener 外部接口文件----------
    //--------------------------------------------------

    public partial class IOSListener
    {
        /// <summary>
        /// 静态构造函数，方便自动调用
        /// </summary>
        static IOSListener()
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("IOSListener static constructor");
#endif
            //生成上下文线程同步对象
            var instance = JCiOSSDKSynchronizationContext.Instance;

#if UNITY_IPHONE && !UNITY_EDITOR
            // 使用Listener前,先调用此方法以作初始化       
            RegistCallBacknotifition();

            RegistSplashCallBack();
            RegistBannerCallBack();
            RegistIntersitialCallBack();
            RegistRewardVideoCallBack();
#endif
        }

#if UNITY_IPHONE
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ResultHandler(string resultString);

        [DllImport("__Internal")]
        static extern void RegistCallBacknotifition();

        [DllImport("__Internal")]
        static extern void splash_CallBack(IntPtr failLoad, IntPtr didShow, IntPtr didClick, IntPtr didClose);

        [DllImport("__Internal")]
        static extern void banner_CallBack(IntPtr sucessLoad, IntPtr failLoad, IntPtr didShow, IntPtr didClick, IntPtr didAutoRefresh, IntPtr tapCloseBtn, IntPtr failToAutoRefresh);

        [DllImport("__Internal")]
        static extern void Intersitial_CallBack(IntPtr failLoad, IntPtr didShow, IntPtr failToShow, IntPtr didClose, IntPtr didClick, IntPtr failToPlayVideo, IntPtr startPlayingVideo, IntPtr endPlayingVideo);

        [DllImport("__Internal")]
        static extern void rewardVideo_CallBack(IntPtr failLoad, IntPtr didRewardSuccess, IntPtr didClose, IntPtr didClick, IntPtr failToPlayVideo, IntPtr startPlayingVideo, IntPtr endPlayingVideo);

        /// <summary>
        /// 注册开屏回调
        /// </summary>
        private static void RegistSplashCallBack()
        {
            var handler1 = new ResultHandler(splashFailLoad);
            var fp1 = Marshal.GetFunctionPointerForDelegate(handler1);

            var handler2 = new ResultHandler(splashDidShow);
            var fp2 = Marshal.GetFunctionPointerForDelegate(handler2);

            var handler3 = new ResultHandler(splashDidClick);
            var fp3 = Marshal.GetFunctionPointerForDelegate(handler3);

            var handler4 = new ResultHandler(splashDidClose);
            var fp4 = Marshal.GetFunctionPointerForDelegate(handler4);

            splash_CallBack(fp1, fp2, fp3, fp4);
        }

        /// <summary>
        /// 注册banner回调
        /// </summary>
        private static void RegistBannerCallBack()
        {
            var handler0 = new ResultHandler(bannersucessLoad);
            var fp0 = Marshal.GetFunctionPointerForDelegate(handler0);

            var handler1 = new ResultHandler(bannerFailLoad);
            var fp1 = Marshal.GetFunctionPointerForDelegate(handler1);

            var handler2 = new ResultHandler(bannerDidShow);
            var fp2 = Marshal.GetFunctionPointerForDelegate(handler2);

            var handler3 = new ResultHandler(bannerDidClick);
            var fp3 = Marshal.GetFunctionPointerForDelegate(handler3);

            var handler4 = new ResultHandler(bannerDidAutoRefresh);
            var fp4 = Marshal.GetFunctionPointerForDelegate(handler4);

            var handler5 = new ResultHandler(bannerTapCloseBtn);
            var fp5 = Marshal.GetFunctionPointerForDelegate(handler5);

            var handler6 = new ResultHandler(bannerFailToAutoRefresh);
            var fp6 = Marshal.GetFunctionPointerForDelegate(handler6);
            banner_CallBack(fp0, fp1, fp2, fp3, fp4, fp5, fp6);
        }

        /// <summary>
        /// 注册插屏回调
        /// </summary>
        private static void RegistIntersitialCallBack()
        {
            var handler1 = new ResultHandler(interFailLoad);
            var fp1 = Marshal.GetFunctionPointerForDelegate(handler1);

            var handler2 = new ResultHandler(interDidShow);
            var fp2 = Marshal.GetFunctionPointerForDelegate(handler2);

            var handler3 = new ResultHandler(interFailtoShow);
            var fp3 = Marshal.GetFunctionPointerForDelegate(handler3);

            var handler4 = new ResultHandler(interDidClose);
            var fp4 = Marshal.GetFunctionPointerForDelegate(handler4);

            var handler5 = new ResultHandler(interDidClick);
            var fp5 = Marshal.GetFunctionPointerForDelegate(handler5);

            var handler6 = new ResultHandler(interFailToPlayVideo);
            var fp6 = Marshal.GetFunctionPointerForDelegate(handler6);

            var handler7 = new ResultHandler(interStartPlayingVideo);
            var fp7 = Marshal.GetFunctionPointerForDelegate(handler7);

            var handler8 = new ResultHandler(interEndPlayingVideo);
            var fp8 = Marshal.GetFunctionPointerForDelegate(handler8);

            Intersitial_CallBack(fp1, fp2, fp3, fp4, fp5, fp6, fp7, fp8);
        }

        /// <summary>
        /// 注册激励视频回调
        /// </summary>
        private static void RegistRewardVideoCallBack()
        {
            var handler1 = new ResultHandler(rewardVfailLoad);
            var fp1 = Marshal.GetFunctionPointerForDelegate(handler1);

            var handler2 = new ResultHandler(rewardVdidRewardSuccess);
            var fp2 = Marshal.GetFunctionPointerForDelegate(handler2);

            var handler3 = new ResultHandler(rewardVdidClose);
            var fp3 = Marshal.GetFunctionPointerForDelegate(handler3);

            var handler4 = new ResultHandler(rewardVdidClick);
            var fp4 = Marshal.GetFunctionPointerForDelegate(handler4);

            var handler5 = new ResultHandler(rewardVfailToPlayVideo);
            var fp5 = Marshal.GetFunctionPointerForDelegate(handler5);

            var handler6 = new ResultHandler(rewardVstartPlayingVideo);
            var fp6 = Marshal.GetFunctionPointerForDelegate(handler6);

            var handler7 = new ResultHandler(rewardVendPlayingVideo);
            var fp7 = Marshal.GetFunctionPointerForDelegate(handler7);

            rewardVideo_CallBack(fp1, fp2, fp3, fp4, fp5, fp6, fp7);
        }


        #region 视频回调
        //本地维护发奖励的状态
        private static bool isRewardSuccess;

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void rewardVfailLoad(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("激励视频回调----->rewardVfailLoad");
#endif

            if (OnRewardVedioLoadFail != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnRewardVedioLoadFail(); }, null);
            }

            //OnRewardVedioLoadFail?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void rewardVdidClick(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("激励视频回调----->rewardVdidClick");
#endif

            if (OnRewardVedioClick != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnRewardVedioClick(); }, null);
            }

            //OnRewardVedioClick?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void rewardVdidClose(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("激励视频回调----->rewardVdidClose");
#endif
            if (OnRewardVedioClose != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnRewardVedioClose(isRewardSuccess); }, null);
            }

            //OnRewardVedioClose?.Invoke(isRewardSuccess);
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void rewardVfailToPlayVideo(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("激励视频回调----->rewardVfailToPlayVideo");
#endif

            if (OnRewardVedioPlayVideoFail != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnRewardVedioPlayVideoFail(); }, null);
            }

            //OnRewardVedioPlayVideoFail?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void rewardVstartPlayingVideo(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("激励视频回调----->rewardVstartPlayingVideo");
#endif
            isRewardSuccess = false;

            if (OnRewardVedioPlayVideoStart != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnRewardVedioPlayVideoStart(); }, null);
            }

            //OnRewardVedioPlayVideoStart?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void rewardVendPlayingVideo(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("激励视频回调----->rewardVendPlayingVideo");
#endif

            if (OnRewardVedioPlayVideoEnd != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnRewardVedioPlayVideoEnd(); }, null);
            }

            //OnRewardVedioPlayVideoEnd?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void rewardVdidRewardSuccess(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("激励视频回调----->rewardVdidRewardSuccess");
#endif
            isRewardSuccess = true;
            //OnRewardVedioSuccess?.Invoke();
        }
        #endregion

        #region 插屏回调
        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void interFailLoad(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("插屏回调----->interFailLoad");
#endif

            if (OnIntersitialLoadFail != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnIntersitialLoadFail(); }, null);
            }
            //OnIntersitialLoadFail?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void interFailtoShow(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("插屏回调----->interFailtoShow");
#endif
            if (OnIntersitialShow != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnIntersitialShow(false); }, null);
            }
            //OnIntersitialShow?.Invoke(false);
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void interDidShow(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("插屏回调----->interDidShow");
#endif
            if (OnIntersitialShow != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnIntersitialShow(true); }, null);
            }
            //OnIntersitialShow?.Invoke(true);
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void interDidClick(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("插屏回调----->interDidClick");
#endif
            if (OnIntersitialClick != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnIntersitialClick(); }, null);
            }
            //OnIntersitialClick?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void interDidClose(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("插屏回调----->interDidClose");
#endif
            if (OnIntersitialClose != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnIntersitialClose(); }, null);
            }
            //OnIntersitialClose?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void interFailToPlayVideo(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("插屏回调----->interFailToPlayVideo");
#endif
            if (OnIntersitialPlayVideoFail != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnIntersitialPlayVideoFail(); }, null);
            }
            //OnIntersitialPlayVideoFail?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void interStartPlayingVideo(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("插屏回调----->interStartPlayingVideo");
#endif
            if (OnIntersitialPlayVideoStart != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnIntersitialPlayVideoStart(); }, null);
            }
            //OnIntersitialPlayVideoStart?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void interEndPlayingVideo(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("插屏回调----->interEndPlayingVideo");
#endif
            if (OnIntersitialPlayVideoEnd != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnIntersitialPlayVideoEnd(); }, null);
            }
            //OnIntersitialPlayVideoEnd?.Invoke();
        }
        #endregion

        #region Banner回调
        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void bannersucessLoad(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("banner回调----->bannersucessLoad");
#endif
            if (OnBannerLoaded != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnBannerLoaded(true); }, null);
            }
            //OnBannerLoaded?.Invoke(true);            
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void bannerFailLoad(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("banner回调----->bannerFailLoad");
#endif
            if (OnBannerLoaded != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnBannerLoaded(false); }, null);
            }
            //OnBannerLoaded?.Invoke(false);
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void bannerDidShow(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("banner回调----->bannerDidShow");
#endif
            if (OnBannerShow != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnBannerShow(); }, null);
            }
            //OnBannerShow?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void bannerDidClick(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("banner回调----->bannerDidClick");
#endif
            if (OnBannerClick != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnBannerClick(); }, null);
            }
            //OnBannerClick?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void bannerTapCloseBtn(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("banner回调----->bannerTapCloseBtn");
#endif
            if (OnBannerTapClose != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnBannerTapClose(); }, null);
            }
            //OnBannerTapClose?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void bannerFailToAutoRefresh(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("banner回调----->bannerFailToAutoRefresh");
#endif
            if (OnBannerAutoRefresh != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnBannerAutoRefresh(false); }, null);
            }
            //OnBannerAutoRefresh?.Invoke(false);
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void bannerDidAutoRefresh(string resultString)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("banner回调----->bannerDidAutoRefresh");
#endif
            if (OnBannerAutoRefresh != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnBannerAutoRefresh(true); }, null);
            }
            //OnBannerAutoRefresh?.Invoke(true);
        }
        #endregion

        #region 开屏回调

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void splashFailLoad(string resultStr)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("----->splashFailLoad");
#endif
            if (OnSplashLoadFail != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnSplashLoadFail(); }, null);
            }
            //OnSplashLoadFail?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void splashDidShow(string resultStr)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("----->splashDidShow");
#endif
            if (OnSplashShow != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnSplashShow(); }, null);
            }
            //OnSplashShow?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void splashDidClick(string resultStr)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("----->splashDidClick");
#endif
            if (OnSplashClick != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnSplashClick(); }, null);
            }
            //OnSplashClick?.Invoke();
        }

        [MonoPInvokeCallback(typeof(ResultHandler))]
        static void splashDidClose(string resultStr)
        {
#if SHOW_IOS_LISTENER_LOG
            Debug.Log("----->splashDidClose");
#endif
            if (OnSplashClose != null)
            {
                OneThreadSynchronizationContext.Instance.Post(o => { OnSplashClose(); }, null);
            }
            //OnSplashClose?.Invoke();
        }
        #endregion

#endif


#if UNITY_EDITOR
        public static void FakeIntersitialCallBack()
        {

            if (OnIntersitialShow != null)
            {
                JCiOSSDKSynchronizationContext.Instance.EditorFakeCallBack(()=>OnIntersitialShow(true));
            }

            if (OnIntersitialClose != null)
            {
                JCiOSSDKSynchronizationContext.Instance.EditorFakeCallBack(() => OnIntersitialClose(), 0.2f);
            }
        }

        public static void FakeRewardVedioCallBack()
        {
            if (OnRewardVedioPlayVideoStart != null)
            {
                JCiOSSDKSynchronizationContext.Instance.EditorFakeCallBack(() => OnRewardVedioPlayVideoStart());
            }

            if (OnRewardVedioClose != null)
            {
                JCiOSSDKSynchronizationContext.Instance.EditorFakeCallBack(() => OnRewardVedioClose(true), 0.2f);
            }
        }
#endif
    }
}