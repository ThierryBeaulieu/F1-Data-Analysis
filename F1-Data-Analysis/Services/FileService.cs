using System;
using F1_Data_Analysis.Models;
using Models;

namespace F1_Data_Analysis.Services;

public class FileService(IFileParser fileParser) : IDatabaseService
{
    private IFileParser _fileParser = fileParser;
    public void FetchContent(LapTimes lapTimes)
    {
        _fileParser.FetchContent(lapTimes);
    }
}
