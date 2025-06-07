using System;
using System.Collections;
using System.Collections.Generic;
using GameFlow;
using UnityEngine;

public class GameTutorial : MonoBehaviour
{
    public Tutorail tutorial;

    public List<GameObject> tutorialsObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartTutorial()
    {
        if(tutorial.PlayTutorial)
            StartCoroutine(Tutorials());
    }

    public IEnumerator Tutorials()
    {
        Time.timeScale = 0;

        // Step 1
        tutorialsObjects[0].SetActive(true);
        yield return new WaitUntil(() => tutorial.TutorialOneDone);
        tutorialsObjects[0].SetActive(false);

        // Step 2
        tutorialsObjects[1].SetActive(true);
        yield return new WaitUntil(() => tutorial.TutorialTwoDone);
        tutorialsObjects[1].SetActive(false);

        // Step 3
        tutorialsObjects[2].SetActive(true);
        yield return new WaitUntil(() => tutorial.TutorialThreeDone);
        tutorialsObjects[2].SetActive(false);

        // Step 4
        tutorialsObjects[3].SetActive(true);
        yield return new WaitUntil(() => tutorial.TutorialFourDone);
        tutorialsObjects[3].SetActive(false);

 

        // Resume the game
        Time.timeScale = 1;
            

            
    }
}
