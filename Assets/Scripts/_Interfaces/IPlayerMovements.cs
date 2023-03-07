using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMovements{
   void Move(Vector2 rightness_n_forwardness);
   void Jump();
}
