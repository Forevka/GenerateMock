namespace GenerateMock.Utilities.Exceptions
{
    public enum ExceptionEnum
    {
        UserNotFound = 1,
        UserAlreadyExist = 2,
        RepositoryDoesntExist = 3,
        FileAtGivenPathDoesntExist = 4,
        VersionOrDatabaseNameMustBeProvided = 5,
        GivenVersionNotValid = 6,
        DatabaseWithGivenParametersNotExist = 7,
        FileTypeNotValid = 8,
        FileContentNotValid = 9,
        ThisFeatureNotImplemented = 10,
    }
}
