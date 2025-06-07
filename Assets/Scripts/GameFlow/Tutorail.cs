using System;
using System.Collections;
using UnityEngine;

namespace GameFlow
{
    [CreateAssetMenu(fileName = "Tutorial", menuName = "tutorial", order = 0)]
    public class Tutorail : ScriptableObject
    {
        private void OnEnable()
        {
            PlayTutorial = true;
            TutorialOneDone = false;
            TutorialTwoDone = false;
            TutorialThreeDone = false;
            TutorialFourDone = false;
            TutorialFiveDone = false;
            TutorialSixDone = false;
            TutorialSevenDone = false;
            
        }

        public bool PlayTutorial;
        public bool TutorialOneDone;
        public bool TutorialTwoDone;
        public bool TutorialThreeDone;
        public bool TutorialFourDone;
        public bool TutorialFiveDone;
        public bool TutorialSixDone;
        public bool TutorialSevenDone;
    }
}