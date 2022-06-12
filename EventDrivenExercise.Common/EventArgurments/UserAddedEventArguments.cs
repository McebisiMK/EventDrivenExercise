using EventDrivenExercise.Common.DTOs;

namespace EventDrivenExercise.Common.EventArgurments
{
    public class UserAddedEventArguments : EventArgs
    {
        public UserDTO User { get; set; }
    }
}