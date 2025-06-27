using System;
using System.Threading.Tasks;
using F1_Data_Analysis.Models;
using Models;

namespace F1_Data_Analysis.Services;

public class FileService(IFileParser fileParser) : IDatabaseService
{
    private IFileParser _fileParser = fileParser;
    public async Task<LapTime[]> FetchContent()
    {
        return await _fileParser.FetchContent();
    }
}
