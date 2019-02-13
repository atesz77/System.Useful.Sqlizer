using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Useful.Sqlizer.Builders
{
    public class SqlizerCaseWhenBuilder
    {
        private string As { get; set; }

        private string toString
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(Environment.NewLine);
                sb.Append("CASE");

                if (cases.Any())
                {
                    sb.Append(Environment.NewLine);
                    sb.Append($"{string.Join(Environment.NewLine, cases.Select(x => $"WHEN {x.Item1} THEN {x.Item2}"))}");
                }

                if (!string.IsNullOrWhiteSpace(elseCase))
                {
                    sb.Append(Environment.NewLine);
                    sb.Append($"ELSE {elseCase}");
                }

                sb.Append(Environment.NewLine);
                sb.Append("END");

                if (string.IsNullOrWhiteSpace(As))
                {
                    return sb.ToString();
                }
                else
                {
                    return $"{sb.ToString()} AS {As}";
                }
            }
        }

        public SqlizerCaseWhenBuilder WHEN(string condition, string then, string elseCase = null)
        {
            this.cases.Add((condition, then));
            this.elseCase = elseCase;

            return this;
        }

        private List<(string, string)> cases = new List<(string, string)>();
        private string elseCase = null;

        public SqlizerCaseWhenBuilder AS(string name)
        {
            this.As = name;
            return this;
        }

        public static implicit operator string(SqlizerCaseWhenBuilder queryBuilder)
        {
            return queryBuilder.ToString();
        }

        public override string ToString()
        {
            return toString;
        }

    }
}
