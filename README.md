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
assertions. This is what most people typically see as unit testing. These execute at a rate of 1000 or more per second typical
computer hardware.

## Integration Testing

This testing mode happens when validating a unit with a highly complex dependency (usually a library of some sort). 

Database wrappers are the most common example. Mocking an ORM has little value (in relation to the amount of code required) and 
SQL is code that needs to be tested. Write tests that exercise the code by actually talking to the database and asserting 
that expected records are created. Do the work in a transaction if possible to rollback the changes. 

Another common example is a wrapper that accesses data via an HTTP call. You still need to test it because you need to ensure
you've built the url and request parameters properly.

## Collaboration

Collaboration tests validate that a unit correctly coordinates work among multiple dependencies. This is often done using 
a mock framework or test stubs/doubles/fakes. Typical examples of this type of unit are Controllers (MVC) and Presenters 
(MVP). The dependencies typically are a combination of multiple other units that have been tested via Unit or Integration 
testing, but delegating to other units that are tested by Collaboration are not uncommon.

A unit that is tested via collaboration has:
* one or many collaborators (dependencies) that are injected (usually via a constructor)
* conditional statements that are only used to determine the next method to call on a collaborator.

Note that a calculation combining values is not permitted. This would be handed off to another collaborator, likely one that
is unit tested.
