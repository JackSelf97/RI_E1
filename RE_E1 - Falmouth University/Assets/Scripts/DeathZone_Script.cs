using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            // Kill player
            GameManager.gMan.timesDiedNo++;
            GameManager.gMan.RespawnAtLastCheckpoint();
        }
    }
}
