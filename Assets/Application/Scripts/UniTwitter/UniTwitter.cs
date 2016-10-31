using UnityEngine;
using System.Runtime.InteropServices;

public class UniTwitter : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void uniTwitterShare(string text);

    public static void Share(string text){
		#if UNITY_IOS && !UNITY_EDITOR
        	uniTwitterShare (text);
		#elif UNITY_ANDROID || UNITY_EDITOR
        	Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(text));
		#endif
    }
}


