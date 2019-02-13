#r "nuget:NETStandard.Library, 2.0.3"

using System.IO;
using System.Linq;
using System.Collections.Generic;

Dictionary<string, (string nspace, string table, List<(string type, string name)> cols)> data = new Dictionary<string, (string nspace, string table, List<(string type, string name)> cols)>();
var model = File.ReadAllLines("ModelToGenerate.txt");

var lastTable = string.Empty;
foreach (var item in model)
{
    if (item.StartsWith("TABLE") || item.StartsWith("NAMESPACE"))
    {
        var table = item.Split(' ')[1];
        lastTable = table;
        if (table.Contains("."))
        {
            var s = table.Split('.');
            var t = s[1];
            var n = s[0];
            data.Add(lastTable, (n, t, new List<(string type, string name)>()));
        }
        else
        {
            data.Add(lastTable, (null, table, new List<(string type, string name)>()));
        }
    }
    else if (string.IsNullOrWhiteSpace(item))
    {
        lastTable = string.Empty;
        continue;
    }
    else
    {
        var s = item.Split(' ');
        data[lastTable].cols.Add((s[0], s[1]));
    }
}

var template = string.Empty;
template += $@"
//**************************************************//
//             THIS IS A GENERATED FILE             //
//**************************************************//

namespace System.Useful.Sqlizer.Sample
{{";

foreach (var item in data)
{
    template += $@"
    public class {item.Value.table} : SqlizerTable<{item.Value.table}{(string.IsNullOrWhiteSpace(item.Value.nspace) ? "" : $", {item.Value.nspace}")}>
    {{";

    foreach (var col in item.Value.cols)
    {
        template += $@"
        public SqlizerColumn<{col.type}> {col.name} => new SqlizerColumn<{col.type}>(this, nameof({col.name}));";
    }

    template += $@"
    }}
    ";
}

template += $@"
}}
";

TextWriter tw = new StreamWriter("GeneratedModel.cs");
tw.Write(template);
tw.Close();