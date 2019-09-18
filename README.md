# Welcome/Introduction

I've been playing with this concept since 2016. It has proven to be useful to me in 
designing software. This is my attempt to codify the principle into something others 
can use. The accompanying code samples are crafted specifically to demonstrate 
aspects of the principle and in particular, the varying modes of test and how
they effect design. This is not production quality code! Use at your own risk.

# Single Testability Principle

Definition: A unit should require only a single mode of testing to be verified. Utilizing multiple modes of testing is an indicator of excessive complexity, and can be used to decompose a unit into smaller, more reusable, and more distinct parts.

# Testing Modes

A testing mode is simply a way in which testing of a unit is completed. 

## Unit testing

This testing mode is characterized by a fully isolated unit like a single function or class with no external dependencies. It 
receives test data via parameters or properties. It produces output via a return value or property. It is tested via simple 
assertions. This is what most people typically see as unit testing. These execute at a rate of 1000 or more per second on typical computer hardware.

### Unit test sample code

Our contrived example program accepts user input via a console, adds them, and returns the result. The adding of numbers is the most isolated and simple unit to build, so this is created first. It also lends itself directly and obviously to unit-mode testing.

Test Sample: [CalculatorTests](https://github.com/mvphelps/SingleTestabilityPrinciple/blob/master/Tests/CalculatorTests.cs). This test fixture creates the unit under test and interacts with only it. 

Unit Sample: [Calculator](https://github.com/mvphelps/SingleTestabilityPrinciple/blob/master/MathApp/Calculator.cs). This class doesn't have any dependencies, and everything is self contained. In this case the result comes out via a return value, but a state change exposed via a property is also acceptable.

## Integration Testing

This testing mode happens when validating a unit with a highly complex dependency (usually a library of some sort). The dependency is used in it's entirety, the only thing different than production use may be alternate configuration. Although it may be possible to mock or stub out parts of the dependency, it generally would result in a lot of code that has nothing to do with the system requirements the unit is intended to satisfy - the dependency is opaque.

These tests are slower than Unit tests, and execute at a rate of 100s per second on typical on computer hardware.

Database wrappers are the most common example. Mocking an ORM has little value (in relation to the amount of code required) and 
SQL is code that needs to be tested. Write tests that exercise the code by actually talking to the database and asserting 
that expected records are created. Do the work in a transaction if possible to rollback the changes. 

Another common example is a wrapper that accesses data via an HTTP call. You still need to test it because you need to ensure
you've built the url and request parameters properly.

### Integration test sample code

Our example also records the user input and results, presumably to direct future product development. This is done by recording events to a text file. It could just as easily be a database table or a post to an HTTP service. 

Test Sample: [UsageLoggerTests](https://github.com/mvphelps/SingleTestabilityPrinciple/blob/master/Tests/UsageLoggerTests.cs). This test validates that opaque dependency did what it should - preferably by using an alternate means.

Unit Sample: [UsageLogger](https://github.com/mvphelps/SingleTestabilityPrinciple/blob/master/MathApp/UsageLogger.cs). This class simulates an opaque dependency by writing to the file system.


## Collaboration

Collaboration tests validate that a unit correctly coordinates work among multiple dependencies. The dependencies are referred to in this case as collaborators. This is often done using 
a mock framework or test stubs/doubles/fakes for the collaborators. Typical examples of this type of unit are Controllers (MVC) and Presenters 
(MVP). The collaborators typically are a combination of multiple other units that have been tested via Unit or Integration 
testing, but delegating to other units that are tested by Collaboration are not uncommon. Execution rates fall between Integration and Unit tests. 

A unit that is tested via collaboration has:
* one or many collaborators (dependencies) that are injected (usually via a constructor)
* conditional statements that are only used to determine the next method to call on a collaborator.

Note that a calculation combining values is not permitted. This would be handed off to another collaborator, likely one that
is unit tested.

### Collaboration test sample code

Our example code uses a presenter pattern to coordinate the user interaction with the screen (the View), as well as our units that do the actual work - the usage logger and the calculator. The presenter accepts all of these as dependencies via the constructor. 

Test Sample: [PresenterTests](https://github.com/mvphelps/SingleTestabilityPrinciple/blob/master/Tests/PresenterTests.cs). This test fixture sets up mock objects for all of the dependencies, then creates the presenter, sending in all of the collaborators via the constructor. Note specifically the AddsAndLogsWhenBothAreNumbers test. It sets up the calculator with a non-sensical behavior - that adding 1 and 38 results in 6. This is intentional - since a real calculator isn't being used, it really doesn't matter what the contents of parameters or return values are - just that the proper items are passed around to the right places. For example, it verifies that the non-sensical 6 is later converted to a string an sent to the view. Normally I will use a realistic example - if this were production code I would likely have actually used 39. I used 6 here to make the point clearer.

Unit Sample: [Presenter](https://github.com/mvphelps/SingleTestabilityPrinciple/blob/master/MathApp/Presenter.cs). Note that all of the conditional statements are only concerned with whether and how to interact further with another collaborator. These are not calculations, they are for determining behavior.
