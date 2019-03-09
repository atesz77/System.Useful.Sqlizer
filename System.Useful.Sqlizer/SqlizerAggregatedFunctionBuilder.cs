using System;
using System.Collections.Generic;
using System.Text;

namespace System.Useful.Sqlizer
{
    public class SqlizerAggregatedFunctionBuilder
    {
        private string functionName;
        private string column;
        
        private string name;

        private bool over;
        private string partitionBy;
        private string orderBy;
        private string rows;

        private string overClause 
        {
            get
            {
                if (!over) return string.Empty;

                StringBuilder sb = new StringBuilder();
                sb.Append(" OVER (");
                sb.Append(Environment.NewLine);

                if (!string.IsNullOrEmpty(partitionBy)) sb.Append(partitionBy);
                if (!string.IsNullOrEmpty(orderBy)) 
                {
                    sb.Append(orderBy);
                    sb.Append(Environment.NewLine);
                }
                else
                {
                    sb.Append(Environment.NewLine);
                }
                if (!string.IsNullOrEmpty(rows)) 
                {
                    sb.Append(rows);
                    sb.Append(Environment.NewLine);
                }

                sb.Append(")");
                sb.Append(Environment.NewLine);
                return sb.ToString();
            }
        }
        protected virtual string toString => $"{this.functionName}({column}){this.overClause}{(string.IsNullOrWhiteSpace(this.name) ? "" : $" AS '{this.name}'")}";

        internal SqlizerAggregatedFunctionBuilder(string functionName, string column)
        {
            this.functionName = functionName;
            this.column = column;
        }

        public SqlizerAggregatedFunctionBuilder AS(string name)
        {
            this.name = name;
            return this;
        }

        public SqlizerAggregatedFunctionBuilder OVER(string partitionBy = null, string orderBy = null, string rows = null)
        {
            over = true;

            this.partitionBy = partitionBy;
            this.orderBy = orderBy;
            this.rows = rows;

            return this;
        }

        public static implicit operator string(SqlizerAggregatedFunctionBuilder aggregated)
        {
            return aggregated.ToString();
        }

        public override string ToString()
        {
            return toString;
        }
    }
}