using UnityEngine;
using UnityEngine.AI;

public interface IMonster
{
   void TakeDamage(int phyDamage, int magicDamage);
   void GetBurnt(int phyDamage, int magicDamage, int burntDamage, float burntTime);
   void GetSlow(int phyDamage, int magicDamage, float slowRatio,float slowTime);
   void DefenseReduction(float value, float reductionTime);
}
