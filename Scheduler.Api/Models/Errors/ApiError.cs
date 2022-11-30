namespace Scheduler.Models.Errors;

public enum ApiError
{
    UserDoesNotExist,
    UserWithTheSameEmailAlreadyExist,
    PasswordIsInvalid
}