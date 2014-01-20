using Inedo.BuildMaster;
using Inedo.BuildMaster.Extensibility.Actions;
using Inedo.BuildMaster.Web;

namespace yuExtension.Actions
{
    [ActionProperties(
        "Sample Action (With Custom Editor)",
        "An action that's identical to Sample Action, except that this one has a CustomEditor.")]
    [Tag("sample")]
    [CustomEditor(typeof(SampleWithEditorActionEditor))]
    public sealed class SampleWithEditorAction : SampleAction
    {
        // the only thing different is that this child class has a 
        // CustomEditor attribute
    }
}
