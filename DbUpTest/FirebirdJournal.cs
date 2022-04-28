using DbUp.Builder;
using DbUp.Firebird;

namespace DbUpTest
{
    public static class FirebirdJournal
    {
        public static UpgradeEngineBuilder JournalToFirebirdTable(this UpgradeEngineBuilder builder, string table)
        {
            builder.Configure(c => c.Journal = new FirebirdTableJournal(() => c.ConnectionManager, () => c.Log, table.ToUpper()));
            return builder;
        }
    }
}