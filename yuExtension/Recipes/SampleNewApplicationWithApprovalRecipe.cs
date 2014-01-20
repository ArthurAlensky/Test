using System;
using System.Linq;
using Inedo.BuildMaster;
using Inedo.BuildMaster.Data;
using Inedo.BuildMaster.Extensibility.Recipes;
using Inedo.BuildMaster.Web;

namespace yuExtension.Recipes
{
    /// <summary>
    /// Represents a recipe that creates an application with an approval in its workflow.
    /// </summary>
    [RecipeProperties(
        "Standard Application w/ Approval",
        "A standard application, the default workflow step ordering, a setup release, and an approval for production.",
        RecipeScopes.None)]
    [CustomEditor(typeof(SampleNewApplicationWithApprovalRecipeEditor))]
    public sealed class SampleNewApplicationWithApprovalRecipe : NewApplicationRecipeBase
    {
        public string UserName { get; set; }
        public string ApprovalDescription { get; set; }
        public bool IsGroupApproval { get; set; }

        public override void Execute()
        {
            int deployableId = Util.Recipes.CreateDeployable(this.ApplicationId, this.ApplicationName);
            int workflowId = Util.Recipes.CreateWorkflow(this.ApplicationId);

            var environments = StoredProcs.Environments_GetEnvironments(null).Execute().ToList();
            if (!environments.Any())
                throw new InvalidOperationException("There are no environments set up in the system.");

            foreach (var environment in environments)
            {
                Util.Recipes.CreateWorkflowStep(workflowId, environment.Environment_Id);
            }

            StoredProcs.Workflows_AddOrRemoveApproval(
                workflowId,
                environments.Last().Environment_Id,
                null,
                this.UserName,
                string.IsNullOrWhiteSpace(this.ApprovalDescription) 
                    ? "Approved by " + this.UserName 
                    : this.ApprovalDescription,
                this.IsGroupApproval ? "G" : "U",
                "N",
                null,
                "A",
                null).Execute();

            Util.Recipes.CreateSetupRelease(this.ApplicationId, this.ReleaseNumberScheme, workflowId, new[] { deployableId });
        }
    }
}