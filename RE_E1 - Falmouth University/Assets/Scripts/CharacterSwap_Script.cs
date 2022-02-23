﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterSwap_Script : MonoBehaviour
{
    // Character Switching
    public Transform character;
    public List<Transform> possibleCharacters;
    public int whichCharacter;
    private int wc;
    public CinemachineVirtualCamera vCam;

    // Start is called before the first frame update
    void Start()
    {
        if (character == null && possibleCharacters.Count >= 1)
        {
            character = possibleCharacters[0];
        }
        SwapCharacters();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            wc = whichCharacter;
            if (whichCharacter == 0)
            {
                whichCharacter = possibleCharacters.Count - 1;
            }
            else
            {
                whichCharacter -= 1;
            }
            if (Vector2.Distance(possibleCharacters[whichCharacter].position, character.position) > 20)
            {
                Debug.Log("too far");
                whichCharacter = wc;
                return;
            }
            SwapCharacters();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            wc = whichCharacter;
            if (whichCharacter == possibleCharacters.Count - 1)
            {
                whichCharacter = 0;
            }
            else
            {
                whichCharacter += 1;
            }
            if (Vector2.Distance(possibleCharacters[whichCharacter].position, character.position) > 20)
            {
                Debug.Log("too far");
                whichCharacter = wc;
                return;
            }
            SwapCharacters();
        }
    }

    public void SwapCharacters()
    {
        Debug.Log("close enough");
        character = possibleCharacters[whichCharacter];

        if (character.tag == "Player")
        {
            character.GetComponent<PlayerController_Script>().enabled = true; // not going to work - use in other script
        }
        else if (character.tag == "Crow")
        {
            character.GetComponent<CrowController_Script>().isFollowing = false;// not going to work - use in other script

        }



        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            if (possibleCharacters[i] != character)
            {
                if (possibleCharacters[i].tag == "Player")
                {
                    possibleCharacters[i].GetComponent<PlayerController_Script>().enabled = false;
                }
                else if (possibleCharacters[i].tag == "Crow")
                {
                    possibleCharacters[i].GetComponent<CrowController_Script>().isFollowing = true;
                } 
            }
        }

        vCam.LookAt = character;
        vCam.Follow = character;
    }
}
