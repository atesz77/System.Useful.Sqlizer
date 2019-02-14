using System;
using System.Collections.Generic;
using System.Text;
using System.Useful.Sqlizer.Builders;

namespace System.Useful.Sqlizer
{
    public static class SqlizerQuery
    {
        #region Commands

        public static SqlizerQueryBuilder FROM(params string[] tables)
        {
            return new SqlizerQueryBuilder().FROM(tables);
        }

        public static SqlizerQueryBuilder UPDATE(string table)
        {
            return new SqlizerQueryBuilder().UPDATE(table);
        }

        public static SqlizerQueryBuilder DELETE_FROM(string table)
        {
            return new SqlizerQueryBuilder().DELETE_FROM(table);
        }

        public static SqlizerQueryBuilder INSERT_INTO(string table)
        {
            return new SqlizerQueryBuilder().INSERT_INTO(table);
        }

        public static SqlizerQueryBuilder SET(string column, string value)
        {
            return new SqlizerQueryBuilder().SET(column, value);
        }

        public static SqlizerQueryBuilder SELECT(params string[] columns)
        {
            return new SqlizerQueryBuilder().SELECT(columns);
        }

        public static SqlizerQueryBuilder SELECT_DISTINCT(params string[] columns)
        {
            return new SqlizerQueryBuilder().SELECT_DISTINCT(columns);
        }

        public static SqlizerQueryBuilder SELECT_TOP(int top, params string[] columns)
        {
            return new SqlizerQueryBuilder().SELECT_TOP(top, columns);
        }

        public static SqlizerQueryBuilder WHERE(string condition)
        {
            return new SqlizerQueryBuilder().WHERE(condition);
        }

        public static SqlizerCaseWhenBuilder CASE()
        {
            return new SqlizerCaseWhenBuilder();
        }

        public static string SCOPE(params string[] queries)
        {
            return string.Join(Environment.NewLine, queries);
        }

        #endregion


        #region Helpers

        public static string II(this string toApostrofize)
        {
            return $"'{toApostrofize}'";
        }

        #endregion


        #region Comparisons

        public static string EQUALS(string left, string right)
        {
            return $"{left} = {right}";
        }

        public static string NOT_EQUALS(string left, string right)
        {
            return $"{left} <> {right}";
        }

        public static string LIKE(string column, string like)
        {
            return $"{column} LIKE '{like}'";
        }

        public static string STARTS_WIDTH(string column, string text)
        {
            return $"{column} LIKE '{text}%'";
        }

        public static string ENDS_WIDTH(string column, string text)
        {
            return $"{column} LIKE '%{text}'";
        }

        public static string CONTAINS(string column, string text)
        {
            return $"{column} LIKE '%{text}%'";
        }

        public static string LESS(string left, string right)
        {
            return $"{left} < {right}";
        }

        public static string LESS_EQUALS(string left, string right)
        {
            return $"{left} <= {right}";
        }

        public static string GREATER(string left, string right)
        {
            return $"{left} > {right}";
        }

        public static string GREATER_EQUALS(string left, string right)
        {
            return $"{left} >= {right}";
        }

        public static string IS_NULL(string column)
        {
            return $"{column} IS NULL";
        }

        public static string IS_NOT_NULL(string column)
        {
            return $"{column} IS NOT NULL";
        }

        public static string IN(string column, params string[] inValues)
        {
            return $"{column} IN ({string.Join(", ", inValues)})";
        }

        public static string NOT_IN(string column, params string[] notinValues)
        {
            return $"{column} NOT IN ({string.Join(", ", notinValues)})";
        }

        public static string DISTINCT(string column)
        {
            return $"DISTINCT {column}";
        }

        public static string BETWEEN(string column, string startValue, string endValue)
        {
            return $"{column} BETWEEN {startValue} AND {endValue}";
        }

        #endregion


        #region Logical

        public static string AND(params string[] conditions)
        {
            return $"({string.Join(" AND ", conditions)})";
        }

        public static string OR(params string[] conditions)
        {
            return $"({string.Join(" OR ", conditions)})";
        }

        public static string NOT(string condition)
        {
            return $"NOT ({condition})";
        }

        #endregion


        #region Calculations

        public static string ISNULL(string column, string value)
        {
            return $"ISNULL({column}, {value})";
        }

        public static string COUNT(string column)
        {
            return $"COUNT({column})";
        }

        public static string SUM(string column)
        {
            return $"SUM({column})";
        }

        public static string AVG(string column)
        {
            return $"AVG({column})";
        }

        public static string MIN(string column)
        {
            return $"MIN({column})";
        }

        public static string MAX(string column)
        {
            return $"MAX({column})";
        }

        #endregion


        #region Functions

        public static string GETDATE()
        {
            return $"GETDATE()";
        }

        public static string SCOPE_IDENTITY()
        {
            return $"SCOPE_IDENTITY()";
        }

        public static string CAST(string value, string type)
        {
            return $"{value} AS {type}";
        }

        #endregion


        #region Types

        public static string INT => "INT";
        public static string NVARCHAR_MAX => "NVARCHAR(MAX)";
        public static string NVARCHAR(int size) => $"NVARCHAR({size})";
        public static string UNIQUEIDENTIFIER => "UNIQUEIDENTIFIER";
        public static string BIT => "BIT";
        public static string DATETIME => "DATETIME";
        public static string DATE => "DATE";
        public static string TIME => "TIME(7)";

        public static string NULL => "NULL";

        #endregion


        #region Declarations

        public static string DECLARE_TABLE(string variable, Dictionary<string, string> columns)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"DECLARE {variable} TABLE(");
            int i = 0;
            foreach (var item in columns)
            {
                sb.Append(Environment.NewLine);
                sb.Append($"   {item.Key} {item.Value}{(i < columns.Count - 1 ? "," : "")}");
                i++;
            }
            sb.Append(Environment.NewLine);
            sb.Append(")");

            return sb.ToString();
        }

        public static string DECLARE(string variable, string type, string value = null)
        {
            return $"DECLARE {variable} {type}{(string.IsNullOrWhiteSpace(value) ? "" : $" = {value}")}";
        }

        #endregion


        #region Set manipulation

        public static string UNION(params string[] queries)
        {
            return string.Join($"{Environment.NewLine}UNION", queries);
        }

        public static string UNION_ALL(params string[] queries)
        {
            return string.Join($"{Environment.NewLine}UNION ALL", queries);
        }

        #endregion
    }
}
