namespace EventDrivenExercise.Core.Abstractions
{
    public interface IUserAuditLogService
    {
        void Subscribe(IUserService userService);
    }
}