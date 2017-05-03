# rock-paper-scissors

Simple application to play a match of Rock, Paper, Scissors

## Getting Started

Download or close the code and build the solution in Visual Studio 2015 or 2017.
Set the startup project as RockPaperScissors.GameConsole.

## Built With

* [Autofac](https://autofac.org/) - Dependency Injection
* [Moq](https://github.com/moq) - Mocking in unit tests
* [Nunit](https://www.nunit.org/) - Used for unit testing
* [FluentAssertion](http://fluentassertions.com/) - Used for unit testing to naturally specify the expected outcome of a test
* [NBuilder](https://github.com/nbuilder/nbuilder) - Used in unit testing create test objects

## Improvement
What I would add to improve
* Add a web front end for player to choose moves
* Add validation of user choices
* Add a wrapper around the Random functionality in OpponentManager.GetNextMove to make it testable
