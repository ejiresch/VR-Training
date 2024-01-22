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
    public GameObject splash;

    public Color selectedColor = new Color(0.5f, 1, 0.5f);
    public Color highlightedColor = new Color(0.3f, 1, 0.3f);
    public Color gone = new Color(0.2f, 1, 0.1f);

    // public SceneLoader scene;
    SceneLoader scene;

    public void StartVideo(int index)
    {
        //scene = GetComponent<SceneLoader>();

        splash.SetActive(false);
        // start video with index
        vp.clip = clips[index-1];
        vp.Play();
    }
    public void StopVideo()
    {
        // stop video
        vp.Pause();
    }
    public void ResumeVideo()
    {
        // resume video
        vp.Play();
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ChangeButtonColor(GameObject button) // Ändert die Farbe des Buttons auf Grün
    {
        var colors = button.gameObject.GetComponent<Button>().colors;
        colors.normalColor = gone; ;
        colors.pressedColor = gone;
        colors.selectedColor = gone;
        colors.highlightedColor = highlightedColor;
        button.gameObject.GetComponent<Button>().colors = colors;
        //Debug.Log(button.transform.tag);
        //setActive(false) works here
        //button.SetActive(false); 
        //GameObject.FindGameObjectsWithTag("Video_8_hinein");



    }
}
