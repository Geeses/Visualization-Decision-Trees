﻿using System;
using System.Data;

[Serializable]
public class UndefinedSplit : ISplitrule
{
    public bool Execute(DataRow row, string attribute, string split)
    {
        throw new System.NotImplementedException();
    }

}