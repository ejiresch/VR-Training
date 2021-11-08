using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//inspired by http://codesaying.com/playing-video-in-unity/

public class TVHandler : MonoBehaviour
{
    public VideoPlayer vp;
    public VideoClip clip1;
    public VideoClip clip2;
    public VideoClip clip3;

    public void startVideo(int index)
    {
        // start video with index
        if (index==1)
        {
            print("1 (play video1)");
            vp.clip = clip1;
            vp.Play();
        }
        if (index == 2)
        {
            print("2 (play video2)");
            vp.clip = clip2;
            vp.Play();
        }
        if (index == 3)
        {
            print("3 (play video3)");
            vp.clip = clip3;
            vp.Play();
        }
    }
    public void stopVideo()
    {
        // stop video
        vp.Pause();
    }
    public void resumeVideo()
    {
        // resume video
        vp.Play();
    }
}
