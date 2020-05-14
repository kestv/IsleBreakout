using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTestsNpc
    {
        GameObject player;
        PlayerCombatController cc;
        GameObject npc;
        ConversationHandler ch;
        int npcId;
        NPC _npc;
        GameObject target;
        [UnityTest]
        public IEnumerator PlayerNpcTest1()
        {
            player = GameObject.Find("PlayerInstance");
            ch = GameObject.Find("EventHandler").GetComponent<ConversationHandler>();
            npc = GameObject.Instantiate(Resources.Load("Npc/Billy") as GameObject);
            npc.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            _npc = npc.GetComponent<NPC>();
            npcId = _npc.GetId();
            ch.StartConversation(npcId, _npc.GetConversations(), _npc.GetName(), _npc.quests);
            yield return new WaitForSeconds(1f);
            ch.Iterate();
            yield return new WaitForSeconds(1f);
            ch.AcceptQuest();
            npc.GetComponent<TalkQuest>().DisplayEvaluation();
            yield return new WaitForSeconds(0.5f);
            Assert.IsTrue(npc.GetComponent<TalkQuest>().IsActive());
        }

        [UnityTest]
        public IEnumerator PlayerNpcTest2()
        {
            var startLvl = player.GetComponent<PlayerLevelController>().GetLevel();
            NpcMessagesHandler.Instance.onTalkedToNpc(npcId, _npc.GetConversations(), _npc.GetName(), _npc.quests);
            NpcMessagesHandler.Instance._onTalkedToNpc();
            yield return new WaitForSeconds(1f);
            ch.Iterate();
            yield return new WaitForSeconds(1f);
            Assert.AreNotEqual(startLvl, player.GetComponent<PlayerLevelController>().GetLevel());
        }

        [UnityTest]
        public IEnumerator PlayerNpcTest3()
        {
            yield return new WaitForSeconds(0.5f);
            Assert.IsTrue(npc.GetComponent<TalkQuest>().IsCompleted());
        }

        [UnityTest]
        public IEnumerator PlayerNpcTest4()
        {
            ch.StartConversation(npcId, _npc.GetConversations(), _npc.GetName(), _npc.quests);
            yield return new WaitForSeconds(1f);
            ch.AcceptQuest();
            Assert.IsTrue(npc.GetComponent<KillQuest>().IsActive());
        }

        [UnityTest]
        public IEnumerator PlayerNpcTest5()
        {
            target = GameObject.Instantiate(Resources.Load("Enemies/Enemy") as GameObject);
            target.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            yield return new WaitForSeconds(1f);
            cc = player.GetComponent<PlayerCombatController>();
            cc.FindTarget();
            cc.AttackFromRange();
            cc.AttackFromRange();
            yield return new WaitForSeconds(1f);
            NpcMessagesHandler.Instance.onTalkedToNpc(npcId, _npc.GetConversations(), _npc.GetName(), _npc.quests);
            NpcMessagesHandler.Instance._onTalkedToNpc();
            yield return new WaitForSeconds(1f);
            ch.Iterate();
            yield return new WaitForSeconds(1f);
            Assert.IsTrue(npc.GetComponent<KillQuest>().IsCompleted());
        }
    }
}
