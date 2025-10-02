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
}