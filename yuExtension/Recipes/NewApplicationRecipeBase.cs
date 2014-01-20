using Inedo.BuildMaster.Extensibility.Recipes;

namespace yuExtension.Recipes
{
    public abstract class NewApplicationRecipeBase : RecipeBase, IExtendedApplicationCreatingRecipe
    {
        public int ApplicationId { get; set; }
        public string ApplicationGroup { get; set; }
        public string ApplicationName { get; set; }
        public bool AutoIncrementReleaseNumber { get; set; }
        public string ReleaseNumberScheme { get; set; }
        public string BuildNumberScheme { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewApplicationRecipeBase"/> class.
        /// </summary>
        public NewApplicationRecipeBase()
        {
        }
    }
}
