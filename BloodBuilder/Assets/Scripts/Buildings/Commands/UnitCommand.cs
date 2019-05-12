using UnityEngine;
using System.Collections;

public abstract class UnitCommand
{
    private OnDone onDoneListener= null;

    protected abstract IEnumerator CommandFunction();

    public IEnumerator Execute()
    {
        return CommandFunction();
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
