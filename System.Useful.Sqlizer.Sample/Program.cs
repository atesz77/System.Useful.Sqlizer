using System;
using System.Collections.Generic;
using static System.Useful.Sqlizer.SqlizerQuery;

namespace System.Useful.Sqlizer.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var a = new Person();
            var b = new Person().AS("c");
            var c = new Email();

            var name = new SqlizerParameter("name");

            var query = SELECT_TOP(1, a.Age, b.Age, b.Name.AS("Kutya"), c.DeletedAt).FROM(a, b);

            var query2 = SELECT(c.Address).FROM(a, SELECT().FROM(a, b, c).AS("x"), "Valami.Asd c");

            var query3 = 
                 SELECT(a.ALL(), ISNULL(c.Address, "asd@asd.hu".II()),
                    CASE()
                    .WHEN(EQUALS(a.Age, b.Age), "1")
                    .WHEN(NOT_EQUALS(a.Age, b.Age), "0", "-1")
                    .AS("Medve")
                 )
                .FROM(a, c)
                .JOIN(b, EQUALS(a.Age, b.Age))
                .LEFT_JOIN(c, EQUALS(c.Address, a.Name))
                .WHERE(
                    AND(
                        EQUALS(a.Name, c.Address),
                        OR(
                            LESS_EQUALS(c.DeletedAt, "2010-10-10".II()),
                            NOT(c.DeletedAt),
                            IS_NOT_NULL(c.DeletedAt)
                        ),
                        NOT_EQUALS(a.Age, "2"),
                        OR(
                            OR(
                                LIKE(a.Name, "medve%kutya"),
                                EQUALS(a.Name, name)
                            ),
                            ENDS_WIDTH(c.Address, "gmail.com"),
                            NOT_IN(c.Address, "Budapest".II(), "Győr".II(), "Debrecen".II())
                        )
                    )
                )
                .GROUPBY(a.Name)
                .ORDERBY(a.Age, a.Name.DESC(), "2")
                ;

            var query4 = UPDATE(a)
                .SET(a.Age, "10")
                .SET(a.Name, "Medve".II())
                .WHERE(EQUALS(a.Age, "9"));

            var query5 = INSERT_INTO(a)
               .SET(a.Age, GETDATE())
               .SET(a.Name, "Medve".II());

            var query8 = INSERT_INTO(b)
               .SELECT_DISTINCT(a.Age, a.Name)
               .FROM(a);

            var query6 = SCOPE(
                DECLARE(name, NVARCHAR(10)),
                DECLARE(name, BIT, "1"),
                DECLARE_TABLE(name, new Dictionary<string, string>
                {
                    ["FelhasznaloId"] = INT,
                    ["FelhasznaloNev"] = NVARCHAR(40),
                    ["Statusz"] = DATETIME
                }),
                query2,
                query5
            );

            var query7 = UNION(query2, query3);

            var query9 = DELETE_FROM(a)
                .WHERE(LESS(a.Age, "10"));

            Console.WriteLine(query);
            Console.WriteLine();
            Console.WriteLine(query2);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(query3);
            Console.WriteLine();
            Console.WriteLine(query4);
            Console.WriteLine();
            Console.WriteLine(query5);
            Console.WriteLine();
            Console.WriteLine(query8);
            Console.WriteLine();
            Console.WriteLine(query9);
            Console.WriteLine();
            Console.WriteLine("---------------");
            Console.WriteLine(query6);
            Console.WriteLine("---------------");
            Console.WriteLine(query7);
        }
    }
}
