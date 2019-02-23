namespace DotNet.Project.LaunchSettings.Tests
{
    using FluentAssertions;
    using Xunit;
    
    public class ResultTests
    {
        [Fact]
        public void SuccessfulResultsShouldReturnResult()
        {
            Profile actual = default;

            var expected = StubbedProfiles.First;
            
            var result = new Result(true, expected);
            result.Match(() => { }, x => { actual = x; });

            actual.Should().Be(expected);
        }
        
        [Fact]
        public void UnsuccessfulResultsShouldCallAction()
        {
            var actual = 0;
            
            var result = new Result(false, default);
            result.Match(() => { actual++; }, _ => { });

            actual.Should().Be(1);
        }
    }
}