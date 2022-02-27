using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Script : MonoBehaviour
{
    // Point Enums
    public enum Landmarks
    {
        Checkpoint,
        FinishLine,
    }

    public Landmarks point;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (point == Landmarks.Checkpoint)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.gMan.currentCheckpoint = gameObject;
            }
        }
        else if (point == Landmarks.FinishLine)
        {
            if (collision.gameObject.tag == "Player")
            {
                // You Win!
                GameManager.gMan.VictoryState();
            }
        }
    }
}
