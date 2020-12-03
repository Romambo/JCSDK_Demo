namespace JCiOSSDK.Editor
{
    /// <summary>
    /// Unity代码修改
    /// </summary>
    public class UnityAppControllerModifier
    {
        /// <summary>
        /// 需要添加的头
        /// </summary>
        static string newHaed1= @"#include <JCSDK/JCSDK.h>";
        static string newHaed2= @"#import <AppTrackingTransparency/AppTrackingTransparency.h>";

        /// <summary>
        /// Unity原来的启动代码
        /// </summary>
        static string orgCode = @"[self performSelector: @selector(startUnity:) withObject: application afterDelay: 0];";

        /// <summary>
        /// 更换Unity原来的启动代码
        /// </summary>
        static string repaceCode = @"[self performSelector: @selector(initSDKWithApplication:) withObject: application afterDelay: 0];";

        /// <summary>
        /// 添加的新启动方法
        /// </summary>
        static string newCode = @"-(void)initSDKWithApplication:(UIApplication*)application{
	if (@available(iOS 14, *)) {
        //iOS 14
        [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
           // 初始化/init JCSDK
            [[JC_unityAdApi getInstance]initJCSDKWithUnityShow:^(BOOL showUnityTime) {
                [self performSelector: @selector(startUnity:) withObject: application afterDelay: 0];
            }];
            
        }];
    } else {
        // 初始化/init JCSDK
        [[JC_unityAdApi getInstance]initJCSDKWithUnityShow:^(BOOL showUnityTime) {
            [self performSelector: @selector(startUnity:) withObject: application afterDelay: 0];
        }];
    }
}";

        public static void ModfiyCode(string prjPath)
        {
            var fullpath = System.IO.Path.Combine(prjPath, "Classes/UnityAppController.mm");
            var editor = new TextEditor(fullpath);

            editor.WriteBelow(@"#include <mach/mach_time.h>", newHaed1);
            editor.WriteBelow(@"#include <mach/mach_time.h>", newHaed2);
            editor.Replace(orgCode, repaceCode);
            editor.WriteBelow("- (void)preStartUnity               {}", newCode);
        }
    }
}