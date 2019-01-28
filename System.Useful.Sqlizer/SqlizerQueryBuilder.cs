using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Useful.Sqlizer
{
    public sealed class SqlizerQueryBuilder
    {
        private string As { get; set; }

        private string toString
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (selectColumns.Any())
                {
                    sb.Append(Environment.NewLine);
                    sb.Append($"SELECT {string.Join(", ", selectColumns)}");
                }

                if (!string.IsNullOrWhiteSpace(updateTable))
                {
                    sb.Append(Environment.NewLine);
                    sb.Append($"UPDATE {updateTable}");
                }

                if (!string.IsNullOrWhiteSpace(insertTable))
                {
                    sb.Append(Environment.NewLine);
                    sb.Append($"INSERT INTO {updateTable}");
                }

                if (setColumns.Any())
                {
                    if (!string.IsNullOrWhiteSpace(updateTable))
                    {
                        sb.Append(Environment.NewLine);
                        sb.Append($"SET {string.Join(", ", setColumns.Select(x => $"{x.col} = {x.value}"))}");
                    }
                    else if (!string.IsNullOrWhiteSpace(insertTable))
                    {
                        sb.Append(Environment.NewLine);
                        var cols = new string[setColumns.Count];
                        var vals = new string[setColumns.Count];
                        var i = 0;
                        foreach (var item in setColumns)
                        {
                            cols[i] = item.col;
                            vals[i] = item.value;
                            i++;
                        }
                        sb.Append($"({string.Join(", ", cols)}) VALUES");
                        sb.Append(Environment.NewLine);
                        sb.Append($"({string.Join(", ", vals)})");
                    }
                }

                if (fromTables.Any())
                {
                    sb.Append(Environment.NewLine);
                    sb.Append($"FROM {string.Join(", ", fromTables)}");
                }

                if (joinTables.Any())
                {
                    sb.Append(Environment.NewLine);
                    sb.Append($"{string.Join(Environment.NewLine, joinTables.Select(x => $"{x.spec}JOIN {x.table} ON {x.condition}"))}");
                }

                if (!string.IsNullOrWhiteSpace(whereCondition))
                {
                    sb.Append(Environment.NewLine);
                    sb.Append($"WHERE {whereCondition}");
                }

                if (groupByColumns.Any())
                {
                    sb.Append(Environment.NewLine);
                    sb.Append($"GROUP BY {string.Join(", ", groupByColumns)}");
                }

                if (orderByColumns.Any())
                {
                    sb.Append(Environment.NewLine);
                    sb.Append($"ORDER BY {string.Join(", ", orderByColumns)}");
                }


                if (string.IsNullOrWhiteSpace(As))
                {
                    return sb.ToString();
                }
                else
                {
                    return $"({sb.ToString()}) AS {As}";
                }
            }
        }

        private List<string> fromTables = new List<string>();
        private List<(string table, string condition, string spec)> joinTables = new List<(string table, string condition, string spec)>();
        private string updateTable = string.Empty;
        private string insertTable = string.Empty;
        private List<string> selectColumns = new List<string>();
        private List<(string col, string value)> setColumns = new List<(string col, string value)>();
        private List<string> groupByColumns = new List<string>();
        private List<string> orderByColumns = new List<string>();
        private string whereCondition = string.Empty;

        public SqlizerQueryBuilder FROM(params string[] tables)
        {
            this.fromTables.AddRange(tables);
            return this;
        }

        public SqlizerQueryBuilder AS(string name)
        {
            this.As = name;
            return this;
        }

        public SqlizerQueryBuilder SELECT(params string[] columns)
        {
            if (columns == null || !columns.Any())
            {
                this.selectColumns.Add("*");
            }
            else
            {
                this.selectColumns.AddRange(columns);
            }
            return this;
        }

        public SqlizerQueryBuilder UPDATE(string table)
        {
            this.updateTable = table;
            return this;
        }

        public SqlizerQueryBuilder INSERT_INTO(string table)
        {
            this.insertTable = table;
            return this;
        }

        public SqlizerQueryBuilder SET(string column, string value)
        {
            this.setColumns.Add((column, value));
            return this;
        }

        public SqlizerQueryBuilder WHERE(string condition)
        {
            this.whereCondition = condition;
            return this;
        }

        public SqlizerQueryBuilder GROUPBY(params string[] columns)
        {
            this.groupByColumns.AddRange(columns);
            return this;
        }

        public SqlizerQueryBuilder ORDERBY(params string[] columns)
        {
            this.orderByColumns.AddRange(columns);
            return this;
        }

        public SqlizerQueryBuilder JOIN(string table, string condition)
        {
            this.joinTables.Add((table, condition, ""));
            return this;
        }

        public SqlizerQueryBuilder LEFT_JOIN(string table, string condition)
        {
            this.joinTables.Add((table, condition, "LEFT "));
            return this;
        }

        public static implicit operator string(SqlizerQueryBuilder queryBuilder)
        {
            return queryBuilder.ToString();
        }

        public override string ToString()
        {
            return toString;
        }
    }
}
