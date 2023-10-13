using System;

namespace TestNinja.HousekeeperHelper
{
    public interface IStatementGenerator
    {
        string SaveStatement(int housekeeperOid, string housekeeperName, DateTime statementDate);
    }
}