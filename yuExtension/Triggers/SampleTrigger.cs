using System;
using System.IO;
using Inedo.BuildMaster;
using Inedo.BuildMaster.Events;
using Inedo.BuildMaster.Extensibility.Notifiers;
using Inedo.BuildMaster.Extensibility.Notifiers.Triggers;

namespace yuExtension.Notifiers
{
    // Note that this class is implementing the market interface ITrigger; from a coding perspective, this is the only difference
    // between triggers and notifiers. In BuildMaster, notifiers are simply associated with a user account, whereas triggers are not.
    
    [NotifierProperties(
        "Sample Trigger", 
        "Logs every System_Configuration_Changed event to a text file.")]
    public sealed class SampleNotifer : NotifierBase, ITrigger
    {
        [Persistent]
        public string TextFilePath { get; set; }

        public override void EventOccured(NotificationContext context)
        {
            File.AppendAllText(this.TextFilePath, string.Format("{0} - Event {1} triggered; key changed was {2}.",
                DateTime.Now,
                context.Event.EventCode,
                context.OccurrenceDetails[EventTypes.System_Configuration_Changed.EventDetails.Key_Name]));

        }

        public override EventType[] Events
        {
            get { return new[] { EventTypes.System_Configuration_Changed }; }
        }

        public override string ToString()
        {
            return "Log System_Configuration_Changed Events to " + this.TextFilePath;
        }
    }
}
