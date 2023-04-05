using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGame : MonoBehaviour
{
   public Transform headTransform, handLeftTransform, handRightTransform;
   public List<GameObject> shapeBoardPrefabs;
   Queue<GameObject> spawnedBoard;

   bool respawnGameActive = false;
   int roundSpawned = 0;
   int roundPassed = 0;
   int roundMax = 3;
   float spawnTimeSpacing = 2.5f;

    // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      if (respawnGameActive)
      {
         //gamelogic
      }
   }
}
