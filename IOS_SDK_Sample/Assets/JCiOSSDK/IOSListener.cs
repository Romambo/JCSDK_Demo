using System;

namespace JCiOSSDK
{
    //--------------------------------------------------
    //----------此文件为IOS Listener 调用、使用文件----------
    //--------------------------------------------------

    public partial class IOSListener
    {
        /// <summary>
        /// 开屏->加载失败
        /// </summary>
        public static event Action OnSplashLoadFail;
        /// <summary>
        /// 开屏->显示
        /// </summary>
        public static event Action OnSplashShow;
        /// <summary>
        /// 开屏->点击
        /// </summary>
        public static event Action OnSplashClick;
        /// <summary>
        /// 开屏->关闭
        /// </summary>
        public static event Action OnSplashClose;



        /// <summary>
        /// Banner->加载失败
        /// </summary>
        public static event Action<bool> OnBannerLoaded;
        /// <summary>
        /// Banner->显示
        /// </summary>
        public static event Action OnBannerShow;
        /// <summary>
        /// Banner->点击
        /// </summary>
        public static event Action OnBannerClick;
        /// <summary>
        /// Banner->点击关闭
        /// </summary>
        public static event Action OnBannerTapClose;
        /// <summary>
        /// Banner->自动刷新
        /// 参数 bool: 是否刷新成功
        /// </summary>
        public static event Action<bool> OnBannerAutoRefresh;



        /// <summary>
        /// 插屏->加载失败
        /// </summary>
        public static event Action OnIntersitialLoadFail;
        /// <summary>
        /// 插屏->展示
        /// 参数 bool: 是否展示成功
        /// </summary>
        public static event Action<bool> OnIntersitialShow;
        /// <summary>
        /// 插屏->点击
        /// </summary>
        public static event Action OnIntersitialClick;
        /// <summary>
        /// 插屏->关闭
        /// </summary>
        public static event Action OnIntersitialClose;
        /// <summary>
        /// 插屏->播放Video失败
        /// </summary>
        public static event Action OnIntersitialPlayVideoFail;
        /// <summary>
        /// 插屏->播放Video开始
        /// </summary>
        public static event Action OnIntersitialPlayVideoStart;
        /// <summary>
        /// 插屏->播放Video结束
        /// </summary>
        public static event Action OnIntersitialPlayVideoEnd;



        /// <summary>
        /// 激励视频->加载失败
        /// </summary>
        public static event Action OnRewardVedioLoadFail;
        ///// <summary>
        ///// 激励视频->展示
        ///// 参数 bool: 是否展示成功
        ///// </summary>
        //public static event Action<bool> OnRewardVedioShow;
        /// <summary>
        /// 激励视频->点击
        /// </summary>
        public static event Action OnRewardVedioClick;
        /// <summary>
        /// 激励视频->关闭
        /// </summary>
        public static event Action<bool> OnRewardVedioClose;
        /// <summary>
        /// 激励视频->播放Video失败
        /// </summary>
        public static event Action OnRewardVedioPlayVideoFail;
        /// <summary>
        /// 激励视频->播放Video开始
        /// </summary>
        public static event Action OnRewardVedioPlayVideoStart;
        /// <summary>
        /// 激励视频->播放Video结束
        /// </summary>
        public static event Action OnRewardVedioPlayVideoEnd;
        ///// <summary>
        ///// 激励视频->获得奖励
        ///// </summary>
        //public static event Action OnRewardVedioSuccess;
    }
}