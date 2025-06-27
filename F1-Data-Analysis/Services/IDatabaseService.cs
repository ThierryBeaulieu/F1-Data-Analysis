using Models;

public interface IDatabaseService
{
    public Task<LapTime[]> FetchContent();
}