using Inedo.BuildMaster.Extensibility.Actions;
using Inedo.BuildMaster.Web.Controls;
using Inedo.BuildMaster.Web.Controls.Extensions;
using Inedo.Web.Controls;

namespace yuExtension.Actions
{
    public sealed class SampleWithEditorActionEditor : ActionEditorBase
    {
        // this is just a normal control with two required methods (Bind and Create)
        // CustomAttributeAction does support .ascx controls, though we find that having
        // pure C# controls are a bit easier to code/debug/maintain

        #region Control Creation
        private ValidatingTextBox txtInformationText, txtWarningText;
        
        protected override void CreateChildControls()
        {
            this.txtInformationText = new ValidatingTextBox { Required = false };
            this.txtWarningText = new ValidatingTextBox { Required = false };

            this.Controls.Add(
                // FormFieldGroup controls are what we use to have the two-column appearance
                // not necessary, but it keeps a consistent UI
                new FormFieldGroup(
                    "Texts to Log",
                    "When left empty, no text will be logged.",
                    false,
                    new StandardFormField("Information Text:", this.txtInformationText),
                    new StandardFormField("Warning Text:", this.txtWarningText)
                )
            );
        }
        #endregion

        #region Required Overrides
        public override void BindToForm(ActionBase extension)
        {
            // always a good practice before accessing child controls
            this.EnsureChildControls(); 
            
            // unless this editor is used by multiple actions, we can
            // be assured that the extension being passed in is the class
            // we linked with CustomEditorAttribute
            var action = (SampleWithEditorAction)extension;
            action.WarningText = txtWarningText.Text;
            action.InformationText = txtInformationText.Text;
        }

        public override ActionBase CreateFromForm()
        {
            // always a good practice before accessing child controls
            this.EnsureChildControls();

            return new SampleWithEditorAction
            {
                InformationText = txtInformationText.Text,
                WarningText = txtWarningText.Text
            };
        }
        #endregion
    }
}
