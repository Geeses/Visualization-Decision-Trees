using ReadWriteCsv;
using System.Text.RegularExpressions;
using System;
using System.Data;

[Serializable]
public class NumericalSplit : ISplitrule
{

    public bool Execute(DataRow row, string attribute, string split)
    {
        bool decider = false;
        string removedBooleanInSplit = Regex.Replace(split, "[<>!=]", "");
        switch (Regex.Replace(split, "[^<>!=]", ""))
        {
            case "<":
                if (float.Parse(row[attribute].ToString()) < float.Parse(removedBooleanInSplit))
                {
                    decider = true;
                }
                break;
            case ">":
                if (float.Parse(row[attribute].ToString()) > float.Parse(removedBooleanInSplit))
                {
                    decider = true;
                }
                break;
            case "<=":
                if (float.Parse(row[attribute].ToString()) <= float.Parse(removedBooleanInSplit))
                {
                    decider = true;
                }
                break;
            case ">=":
                if (float.Parse(row[attribute].ToString()) >= float.Parse(removedBooleanInSplit))
                {
                    decider = true;
                }
                break;
            case "==":
                if (float.Parse(row[attribute].ToString()) == float.Parse(removedBooleanInSplit))
                {
                    decider = true;
                }
                break;
            case "!=":
                if (float.Parse(row[attribute].ToString()) != float.Parse(removedBooleanInSplit))
                {
                    decider = true;
                }
                break;

        }

        return decider;
    }

    //float GetAttributeValueFromRow(DataRow row, string attribute)
    //{
    //    float value = 0;
    //    for (int i = 0; i < row.ItemArray.Length; i ++)
    //    {
    //        if (row[i].Equals(attribute))
    //        {

    //            //value = float.Parse(row[i + 1]);
    //        }
    //    }

    //    return value;
    //}

}
