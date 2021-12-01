
<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <h3 align="center">Film Repository</h3>
  <p align="center">
    An app for sharing your favorite movies.
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
  </ol>
</details>



## About The Project

This application provides a  database for users and their favorite films. The goal of this project was to learn how to use dependency injection and mocking to create testable code.

### Modules
* FilmUI - A console application which displays a list of all users and their favorite films.
* FilmLibrary - A reusable library which provides an asynchronous data access layer (DAL) to the database. Made with Dapper ORM, it provides models for each table and contains all SQL queries needed for the app.
* FilmLibrary.Tests - Uses xUnit and Moq to test the FilmLibrary module.
* FilmDatabase - Contains the SQL table definitions for the database as well as a post deployment script for creating the example database records.

### Built With

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)

## Getting Started

To get a local copy up and running, follow these steps.

### Prerequisites

Download [Visual Studio 2019.](https://visualstudio.microsoft.com/downloads/)<br/>
In the Visual Studio Installer, install the .NET desktop development workload.<br />
Then install the Data storage and processing toolset.

### Installation

1. Clone the repo
   ```sh
   https://github.com/mrees791/film-repository.git
   ```
2. Open the FilmRepository.sln solution file with Visual Studio.
3. Build the solution in Visual Studio.
4. In the Solution Explorer, publish the FilmDatabase by right clicking on FilmDatabase and select Publish...
5. In the Target Database Connection, click Edit, Browse, then select MSSQLLocalDB then click OK.
6. Enter FilmDatabase in the Database Name field then click Publish.
7. In Solution Explorer, right click on FilmUI and click Set as Startup Project.
8. Start the console app by clicking Debug, then Start Debugging or Start Without Debugging.

## Contact

Michael Rees - mrees791@gmail.com

Project Link: [https://github.com/mrees791/film-repository](https://github.com/mrees791/film-repository)

## Acknowledgements
* [Dapper ORM](https://www.nuget.org/packages/Dapper/)
* [xUnit.net](https://xunit.net/)
* [Moq](https://github.com/moq/moq4)
