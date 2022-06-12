using EventDrivenExercise.Common.DTOs;

namespace EventDrivenExercise.Common.EventArgurments
{
    public class UserUpdatedEventArguments : EventArgs
    {
        public UserDTO PreviousUser { get; set; }
        public UserDTO CurrentUser { get; set; }
    }
}