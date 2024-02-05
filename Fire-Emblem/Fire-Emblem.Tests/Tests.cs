namespace Fire_Emblem.Tests;
using Fire_Emblem_View;
using Fire_Emblem;

public class Tests
{
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E1-BasicCombat")]
    public void TestE1_BasicCombat(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E1-InvalidTeams")]
    public void TestE1_InvalidTeams(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E2")]
    public void TestE2(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E2-Random")]
    public void TestE2_Random(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E2-Mix")]
    public void TestE2_Mix(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E3")]
    public void TestE3(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E3-Random")]
    public void TestE3_Random(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E3-Mix")]
    public void TestE3_Mix(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E4-1")]
    public void TestE4_1(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E4-1-Random")]
    public void TestE4_1_Random(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E4-1-Mix")]
    public void TestE4_1_Mix(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E4-2")]
    public void TestE4_2(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E4-2-Random")]
    public void TestE4_2_Random(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);
    
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), parameters: "E4-2-Mix")]
    public void TestE4_2_Mix(string deckFolder, string testFile)
        => RunTest(deckFolder, testFile);

    public static IEnumerable<object[]> GetTestsAssociatedWithThisFolder(string deckFolder)
    {
        deckFolder = Path.Combine("data", deckFolder);
        var testFolder = deckFolder + "-Tests"; 
        var testFiles = GetAllTestFilesFrom(testFolder);
        return ConvertDataIntoTheAppropriateFormat(deckFolder, testFiles);
    }
    
    private static string[] GetAllTestFilesFrom(string folder)
        => Directory.GetFiles(folder, "*.txt", SearchOption.TopDirectoryOnly);

    private static IEnumerable<object[]> ConvertDataIntoTheAppropriateFormat(string deckFolder, string[] testFiles)
    {
        var allData = new List<object[]>();
        foreach (var testFile in testFiles)
            allData.Add(new object []{deckFolder, testFile});
        return allData;
    }

    private static void RunTest(string teamsFolder, string testFile)
    {
        var view = View.BuildTestingView(testFile);
        var game = new Game(view);
        game.Play();

        var actualScript = view.GetScript();
        var expectedScript = File.ReadAllLines(testFile);
        CompareScripts(actualScript, expectedScript);
    }

    private static void CompareScripts(IReadOnlyList<string> actualScript, IReadOnlyList<string> expectedScript)
    {
        var numberOfLines = Math.Max(expectedScript.Count, actualScript.Count);
        for (var i = 0; i < numberOfLines; i++)
        {
            var expected = GetTheItemOrEmptyIfOutOfIndex(i, expectedScript);
            var actual = GetTheItemOrEmptyIfOutOfIndex(i, actualScript);
            Assert.Equal($"[L{i+1}] " + expected, $"[L{i+1}] " + actual);
        }
    }

    private static string GetTheItemOrEmptyIfOutOfIndex(int index, IReadOnlyList<string> array)
        => index < array.Count ? array[index] : "";
}