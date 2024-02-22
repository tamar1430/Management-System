﻿
using System.Collections;

namespace PL;

/// <summary>
/// level collection
/// </summary>
internal class LevelsCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> s_enums =
            (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();

}

