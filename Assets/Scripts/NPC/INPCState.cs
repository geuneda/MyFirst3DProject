using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

public interface INPCState
{
    void EnterState(NPCController npc);
    void UpdateState(NPCController npc);
    void ExitState(NPCController npc);
}

public class IdleState : INPCState
{
    public void EnterState(NPCController npc)
    {
        npc.animator.SetFloat("Speed", 0f);
        npc.StartMoveToTarget();
    }

    public void UpdateState(NPCController npc)
    {
        npc.SwitchState(new MoveState());
    }

    public void ExitState(NPCController npc) { }
}

public class MoveState : INPCState
{
    public void EnterState(NPCController npc)
    {
        npc.animator.SetFloat("Speed", 1f);
    }

    public void UpdateState(NPCController npc)
    {
        if (npc.AtTarget())
        {
            npc.SetNextWaypoint();
            npc.SwitchState(new TalkState());
        }
    }

    public void ExitState(NPCController npc) { }
}

public class TalkState : INPCState
{
    private Coroutine talkCoroutine;

    public void EnterState(NPCController npc)
    {
        npc.animator.SetFloat("Speed", 0f);
        npc.animator.SetBool("IsTalk", true);
        talkCoroutine = npc.StartCoroutine(TalkRoutine(npc));
    }

    public void UpdateState(NPCController npc)
    {

    }

    public void ExitState(NPCController npc)
    {
        npc.animator.SetBool("IsTalk", false);
        if (talkCoroutine != null)
        {
            npc.StopCoroutine(talkCoroutine);
            talkCoroutine = null;
        }
    }

    private IEnumerator TalkRoutine(NPCController npc)
    {
        yield return new WaitForSeconds(3f);
        npc.SwitchState(new IdleState());
    }
}
