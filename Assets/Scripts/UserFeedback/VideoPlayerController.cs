using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//inspired by http://codesaying.com/playing-video-in-unity/

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer vp;
    public VideoClip clip1;
    public VideoClip clip2;
    public VideoClip clip3;
    // Start is called before the first frame update
    void Start()
    {
        vp = this.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //choose a clip by pressing a number
        if (Input.GetKeyDown("1"))
        {
            print("1 (play video1)");
            vp.clip = clip1;
            vp.Play();
        }
        if (Input.GetKeyDown("2"))
        {
            print("2 (play video2)");
            vp.clip = clip2;
            vp.Play();
        }
        if (Input.GetKeyDown("3"))
        {
            print("3 (play video3)");
            vp.clip = clip3;
            vp.Play();
        }

        //space is for play and pause
        if (Input.GetKeyDown("space"))
        {
            print("space (play/pause)");
            if (vp.isPlaying == true)
            {
                vp.Pause();
            }
            else
            {
                vp.Play();
            }
        }

    }
}
