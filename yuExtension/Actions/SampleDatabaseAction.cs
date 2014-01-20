using System;
using Inedo.BuildMaster;
using Inedo.BuildMaster.Extensibility.Actions;
using Inedo.BuildMaster.Extensibility.Actions.Database;

namespace yuExtension.Actions
{
    [ActionProperties(
        "Sample DatabaseBaseAction Action",
        "An action that demonstrates DatabaseBaseAction usage by logging information about the connection string.")]
    [Tag("sample")]
    public sealed class SampleDatabaseAction : DatabaseBaseAction
    {
        protected override void Execute()
        {
            // logging the number of characters in a connection string is useless, but keep in mind
            // that logging the connection string would be *beyond* useless because it would expose the 
            // database ConnectionString to users who don't have the privileges to edit/view database providers
            this.LogInformation("The connection string has {0} characters.", this.Provider.ConnectionString.Length);
            
            // something that could be potentially useful is something like the following commented out line
            // this would hide/abstract the SQL query, and allow you to reuse it from action to action
            
            // this.Provider.ExecuteQuery("UPDATE SomeTable SET SomeColumn='SomeValue' ...");
        }

        public override string ToString()
        {
            return "Log connection string information.";
        }
    }
}
