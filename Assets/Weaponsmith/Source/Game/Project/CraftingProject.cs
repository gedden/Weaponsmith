using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class CraftingProject : Project
{
    public enum ECraftingPhase
    {
        UNSTARTED,

        ITEM_SELECT,
        GENERTE_BASE_STATS,

        GENERATE_AFFIX_COUNT,

        SELECT_AFFIX,
        GENERTE_AFFIX_STATS,
    }

    private ECraftingPhase _CurrentPhase = ECraftingPhase.UNSTARTED;
    public ECraftingPhase CurrentPhase
    {
        set
        {
            // set the phase
            _CurrentPhase = value;

            switch (_CurrentPhase)
            {
                case ECraftingPhase.ITEM_SELECT:
                    SelectItem();
                    break;
                case ECraftingPhase.GENERTE_BASE_STATS:
                    GenerateStats();
                    break;
                case ECraftingPhase.GENERATE_AFFIX_COUNT:
                    GenerateAffixCount();
                    break;

                case ECraftingPhase.SELECT_AFFIX:
                    SelectAffix();
                    break;
                case ECraftingPhase.GENERTE_AFFIX_STATS:
                    GenerateAffix();
                    break;
            }

        }
        get
        {
            return _CurrentPhase;
        }
    }

    /// <summary>
    /// Start a new project!
    /// </summary>
    public virtual void StartProject()
    {
        // start a new phase
        CurrentPhase = ECraftingPhase.ITEM_SELECT;
    }


    public abstract void SelectItem();
    public abstract void GenerateStats();
    public abstract void GenerateAffixCount();
    public abstract void SelectAffix();
    public abstract void GenerateAffix();
}
