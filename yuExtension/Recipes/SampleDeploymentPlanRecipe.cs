using Inedo.BuildMaster.Data;
using Inedo.BuildMaster.Extensibility.Recipes;
using Inedo.Data;

namespace yuExtension.Recipes
{
    [RecipeProperties("Sample Deployment Plan Recipe",
        "Flips the active indicator on all Action Groups within the specified environment.", 
        RecipeScopes.DeploymentPlan)]
    public sealed class SampleDeploymentPlanRecipe : RecipeBase, IDeploymentPlanRecipe
    {
        // these properties are defined in the IDeploymentPlanRecipe interface and will be set by the host before execution
        public int ApplicationId { get; set; }
        public int EnvironmentId { get; set; }

        public override void Execute()
        {
            // retrieve the PlanActionGroups from the database
            var planActionGroups = StoredProcs
                .Plans_GetPlanActionGroups(this.ApplicationId, this.EnvironmentId)
                .ExecuteDataTable()
                .AsStrongTyped<Tables.PlanActionGroups_Extended>();

            foreach (var planActionGroup in planActionGroups)
            {
                StoredProcs
                    .Plans_UpdatePlanActionGroup(
                        planActionGroup.PlanActionGroup_Id,
                        planActionGroup.OverridesReferenced_Indicator,
                        planActionGroup.Active_Indicator == Domains.YN.Yes
                            ? Domains.YN.No
                            : Domains.YN.Yes,
                        planActionGroup.ActionGroup_Name,
                        planActionGroup.ActionGroup_Description,
                        planActionGroup.PlanActionGroup_Predicate_Configuration,
                        planActionGroup.PlanActionGroup_Predicate_Description,
                        planActionGroup.PlanActionGroup_Server_Id,
                        planActionGroup.Deployable_Id)
                    .ExecuteNonQuery();
            }
        }

        
    }
}
