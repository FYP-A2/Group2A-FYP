using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnGame : MonoBehaviour
{
   //Queue<Player> playersQueue = new Queue<Player>();
   //Queue<Vector3> playersQueueDeathPos = new Queue<Vector3>();
   //Player playerCurrent;
   //Vector3 DeathPosCurrent;
   //public Transform dollHead, dollHandLeft, dollHandRight;
   //public Transform playerHead, playerHandLeft, playerHandRight;
   //public List<GameObject> shapeBoardPrefabs;
   //List<GameObject> spawnedBoard;
   //
   //bool respawnGameActive = false;
   //int roundSpawned = 0;
   //int roundCurrent = 0;
   //int roundMax = 3;
   //float spawnTimeSpacing = 2.5f;
   //
   //public Transform boardSpawnPoint;
   //public Vector3 boardPushVelocity;
   //public Transform playerView;
   //
   //// Start is called before the first frame update
   //void Start()
   //{
   //
   //}
   //
   //// Update is called once per frame
   //void Update()
   //{
   //   if (respawnGameActive)
   //   {
   //      //game logic
   //   }
   //   else if (playersQueue.Count > 0)
   //   {
   //      NextPlayer();
   //   }
   //}
   //
   //void JoinGame(Player player,Vector3 deathPos)
   //{
   //   playersQueue.Enqueue(player);
   //   playersQueueDeathPos.Enqueue(deathPos);
   //}
   //
   //void NextPlayer()
   //{
   //   playerCurrent = playersQueue.Dequeue();
   //   DeathPosCurrent = playersQueueDeathPos.Dequeue();
   //
   //   //get head and hand Transform;
   //   //...
   //
   //   respawnGameActive = true;
   //}
   //
   //void CreateBoard()
   //{
   //
   //}
   //
   //void PushBoard()
   //{
   //   foreach (GameObject b in spawnedBoard)
   //   {
   //      b.transform.position += boardPushVelocity;
   //   }
   //}
   //
   //void ClearAllBoard()
   //{
   //   for (int i = 0;i < spawnedBoard.Count;i++)
   //   {
   //      Destroy(spawnedBoard[i]);
   //   }
   //
   //   spawnedBoard.Clear();
   //}
   //
   //
   //
   //
   //void Win()
   //{
   //   //send back
   //}
   //
   //void Lose()
   //{
   //   //join queue to retry
   //}
}
