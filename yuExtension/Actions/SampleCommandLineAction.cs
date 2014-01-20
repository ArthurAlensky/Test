using System;
using Inedo.BuildMaster;
using Inedo.BuildMaster.Extensibility.Actions;

namespace yuExtension.Actions
{
    [ActionProperties(
        "Basic ExecuteCommandLine Action",
        "An action that demonstrates simple ExecuteCommandLine usage by wrapping cmd.exe to echo the specified text.")]
    [Tag("sample")]
    public sealed class SampleCommandLineAction : AgentBasedActionBase
    {
        /// <summary>
        /// Gets or sets the text to echo to cmd.exe.
        /// </summary>
        [Persistent]
        public string TextToEcho { get; set; }

        protected override void Execute()
        {
            // the ExecuteCommandLine method will execute the specified command on the agent
            var returnCode =  this.ExecuteCommandLine(
                "cmd.exe",
                "/c echo " + this.TextToEcho,
                this.Context.SourceDirectory);
        }

        public override string ToString()
        {
            return "Echos \"" + this.TextToEcho + "\" via cmd.exe.";
        }
    }
}
