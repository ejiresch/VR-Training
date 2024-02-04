using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvasBackground : MonoBehaviour
{
    public RectTransform TutorialCanvas;
    public TutorialCanvasBackgroundHeightCalculator tcbhc;
    private RectTransform transformSelf;

    // Start is called before the first frame update
    void Start()
    {
        transformSelf = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePos();
    }

    public void UpdatePos()
    {
        transformSelf.position = TutorialCanvas.position;
        transformSelf.rotation = TutorialCanvas.rotation;
        tcbhc.updateHeight();
    }
}
