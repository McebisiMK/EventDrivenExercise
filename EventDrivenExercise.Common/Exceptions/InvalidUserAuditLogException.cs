namespace EventDrivenExercise.Common.Exceptions
{
    public class InvalidUserAuditLogException : Exception
    {
        public InvalidUserAuditLogException(string message) : base(message) { }
    }
}