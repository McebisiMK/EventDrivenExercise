using EventDrivenExercise.Common.DTOs;

namespace EventDrivenExercise.Common.EventArgurments
{
    public class UserDeletedEventArguments : EventArgs
    {
        public UserDTO DeletedUser { get; set; }
    }
}