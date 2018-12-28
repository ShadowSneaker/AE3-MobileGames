using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointInstruction
{
    // The entity does nothing when it reaches this node.
    None,

    // The entity jumps when it reaches this node.
    Jump,

    // Kills the entity on contact
    Die
}


public enum InstructionHandle
{
    // Only performs the instruction when moving forward.
    PerformOnForward,
    
    // Only performs the instruction when moving in reverse.
    PerformOnReverse,

    // Never performs the instruction.
    PerformNever,

    // Always performs the instruction.
    PerformAlways
}



public class AINode : MonoBehaviour
{
    // What action the entity should do when it reaches this node.
    public PointInstruction Instruction;

    // Determines when the entity should perform the instruction when it reaches this node.
    public InstructionHandle Handle;

    // How long the entity should wait at this node before continuing.
    // (The entity will perform the instruction after waiting).
    public float WaitDuration;

    
    // The reference to the PatrolScript.
    internal PatrolScript Owner;

    // The reference to the Entity that will be controlled.
    internal Entity ENT;

    // The number representing what order this node is in.
    internal int NodeNum;
    



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Owner.gameObject)
        {
            if (NodeNum == Owner.CurrentPoint)
            {
                Owner.Direction = 0.0f;
                if (WaitDuration > 0.0f)
                {
                    StartCoroutine(WaitDelay());
                }
                else
                {
                    PerformInstruction();
                }
            }
        }
    }


    private void PerformInstruction()
    {
        switch (Instruction)
        {
            case PointInstruction.None:
                Owner.GoToNextPoint();
                break;


            case PointInstruction.Jump:
                if (ShouldPerform())
                    ENT.Jump();
                Owner.GoToNextPoint();
                break;


            case PointInstruction.Die:
                if (ShouldPerform())
                    ENT.ApplyDamage(9999);
                Owner.GoToNextPoint();
                break;
                
        }
    }


    private IEnumerator WaitDelay()
    {
        yield return new WaitForSeconds(WaitDuration);
        PerformInstruction();
    }


    bool ShouldPerform()
    {
        switch (Handle)
        {
            case InstructionHandle.PerformOnForward:
                return !(Owner.Reverse);


            case InstructionHandle.PerformOnReverse:
                return (Owner.Reverse);


            case InstructionHandle.PerformNever:
                return false;


            case InstructionHandle.PerformAlways:
                return true;
        }
        return false;
    }
}
