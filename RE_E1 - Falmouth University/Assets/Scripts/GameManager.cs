using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    // Character Switching
    public GameObject currentCharacter;
    public Queue<GameObject> crowQueue = new Queue<GameObject>();
    public GameObject[] possibleCharacters = new GameObject[3];
    public Transform[] crowPos = new Transform[3];
    public GameObject[] crowSlots = new GameObject[3];
    public GameObject crowPrefab;

    public CinemachineVirtualCamera vCam, vCamMM;
    public PlayerController_Script bigCrowCont;

    public delegate void SendCrow();
    public SendCrow sending;

    public GameObject buttonPanel, startingBarrier;
    private bool inMainMenu;
    private const int one = 1;

    // Start is called before the first frame update
    void Start()
    {
        inMainMenu = true;

        for (int i = 0; i < possibleCharacters.Length; i++)
        {
            possibleCharacters[i] = Instantiate(crowPrefab);
            possibleCharacters[i].name = "Crow_" + i;
            var crowCont = possibleCharacters[i].GetComponent<CrowController_Script>();
            crowCont._gMan = this;
            crowCont.myPos = crowPos[i];
            crowCont.crowPosIndex = i;
            crowQueue.Enqueue(possibleCharacters[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currentCharacter == null && inMainMenu == false)
        {
            var crow = crowQueue.Dequeue();
            currentCharacter = crow;
            var crowCont = crow.GetComponent<CrowController_Script>();
            crowCont.isFollowing = false;
            bigCrowCont.enabled = false;
            sending.Invoke();
            CharacterSwap();
        }     
    }

    public void CharacterSwap()
    {
        VCamTargets(currentCharacter.transform);
    }

    public void EmptyCurrentCharacter()
    {
        currentCharacter = null;

        VCamTargets(bigCrowCont.transform);
        bigCrowCont.enabled = true;
    }

    private void VCamTargets(Transform currentChar)
    {
        vCam.LookAt = currentChar;
        vCam.Follow = currentChar;
    }

    public void PlayGame()
    {
        // Disable Buttons and UI
        buttonPanel.GetComponent<Animator>().SetBool("StartGame", true);
        StartCoroutine(DelayGameState());
    }

    IEnumerator DelayGameState()
    {
        yield return new WaitForSeconds(one);
        buttonPanel.SetActive(false);
        vCam.enabled = true;
        vCamMM.enabled = false;
        inMainMenu = false;
        startingBarrier.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }
}
