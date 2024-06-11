## JobNest

A web application portal developed using ASP.NET Entity Framework Core, designed to streamline the job search process for 
both employers and applicants. With a robust database structure and role-based functionality, JobNest offers a flexible and scalable 
platform for managing job listings and applications.

## Built With
<p align='center'>
  <img src="https://img.shields.io/badge/code-javascript-informational?style=for-the-badge&logo=javascript&logoColor=white&color=2aa889"/>&nbsp;
  <img src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white"/>&nbsp;
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white"/>&nbsp;
  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white"/>&nbsp;
  <img src="https://img.shields.io/badge/web-html-informational?style=for-the-badge&logo=html5&logoColor=white&color=2aa889"/>&nbsp;
  <img src="https://img.shields.io/badge/web-css-informational?style=for-the-badge&logo=css3&logoColor=white&color=2aa889"/>&nbsp;
</p>

## Demo
Click [here](https://jobnest.azurewebsites.net/) to check it out

## Functionality

* User Roles: Users can have different roles (e.g., "Applicant" or "Employer"), managed through the USERROLE table.
* Employers: Check the "Are you an Employer" box to register as an employer. You can create job listings. Each job is associated with the employer who posted it.
* Applicants: Can apply for jobs, and their applications are stored in the APPLICATION table. Each application is associated with the applicant who made it and the job they applied for.

## Database Schema

### USER Table
This table stores information about users in the system. Each user can have one or more roles associated with them.

Fields:

    UserID
    UserName
    Email
    PasswordHash

### JOB Table
Contains information about job listings in the system. Each job is associated with the employer who posted it.

Fields:

    JobID
    Position
    CompanyName
    Salary
    StartingDate
    Location
    EmployerID (foreign key)

### APPLICATION Table
Stores information about job applications made by users.

Fields:

    ApplicationID
    UserID (foreign key)
    JobID (foreign key)
    ApplicationDate

### USERROLE Table
Establishes a many-to-many relationship between users and roles.

Fields:

    UserID (foreign key)
    RoleID (foreign key)

### ROLE Table
Stores information about the roles available in the system.
Fields:

    RoleID
    RoleName

#### Roles available:

    Applicant
    Employer

## Benefits

Flexibility: The database schema provides a flexible and scalable way to manage user roles, job listings, and applications.
Role-based Functionality: Enables role-based functionality and access control tailored to the specific needs of applicants and employers.

Jobnest offers a comprehensive solution for managing job listings and applications, providing a seamless experience for both employers and applicants.
