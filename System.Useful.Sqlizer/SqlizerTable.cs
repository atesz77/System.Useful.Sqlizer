using System;
using System.Collections.Generic;
using System.Text;

namespace System.Useful.Sqlizer
{
    public class SqlizerTable
    {
        internal string TableName => this.GetType().Name;
        internal string As { get; set; }

        internal string AsValue => (string.IsNullOrWhiteSpace(As) ? this.TableName.ToLower() : As);

        protected virtual string toString => $"{TableName} AS {AsValue}";

        public SqlizerTable AS(string name)
        {
            this.As = name;
            return this;
        }

        public string ALL()
        {
            return $"{AsValue}.*";
        }

        public static implicit operator string(SqlizerTable table)
        {
            return table.ToString();
        }

        public override string ToString()
        {
            return toString;
        }
    }

    public class SqlizerTable<Tclass> : SqlizerTable where Tclass : SqlizerTable
    {
        public new Tclass AS(string name)
        {
            this.As = name;
            return this as Tclass;
        }
    }

    public class SqlizerTable<Tclass, Tnamespace> : SqlizerTable<Tclass> where Tclass : SqlizerTable
    {
        internal string Namespace => typeof(Tnamespace).Name;

        protected override string toString => $"{Namespace}.{TableName} AS {AsValue}";
    }
}
