# TEST-3
Student Management System (SMS) - ASP.NET Core Web API
Introduction
This project is a Student Management System (SMS) developed using ASP.NET Core Web API. It allows administrators, teachers, and students to manage courses, enrollments, and grades.

Setup Instructions:

1. Download and Extract the Project
Download the project zip file from the provided URL.
Extract the zip file to your desired location on your computer.

2. Open Project in Visual Studio 2022
Open Visual Studio 2022.
Go to File > Open > Project/Solution.
Navigate to the folder where you extracted the project and select the solution file (StudentManagementSystem.sln).
Click Open to open the project in Visual Studio.

3. Set Multiple Startup Projects
Right-click on the solution in the Solution Explorer.
Select Properties.
In the properties window, navigate to Startup Project section.
Select Multiple startup projects.
Set the Action for both SMS-APP and StudentManagementSystem.API projects to Start.
Click OK to save the changes.

4. Update Database
Open the NuGet Package Manager Console: Tools > NuGet Package Manager > Package Manager Console.
Ensure that StudentManagementSystem is selected as the Default project in the Package Manager Console dropdown.
Run the following command to apply pending migrations and update the database:
mathematica
Copy code
Update-Database
This command will apply migrations and create the database with the necessary tables.

5. Run the Project
Press Ctrl + F5 or click Start to run the project.
Both the SMS-APP  and StudentManagementSystem.API projects will start simultaneously.

6. Register Users
Upon running the project, navigate to the registration page.
Register the first user with the role of Admin.
Register the second user with the role of Teacher.
Register any additional users with the role of Student.

7. Log in as Admin
Use the credentials of the Admin user to log in.
As an Admin, you have access to add course details.

8. Log in as Teacher
Use the credentials of the Teacher user to log in.
As a Teacher, you can see the enrolled student list and give grades to students.

9. Log in as Student
Use the credentials of a Student user to log in.
As a Student, you can enroll in courses and view your grade details.
By following these steps, you should be able to set up and run the Student Management System project successfully. Enjoy managing courses, enrollments, and grades!






