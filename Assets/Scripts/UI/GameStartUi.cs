using UI;
using UnityEngine;

public class GameStartUi : MonoBehaviour
{
   public SoUIEvents SoUIEvents;


   public void startGame()
   {
      SoUIEvents.RaiseGameStart();
   }
}
