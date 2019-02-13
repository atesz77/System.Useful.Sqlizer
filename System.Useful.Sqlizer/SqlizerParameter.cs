using System;
using System.Collections.Generic;
using System.Text;

namespace System.Useful.Sqlizer
{
    public class SqlizerParameter
    {
        internal string ParameterName { get; set; }

        public SqlizerParameter(string paramName)
        {
            ParameterName = paramName;
        }

        protected virtual string toString => $"@{ParameterName}";


        public static implicit operator string(SqlizerParameter table)
        {
            return table.ToString();
        }

        public override string ToString()
        {
            return toString;
        }
    }
}
