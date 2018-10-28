using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// A project is a thing you can do. Like crafting something!
/// </summary>
public abstract class Project
{
    public virtual float Percent { get; private set; }
    public virtual float PhasePercent { get; private set; }

    public abstract void StartProject();


    public Project()
    {
        Percent = 0;
    }
}
