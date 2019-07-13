using UnityEngine;
using System.Collections;

public abstract class UnitCommand
{
    private Vector3 positionForFinishedUnit = new Vector3();
    private Vector3 assemblyPoint = new Vector3();

    public delegate void OnDone();
    public OnDone onDoneListener;


    public UnitCommand(Vector3 positionForFinishedUnit, Vector3 assemblyPoint)
    {
        this.positionForFinishedUnit = positionForFinishedUnit;
        this.assemblyPoint = assemblyPoint;
    }

    protected abstract IEnumerator CommandFunction();

    public IEnumerator Execute()
    {
        return CommandFunction();
    }

    protected Vector3 getPositionForFinishedUnit()
    {
        return positionForFinishedUnit;
    }

    protected Vector3 getAssemblyPoint()
    {
        return assemblyPoint;
    }

    /**
     * Call when command function has finished
     **/
    protected void finish()
    {
        if (onDoneListener != null)
        {
            onDoneListener.Invoke();
        }
    }
}
