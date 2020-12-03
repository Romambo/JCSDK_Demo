using System;
using System.Threading;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JCiOSSDK
{
    public class JCiOSSDKSynchronizationContext : MonoBehaviour
    {
        protected static JCiOSSDKSynchronizationContext instance = null;

        public static JCiOSSDKSynchronizationContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Object.FindObjectOfType<JCiOSSDKSynchronizationContext>();
                }

                if (instance == null)
                {
                    var obj = new GameObject(typeof(JCiOSSDKSynchronizationContext).Name);
                    DontDestroyOnLoad(obj);
                    instance = obj.AddComponent<JCiOSSDKSynchronizationContext>();
                }

                return instance;
            }
        }

        private void Awake()
        {
            SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);
        }

        private void Update()
        {
            OneThreadSynchronizationContext.Instance.Update();
        }


#if UNITY_EDITOR
        public void EditorFakeCallBack(Action action, float seconds = 0)
        {
            if (action == null) return;
            StartCoroutine(DealyCallBack(action, seconds));
        }

        IEnumerator DealyCallBack(Action action, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            action?.Invoke();
        }
#endif
    }
}


