using System.Collections.Generic;

public interface IBuildChoiceChangeListener
{
    void OnBuildChoicesChanged(List<BuildChoice> buildChoices);
}
