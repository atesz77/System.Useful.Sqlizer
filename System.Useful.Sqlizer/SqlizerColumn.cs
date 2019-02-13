using System;
using System.Collections.Generic;
using System.Text;

namespace System.Useful.Sqlizer
{
    public class SqlizerColumn<T>
    {
        internal SqlizerTable Table { get; set; }
        internal string ColumnName { get; set; }
        internal string As { get; set; }


        public SqlizerColumn(SqlizerTable table, string columnName)
        {
            this.Table = table;
            this.ColumnName = columnName;
        }

        protected virtual string toString => $"{Table.AsValue}.{ColumnName}{(string.IsNullOrWhiteSpace(As) ? "" : $" AS '{As}'")}";

        public SqlizerColumn<T> AS(string name)
        {
            this.As = name;
            return this;
        }

        public string DESC()
        {
            return $"{Table.AsValue}.{ColumnName} DESC";
        }

        public static implicit operator string(SqlizerColumn<T> column)
        {
            return column.ToString();
        }

        public override string ToString()
        {
            return toString;
        }
    }
}
