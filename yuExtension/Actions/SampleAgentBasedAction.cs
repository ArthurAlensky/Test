using System.Text;
using Inedo.BuildMaster;
using Inedo.BuildMaster.Extensibility.Actions;
using Inedo.BuildMaster.Extensibility.Agents;

namespace yuExtension.Actions
{
    [ActionProperties(
        "Sample AgentBasedActionBase Action",
        "An action that demonstrates basic AgentBasedActionBase usage by writing all variables to a file on the remote agent. "
        + "This action should only be used when ExecuteCommandLine does not provide the necessary functionality.")]
    [Tag("sample")]
    public class SampleAgentBasedAction : AgentBasedActionBase
    {
        protected override void Execute()
        {
            var buffer = new StringBuilder();
            foreach (var variable in this.Context.Variables)
            {
                buffer.AppendLine(string.Format("{0}:\t{1}", variable.Key, variable.Value));
            }

            byte[] fileBytes = Encoding.UTF8.GetBytes(buffer.ToString());

            // see http://inedo.com/support/kb/1070/writing-an-agent-based-action for the list
            // of services provided by an agent
            var fileOperationsExecuter = this.Context.Agent.GetService<IFileOperationsExecuter>();

            string path = fileOperationsExecuter.CombinePath(this.Context.SourceDirectory, "variables.txt");
            this.LogInformation("Writing variables to file: {0}", path);
            fileOperationsExecuter.WriteFileBytes(path, fileBytes);
        }

        public override string ToString()
        {
            return "Writes all variables to variables.txt in the default directory on the remote agent.";
        }
    }
}
