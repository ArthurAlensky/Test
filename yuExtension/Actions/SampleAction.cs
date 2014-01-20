using Inedo.BuildMaster;
using Inedo.BuildMaster.Extensibility.Actions;

namespace yuExtension.Actions
{
 
    [ActionProperties(
        "Sample Action",
        "An action that demonstrates basic ActionBase usage by logging the specified text. " 
        + "ActionBase actions are not designed to interact with agents.")]
    [Tag("sample")]
    public class SampleAction : ActionBase 
        // this action is not sealed because it's inherited by SampleWithEditorAction; if this
        // were a real extension, we'd likely make an abstract base class with an internal 
        // constructor. Extensions should generally not reference/link other extension assemblies,
        // especially for inheritance purposes, and sealing enforces this practice
    {
        /// <summary>
        /// Gets or sets the warning text.
        /// </summary>
        /// <remarks>
        /// If null or empty, no warning text will be logged.
        /// </remarks>
        [Persistent]
        public string WarningText { get; set; }

        /// <summary>
        /// Gets or sets the information text.
        /// </summary>
        /// <remarks>
        /// If null or empty, no information text will be logged.
        /// </remarks>
        [Persistent]
        public string InformationText { get; set; }

        /// <summary>
        /// This method is called to execute the Action.
        /// </summary>
        protected override void Execute()
        {
            this.LogDebug("Logging text...");
            if (!string.IsNullOrEmpty(this.WarningText)) this.LogWarning(this.WarningText);
            if (!string.IsNullOrEmpty(this.InformationText)) this.LogInformation(this.InformationText);
            this.LogDebug("Finished logging!");
        }

        /// <summary>
        /// Returns a 
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(this.WarningText))
            {
                return "Log Warning Text";
            }
            else if (!string.IsNullOrEmpty(this.InformationText))
            {
                return "Log Information Text.";
            }
            else
            {
                return "Log Nothing";
            }
        }
    }
}
