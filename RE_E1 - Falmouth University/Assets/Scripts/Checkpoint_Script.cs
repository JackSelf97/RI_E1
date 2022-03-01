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
        Spikes,
        Lever_1,
        Lever_2,
    }

    public Landmarks point;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (point == Landmarks.Checkpoint)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.gMan.currentCheckpoint = gameObject;
                gameObject.transform.GetComponentInChildren<TextMesh>().color = Color.green;
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
        else if (point == Landmarks.Spikes)
        {
            if (collision.gameObject.tag == "Player")
            {
                // Show Spikes
                GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(DelayDeath());
            }
        }
        else if (point == Landmarks.Lever_1)
        {
            if (collision.gameObject.tag == "Crow")
            {
                // Disable barrier
                GameManager.gMan.barriers[0].SetActive(false);
                gameObject.SetActive(false);
            }
        }
        else if (point == Landmarks.Lever_2)
        {
            if (collision.gameObject.tag == "Crow")
            {
                // Disable barrier
                GameManager.gMan.barriers[1].SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(0.2f);
        
        // Kill player
        GameManager.gMan.timesDiedNo++;
        GameManager.gMan.RespawnAtLastCheckpoint();
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
