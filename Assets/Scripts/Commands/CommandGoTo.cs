using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class CommandGoTo : Command
{

    PartnerController partnerController;
    Vector3 newPosition;
    NavMeshAgent navMeshAgent;
    float remainingDistanceOffset = 0.03f;

    public CommandGoTo(PartnerController partnerController, Vector3 newPosition)
    {
        this.partnerController = partnerController;
        this.newPosition = newPosition;
        this.commandType = CommandType.GOTO;
    }


    public override IEnumerator ActivateCommand()
    {
        
        partnerController.followTarget = false;
        navMeshAgent = partnerController.GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(newPosition);
        
        yield return null; //We wait one frame


        yield return new WaitUntil(() => navMeshAgent.remainingDistance < remainingDistanceOffset);
        isCommandCompleted = true;
        Debug.Log("Command completed");
    }

    public override IEnumerator UpdateCommandCallback()
    {
        yield return new WaitUntil(()=> navMeshAgent.remainingDistance < remainingDistanceOffset);
        isCommandCompleted = true;
        Debug.Log("Command completed");
        //partnerController.followTarget = true;
    }
}
