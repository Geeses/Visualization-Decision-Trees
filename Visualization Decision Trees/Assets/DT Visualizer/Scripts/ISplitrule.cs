using System.Collections.Generic;
using ReadWriteCsv;
using System;
using System.Data;

public interface ISplitrule
{
    bool Execute(DataRow row, string attribute, string split);
}
