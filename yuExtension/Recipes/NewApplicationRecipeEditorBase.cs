using Inedo.BuildMaster.Extensibility.Recipes;
using Inedo.BuildMaster.Web.Controls.Extensions;

namespace yuExtension.Recipes
{
    /// <summary>
    /// Represents a custom editor for the <see cref="T:NewApplicationRecipeBase" /> recipe.
    /// </summary>
    internal abstract class NewApplicationRecipeEditorBase<T> : RecipeEditorBase
        where T : NewApplicationRecipeBase, new()
    {
        public override string ExecuteRecipeButtonText { get { return "Create Application"; } }

        public override RecipeBase CreateFromForm()
        {
            return new T();
        }

        protected override void CreateChildControls()
        {
        }
    }
}
