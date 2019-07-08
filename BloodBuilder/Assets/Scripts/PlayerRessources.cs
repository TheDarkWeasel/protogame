using System.Runtime.CompilerServices;

public class PlayerResources
{
    private int overallBlood = 0;
    private int selectedBlood = 0;

    public enum PlayerResource
    {
        OVERALL_BLOOD, SELECTED_BLOOD
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void IncreaseResource(int amount, PlayerResource type)
    {
        switch(type)
        {
            case PlayerResource.OVERALL_BLOOD:
                overallBlood += amount;
                break;
            case PlayerResource.SELECTED_BLOOD:
                selectedBlood += amount;
                break;
            default:
                throw new NotYetImplementedException("Resource type not implemeted");
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DecreaseResource(int amount, PlayerResource type)
    {
        switch (type)
        {
            case PlayerResource.OVERALL_BLOOD:
                overallBlood -= amount;
                break;
            case PlayerResource.SELECTED_BLOOD:
                selectedBlood -= amount;
                break;
            default:
                throw new NotYetImplementedException("Resource type not implemeted");
        }
    }
}
