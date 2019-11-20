using System.Collections.Generic;
using ReadWriteCsv;
using System;

public interface ISplitrule
{
    bool Execute(CsvRow row, string attribute, string split);
}
