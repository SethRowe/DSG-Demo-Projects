Project Notes
==============

This project is a simple example of how Cucumber/Gherkin could be used to author
integration tests against a RESTful API.

**_Note:_** _Be sure to first run the API project so that IIS will be running the API. Otherwise, 
the tests will fail, normally with a very cryptic error message!_

### SpecFlow

The 'official' Cucumber port for .NET is SpecFlow (http://specflow.org/). SpecFlow is structured as a
third-party plugin to Visual Studio, and adds item templates for .feature files. Once those files are
created, you can right-click within the editor to generate the step definitions. By default, SpecFlow uses
the NUnit testing framework, and integrates into the Visual Studio Test Runner. Each run, it also generates
a report file listing which specifications passed or failed. The link to the report is found in the Output 
window, or can be located under the TestResults folder for the project.

SpecFlow has some negatives, some more major than others. First, its nature as a Visual Studio plugin means
that every team member must use Visual Studio, and must have the plugin installed. Second, they have a premium
version available, and use 'nag-ware' principles (such as delaying each test run by a number of seconds with a 
message to 'purchase the premium license') to promote it. Additionally, SpecFlow does not yet support .NET
Core, which at best is an annoyance, and at worst could prevent its adoption in a project.

### Xunit.Gherkin.Quick

An alternate to SpecFlow - and one that does support .NET Core - is Xunit.Gherkin.Quick 
(https://github.com/ttutisani/Xunit.Gherkin.Quick). It was designed to be a lightweight alternate to SpecFlow
and does not require any "extra" items such as the Visual Studio plugin.

However, the project is not yet fully featured, and notably does not include support for the Scenario Outline
syntax (where you can pass in a table of Examples to be ran in a Scenario. Additionally, the lack of a plugin
for Visual Studio means that you will have to manually auther the step definitions, rather than have them
generated for you (not a huge issue, but something to be considered).

Because of its lack of support for Scenario Outlines, I did not create a sample project for it.
