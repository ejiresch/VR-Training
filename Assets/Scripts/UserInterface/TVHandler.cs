using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

//inspired by http://codesaying.com/playing-video-in-unity/

public class TVHandler : MonoBehaviour
{
    public VideoPlayer vp;
    public VideoClip clip1;
    public VideoClip clip2;
    public VideoClip clip3;
    public VideoClip clip4;
    public VideoClip clip5;

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
        if (index == 4)
        {
            print("4 (play video4)");
            vp.clip = clip4;
            vp.Play();
        }
        if (index == 5)
        {
            print("5 (play video5)");
            vp.clip = clip5;
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
    public void reset_Scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
