using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadWriteCsv;
using System.Text.RegularExpressions;
using System;

[Serializable]
public class NumericalSplit : ISplitrule
{

    public bool Execute(CsvRow row, string attribute, string split)
    {
        bool decider = false;
        string removedBooleanInSplit = Regex.Replace(split, "[<>!=]", "");
        switch (Regex.Replace(split, "[^<>!=]", ""))
        {
            case "<":
                if (GetAttributeValueFromRow(row, attribute) < float.Parse(removedBooleanInSplit))
                {
                    decider = true;
                }
                break;
            case ">":
                if (GetAttributeValueFromRow(row, attribute) > float.Parse(removedBooleanInSplit))
                {
                    decider = true;
                }
                break;
            case "<=":
                if (GetAttributeValueFromRow(row, attribute) <= float.Parse(removedBooleanInSplit))
                {
                    decider = true;
                }
                break;
            case ">=":
                if (GetAttributeValueFromRow(row, attribute) >= float.Parse(removedBooleanInSplit))
                {
                    decider = true;
                }
                break;
            case "==":
                if (GetAttributeValueFromRow(row, attribute) == float.Parse(removedBooleanInSplit))
                {
                    decider = true;
                }
                break;
            case "!=":
                if (GetAttributeValueFromRow(row, attribute) != float.Parse(removedBooleanInSplit))
                {
                    decider = true;
                }
                break;

        }

        return decider;
    }

    float GetAttributeValueFromRow(CsvRow row, string attribute)
    {
        float value = 0;
        for (int i = 0; i < row.Count; i += 2)
        {
            if (row[i].Equals(attribute))
            {
                value = float.Parse(row[i + 1]);
            }
        }

        return value;
    }

}
