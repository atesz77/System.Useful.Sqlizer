
//**************************************************//
//             THIS IS A GENERATED FILE             //
//**************************************************//

namespace System.Useful.Sqlizer.Sample
{
    public class Person : SqlizerTable<Person>
    {
        public SqlizerColumn<string> Name => new SqlizerColumn<string>(this, nameof(Name));
        public SqlizerColumn<int> Age => new SqlizerColumn<int>(this, nameof(Age));
    }
    
    public class Rendszer : SqlizerTable<Rendszer>
    {
    }
    
    public class Email : SqlizerTable<Email, Rendszer>
    {
        public SqlizerColumn<string> Address => new SqlizerColumn<string>(this, nameof(Address));
        public SqlizerColumn<DateTime?> DeletedAt => new SqlizerColumn<DateTime?>(this, nameof(DeletedAt));
    }
    
}
