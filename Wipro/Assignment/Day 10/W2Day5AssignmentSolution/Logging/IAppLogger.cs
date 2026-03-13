namespace W2Day5AssignmentSolution.Logging;

public interface IAppLogger
{
    void Info(string message);
    void Error(string message, Exception exception);
}
