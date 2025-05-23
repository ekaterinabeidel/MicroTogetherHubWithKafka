namespace Core.Exceptions;

public class VersionConflictException : Exception
{
    public VersionConflictException(
        string message = nameof(VersionConflictException)
    ) : base(message) { }
}