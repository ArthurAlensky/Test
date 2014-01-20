using System;
using System.Collections.Generic;
using System.Text;
using Inedo.BuildMaster.Extensibility.PromotionRequirements;
using Inedo.BuildMaster;

namespace yuExtension.PromotionRequirement
{
    [PromotionRequirementsProperties(
        "Sample Promotion Requirement",
        "A promotion requirement that, depending on configuration, is always met or not met.")]
    public sealed class SamplePromotionRequirement : PromotionRequirementBase
    {

        /// <summary>
        /// Gets or sets a value indicating whether this promotion requirement is required.
        /// </summary>
        [Persistent]
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this promotion requirement is met.
        /// </summary>
        [Persistent]
        public bool Met { get; set; }


        public override bool IsRequired(PromotionContext context)
        {
            // most of the time you'll want to return "true" here, as this just gives some 
            // flexibility in determining if a promotion requirement must be verified
            //
            // for example, "Unit Tests Passed" would not be required if there are no unit 
            // tests associated with the build. in the UI, this could be represented 
            // as "Passed", "Fail", "N/A"

            return this.Required;
        }

        public override bool IsMet(PromotionContext context)
        {
            // when False is returned, a Build must be forced to be promoted

            return this.Met;
        }

        public override string ToString()
        {
            return
                (this.Required ? "" : "Not ") + "Required" +
                " and " +
                (this.Met ? "" : "Not ") + "Met";
        }
    }
}
