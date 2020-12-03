using System;

namespace JCiOSSDK
{
    //--------------------------------------------------
    //----------This file is called and used by IOS Listener----------
    //--------------------------------------------------

    public partial class IOSListener
    {
        /// <summary>
        /// splash->load file
        /// </summary>
        public static event Action OnSplashLoadFail;
        /// <summary>
        /// splash->show
        /// </summary>
        public static event Action OnSplashShow;
        /// <summary>
        /// splash->click
        /// </summary>
        public static event Action OnSplashClick;
        /// <summary>
        /// splash->close
        /// </summary>
        public static event Action OnSplashClose;



        /// <summary>
        /// Banner->load file
        /// </summary>
        public static event Action<bool> OnBannerLoaded;
        /// <summary>
        /// Banner->show
        /// </summary>
        public static event Action OnBannerShow;
        /// <summary>
        /// Banner->click
        /// </summary>
        public static event Action OnBannerClick;
        /// <summary>
        /// Banner->Click close
        /// </summary>
        public static event Action OnBannerTapClose;
        /// <summary>
        /// Banner->Auto Refresh
        ///  bool: Whether the refresh is successful
        /// </summary>
        public static event Action<bool> OnBannerAutoRefresh;



        /// <summary>
        /// Intersitial->load fail
        /// </summary>
        public static event Action OnIntersitialLoadFail;
        /// <summary>
        /// Intersitial->show
        /// parameter bool: is show sucess or not
        /// </summary>
        public static event Action<bool> OnIntersitialShow;
        /// <summary>
        /// Intersitial->click
        /// </summary>
        public static event Action OnIntersitialClick;
        /// <summary>
        /// Intersitial->close
        /// </summary>
        public static event Action OnIntersitialClose;
        /// <summary>
        /// Intersitial->PlayVideo Fail
        /// </summary>
        public static event Action OnIntersitialPlayVideoFail;
        /// <summary>
        /// Intersitial->PlayVideo Start
        /// </summary>
        public static event Action OnIntersitialPlayVideoStart;
        /// <summary>
        /// Intersitial->PlayVideo End
        /// </summary>
        public static event Action OnIntersitialPlayVideoEnd;



        /// <summary>
        /// rewardVideo->load fail
        /// </summary>
        public static event Action OnRewardVedioLoadFail;
        ///// <summary>
        ///// rewardVideo->show
        ///// parameter bool: is show sucess or not
        ///// </summary>
        //public static event Action<bool> OnRewardVedioShow;
        /// <summary>
        /// rewardVideo->click
        /// </summary>
        public static event Action OnRewardVedioClick;
        /// <summary>
        /// rewardVideo->close ,and you can send rewards here
        /// </summary>
        public static event Action<bool> OnRewardVedioClose;
        /// <summary>
        /// rewardVideo->playVideo fail
        /// </summary>
        public static event Action OnRewardVedioPlayVideoFail;
        /// <summary>
        /// rewardVideo->playVideo start
        /// </summary>
        public static event Action OnRewardVedioPlayVideoStart;
        /// <summary>
        /// rewardVideo->playVideo end
        /// </summary>
        public static event Action OnRewardVedioPlayVideoEnd;
        
    }
}