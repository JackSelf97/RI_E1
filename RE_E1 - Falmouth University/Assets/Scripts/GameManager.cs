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

    public CinemachineVirtualCamera vCam;
    public PlayerController_Script bigCrowCont;

    public delegate void SendCrow();
    public SendCrow sending;


    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Q) && currentCharacter == null)
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
}
