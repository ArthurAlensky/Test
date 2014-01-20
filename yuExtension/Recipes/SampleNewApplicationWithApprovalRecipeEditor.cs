using System.Web.UI.WebControls;
using Inedo.BuildMaster;
using Inedo.BuildMaster.Configuration;
using Inedo.BuildMaster.Data;
using Inedo.BuildMaster.Extensibility.Recipes;
using Inedo.BuildMaster.Web.Controls;
using Inedo.Web.Controls;

namespace yuExtension.Recipes
{
    internal sealed class SampleNewApplicationWithApprovalRecipeEditor : NewApplicationRecipeEditorBase<SampleNewApplicationWithApprovalRecipe>
    {
        private DirectoryBrowser txtUserName;
        private ValidatingTextBox txtDescription;
        private CheckBox chkIsGroupApproval;

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            int directoryProviderId = int.Parse(StoredProcs.Configuration_GetValue("CoreEx", "DirectoryProvider", null).Execute());

            this.txtUserName = new DirectoryBrowser() 
            {
                DirectoryProviderId = directoryProviderId,
                Required = true, 
                Width = 270, 
                DisplayMode = DirectoryBrowser.DisplayModes.UsersAndGroups 
            };

            this.txtDescription = new ValidatingTextBox() { DefaultText = "Approved by [username]...", Width = 300 };
            this.chkIsGroupApproval = new CheckBox() { Text = "Group-based approval" };

            this.Controls.Add(
                new FormFieldGroup(
                    "Approval Details",
                    "This approval will be required for all deployments to the final environment. " + 
                    "Enter the username, the description of this approval, and whether the username represents a group.",
                    false,
                    new StandardFormField("User Name:", this.txtUserName),
                    new StandardFormField("Description:", this.txtDescription),
                    new StandardFormField("", this.chkIsGroupApproval)
                )
            );
        }

        public override RecipeBase CreateFromForm()
        {
            return new SampleNewApplicationWithApprovalRecipe()
            {
                UserName = this.txtUserName.Text,
                ApprovalDescription = this.txtDescription.Text,
                IsGroupApproval = this.chkIsGroupApproval.Checked
            };
        }
    }
}
