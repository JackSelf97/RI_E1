using UnityEngine;

public class DeathZone_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Kill player
            GameManager.gMan.timesDiedNo++;
            GameManager.gMan.RespawnAtLastCheckpoint();
        }
        if (collision.gameObject.tag == "Crow")
        {
            // Kill Crow
            GameManager.gMan.crowsLostNo++;
            collision.GetComponent<CrowController_Script>().timeLimit = 0;
        }

    }
}
