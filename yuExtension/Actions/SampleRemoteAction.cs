using System;
using Inedo.BuildMaster;
using Inedo.BuildMaster.Extensibility.Actions;

namespace yuExtension.Actions
{
    [ActionProperties(
        "Sample RemoteActionBase Action",
        "An action that demonstrates RemoteActionBase's ProcessRemoteCommand usage.")]
    [Tag("sample")]
    public sealed class SampleRemoteAction : RemoteActionBase
    {
        /// <summary>
        /// Gets or sets the non persistent property text; this text will *not* be saved in the database
        /// nor will it make it over the remoting boundary when the action is de-serialized
        /// </summary>
        public string NonPersistentPropertyText { get; set; }

        /// <summary>
        /// Gets or sets the text to log.
        /// </summary>
        [Persistent]
        public string TextToLog { get; set; }
        
        protected override void Execute()
        {
            // when this ExecuteRemoteCommand is called, the following will occur:
            //  1. The [Persistent] properties are serialized
            //  2. The Agent(s) this action is associated with are contacted
            //     a. The action is de-serialized
            //     b. ProcessRemoteCommand is called with the specified command and args
            //     c. The action's logs and return values are packaged and sent back
            //  3. The logs entries are saved to the database and the return value is passed as the output of ExecuteRemoteCommand
            this.ExecuteRemoteCommand("log-text");
            this.ExecuteRemoteCommand("log-args", this.TextToLog);
            this.ExecuteRemoteCommand("log-other");

        }

        protected override string ProcessRemoteCommand(string name, string[] args)
        {
            // note that the ProcessRemoteCommand method should not be called directly;
            // otherwise, there's no remote action things taking place

            switch (name)
            {
                case "log-text":
                    this.LogInformation("TextToLog is \"{0}\"", this.TextToLog);
                    return null;
                case "log-args":
                    this.LogInformation("args[0] is \"{0}\"", args[0]);
                    return null;
                case "log-other":
                    this.LogInformation("NonPersistentPropertyText is \"{0}\"", this.NonPersistentPropertyText);
                    return null;
                default:
                    throw new InvalidOperationException(name + " is an unknown command.");
            }
        }

        public override string ToString()
        {
            return "Logs \"" + this.TextToLog + "\" via a remote command.";
        }
    }
}
