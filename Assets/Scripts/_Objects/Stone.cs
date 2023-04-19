using FYP2A.VR.Melee.Target;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public MeleeTargetStone meleeStone;

    public int HP = 5;
    public int NowHP { get => (int)meleeStone.nowHp; }


    [Header("Reward")]
    [SerializeField]
    public List<Reward> rewards;

    [Serializable]
    public struct Reward
    {
        public string resourceType;
        public int amount;
    }


    // Start is called before the first frame update
    private void Start()
    {
        if (meleeStone == null)
            TryGetComponent(out meleeStone);
        meleeStone.stone = this;
        meleeStone.hp = HP;
    }

    public void HewComplete(Player byWho = null)
    {
        RewardPlayers();
        Destroy(gameObject);

        if (byWho != null)
        {
            if (byWho.director.mode._TNT_State == Mode.TNT_State.Waiting_MineStone)
                byWho.director.TNTModeJumpState();
        }
    }

    public void RewardPlayers()
    {
        foreach (Reward reward in rewards)
            ResourceGroupType.Instance.Add(reward.resourceType, reward.amount);
    }
}
