using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//inspired by http://codesaying.com/playing-video-in-unity/

public class TVHandler : MonoBehaviour
{
    public VideoPlayer vp;
    public List<VideoClip> clips;

    public void startVideo(int index)
    {
        // start video with index
        vp.clip = clips[index-1];
        vp.Play();
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
    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void changeButtonColor(GameObject button)
    {
        var colors = button.gameObject.GetComponent<Button>().colors;
        colors.normalColor = new Color(0.5f, 1, 0.5f);
        colors.pressedColor = new Color(0.5f, 1, 0.5f);
        colors.selectedColor = new Color(0.5f, 1, 0.5f);
        colors.highlightedColor = new Color(0.3f, 1, 0.3f);
        button.gameObject.GetComponent<Button>().colors = colors;
    }
}
