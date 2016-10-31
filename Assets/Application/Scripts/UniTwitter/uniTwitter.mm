#import <Social/Social.h>

extern "C"{
    void uniTwitterShare(const char *text);
}

void uniTwitterShare(const char *text){
    NSString * textStr = [NSString stringWithUTF8String:text ? text : ""];
    
    SLComposeViewController *vc = [SLComposeViewController
                                   composeViewControllerForServiceType:SLServiceTypeTwitter];
    [vc setInitialText:textStr];
    [vc dismissViewControllerAnimated:YES completion:nil];
    [UnityGetGLViewController() presentViewController:vc animated:YES completion:nil];
}