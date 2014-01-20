using Inedo.BuildMaster;
using Inedo.BuildMaster.Extensibility.Actions;
using Inedo.BuildMaster.Extensibility.Predicates;

namespace yuExtension.Predicates
{
    [PredicateProperties(
        "Sample Predicate",
        "A predicate that will always evaluate to the configured boolean value.")]
    public sealed class SamplePredicate : PredicateBase
    {
        /// <summary>
        /// Gets or sets the value returned when the predicate is evaluated
        /// </summary>
        [Persistent]
        public bool EvaluateValue { get; set; }

        public override bool Evaluate(IActionExecutionContext context)
        {
            return this.EvaluateValue;
        }

        public override string ToString()
        {
            return "Dummy predicate that is always \"" + this.EvaluateValue.ToString().ToLower() + "\"";
        }
    }
}
