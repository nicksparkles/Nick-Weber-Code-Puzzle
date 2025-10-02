namespace RedRoverCodePuzzle.Tests;

public class StringHelperTests
{
    [Fact]
    public void TestOutput()
    {
        // Arrange
        const string input = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
        const string expectedOutput = @"
- id
- name
- email
- type
  - id
  - name
  - customFields
    - c1
    - c2
    - c3
- externalId";

        // Act
        var actual = StringHelper.FormatString(input);

        // Assert
        Assert.Equal(expectedOutput.Trim(), actual.Trim());

    }

    [Fact]
    public void TestOutput_Alphabetical()
    {
        // Arrange
        const string input = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
        const string expectedOutput = @"
- email
- externalId
- id
- name
- type
  - customFields
    - c1
    - c2
    - c3
  - id
  - name";

        // Act
        var actual = StringHelper.FormatString(input, FormatOptions.Alphabetical);

        // Assert
        Assert.Equal(expectedOutput.Trim(), actual.Trim());
    }
}