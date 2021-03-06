﻿using System.Collections.Generic;

using FoxKit.Modules.DataSet.Fox;
using FoxKit.Modules.DataSet.Fox.FoxCore;

public class TargetFileAsset : EntityFileAsset
{
    /// <inheritdoc />
    public override string Extension => "tgt";

    protected override IEnumerable<Data> MakeInitialEntities()
    {
        var entity = new GeoxTargetDesc { Name = "GeoxTargetDesc0000" };
        return new[] { entity };
    }
}
