using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialCanvasBackgroundHeightCalculator : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float lineHeight;

    private void Start()
    {
        updateHeight();
    }

    public void updateHeight()
    {
        // Get the number of lines in the text
        int lineCount = textMeshPro.textInfo.lineCount;

        // Calculate the total height
        float totalHeight = 1 + (lineCount * lineHeight);

        this.transform.localScale = new Vector3(this.transform.localScale.x, totalHeight, this.transform.localScale.z);
    }


}
