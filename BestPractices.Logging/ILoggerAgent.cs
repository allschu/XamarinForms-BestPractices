namespace BestPractices.Logging
{
    public interface ILoggerAgent
    {
        void Debug(string debugInformation);
        void Error(string errorMessage);
        void Information(string informationMessage);
        void Warning(string warningMessage);
    }
}