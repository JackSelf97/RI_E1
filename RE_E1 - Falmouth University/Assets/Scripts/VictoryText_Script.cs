using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryText_Script : MonoBehaviour
{
    public Transform[] children;
    private float timer, timeLimit = 0.5f;
    public bool colourSwitch;
    public Text SMG;

    // Start is called before the first frame update
    void Start()
    {
        children = new Transform[transform.childCount];
        SMG.text = "Congratulations, you win!";
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeLimit)
        {
            SwitchColour();
            for (int i = 0; i < children.Length; i++)
            {
                children[i] = transform.GetChild(i);
                Text childText = children[i].GetComponent<Text>();

                if (colourSwitch)
                {
                    childText.color = Color.green;
                }
                else if (!colourSwitch)
                {
                    childText.color = Color.white;
                }
            }

            timer = 0;
        }
    }

    private void SwitchColour()
    {
        colourSwitch = !colourSwitch;
    }
}
