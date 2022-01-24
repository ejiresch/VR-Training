using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

//inspired by http://codesaying.com/playing-video-in-unity/

public class TVHandler : MonoBehaviour
{
    public VideoPlayer vp;
    public VideoClip clip1heraus;
    public VideoClip clip2heraus;
    public VideoClip clip3heraus;
    public VideoClip clip4heraus;
    public VideoClip clip5heraus;
    public VideoClip clip6hinein;
    public VideoClip clip7hinein;
    public VideoClip clip8hinein;
    public VideoClip clip9hinein;
    public VideoClip clip10hinein;
    public VideoClip clip11hinein;
    public VideoClip clip12hinein;
    public VideoClip clip13hinein;
    public VideoClip clip14hinein;

    public void startVideo(int index)
    {
        // start video with index
        if (index==1)
        {
            print("1 (play video1)");
            vp.clip = clip1heraus;
            vp.Play();
        }
        if (index == 2)
        {
            print("2 (play video2)");
            vp.clip = clip2heraus;
            vp.Play();
        }
        if (index == 3)
        {
            print("3 (play video3)");
            vp.clip = clip3heraus;
            vp.Play();
        }
        if (index == 4)
        {
            print("4 (play video4)");
            vp.clip = clip4heraus;
            vp.Play();
        }
        if (index == 5)
        {
            print("5 (play video5)");
            vp.clip = clip5heraus;
            vp.Play();
        }
        if (index == 6)
        {
            print("6 (play video6)");
            vp.clip = clip6hinein;
            vp.Play();
        }
        if (index == 7)
        {
            print("7 (play video7)");
            vp.clip = clip7hinein;
            vp.Play();
        }
        if (index == 8)
        {
            print("8 (play video8)");
            vp.clip = clip8hinein;
            vp.Play();
        }
        if (index == 9)
        {
            print("9 (play video9)");
            vp.clip = clip9hinein;
            vp.Play();
        }
        if (index == 10)
        {
            print("10 (play video10)");
            vp.clip = clip10hinein;
            vp.Play();
        }
        if (index == 11)
        {
            print("11 (play video11)");
            vp.clip = clip11hinein;
            vp.Play();
        }
        if (index == 12)
        {
            print("12 (play video12)");
            vp.clip = clip12hinein;
            vp.Play();
        }
        if (index == 13)
        {
            print("13 (play video13)");
            vp.clip = clip13hinein;
            vp.Play();
        }
        if (index == 14)
        {
            print("14 (play video14)");
            vp.clip = clip14hinein;
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
