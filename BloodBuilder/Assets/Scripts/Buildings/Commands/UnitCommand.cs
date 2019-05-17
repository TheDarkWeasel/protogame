using UnityEngine;
using System.Collections;

public abstract class UnitCommand
{
    private OnDone onDoneListener= null;
    private Vector3 positionForFinishedUnit = new Vector3();

    public UnitCommand(Vector3 positionForFinishedUnit)
    {
        this.positionForFinishedUnit = positionForFinishedUnit;
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

    /**
     * Call when command function has finished
     **/
    protected void finish()
    {
        if (onDoneListener != null)
        {
            onDoneListener.run();
        }
    }

    public void SetOnDoneListener(OnDone onDone)
    {
        onDoneListener = onDone;
    }
}
