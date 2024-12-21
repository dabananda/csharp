# ASP.NET Core MVC: Building a CRUD Application

[ASP.NET](http://asp.net/) Core MVC is a robust framework that empowers developers to build scalable web applications using the Model-View-Controller architectural pattern. In this comprehensive tutorial, I'll guide you through creating a web application from the ground up using [ASP.NET](http://asp.net/) Core MVC. Whether you're new to the framework or looking to strengthen your skills, this step-by-step guide will help you understand the core concepts and best practices. Let's dive in and start building together.

---

### Prerequisites

Before we begin, ensure you have the following installed:

1. [Visual Studio](https://visualstudio.microsoft.com/) (Community Edition).
2. [.NET SDK](https://dotnet.microsoft.com/en-us/download)
3. [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
4. [SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)

---

### Step 1: Setting Up the Project

1. Open Visual Studio and create a new project.
2. Select **ASP.NET Core Web App (Model-View-Controller)**.
3. Name your project (e.g., `StudentInfo`) and configure it as needed.

---

### Step 2: Creating the Model

The model represents the data structure. In this case, we create a `Domain`  folder and define a `Student` class in the `Domain` folder under the `Models`  folder (`Models → Domain → Student.cs`):

```csharp
namespace StudentInfo.Models.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Session {  get; set; }
    }
}
```

---

### Step 3: Setting Up the Database

1. Install the necessary packages from the top navigation bar: `Tools → NuGet Package Manager → Manage NuGet Package for Solution`
    
    ```
    Microsoft.EntityFrameworkCore
    Microsoft.EntityFrameworkCore.Tools
    Microsoft.EntityFrameworkCore.SqlServer
    ```
    
2. Create a `Data` folder in the project directory and create `StudentDbContext` class  inside the folder to manage database interactions:
    
    ```csharp
    using Microsoft.EntityFrameworkCore;
    using StudentInfo.Models.Domain;
    
    namespace StudentInfo.Data
    {
        public class StudentDbContext : DbContext
        {
            public StudentDbContext(DbContextOptions options) : base(options) { }
            public DbSet<Student> StudentInfos { get; set; }
        }
    }
    ```
    
3. Configure the connection string in `appsettings.json` . Replace `<Your_PC_Name>` with you pc name. To get it open the **SQL Server Management Studio** and copy the `server name`. Then tik on the `Trust Server Certificate` and `Connect`the database.
    
    ```json
    {
        "ConnectionStrings": {
            "StudentDbConnectionString": "Server=<Your_PC_Name>\\SQLEXPRESS;Database=StudentDb;Trusted_Connection=True;TrustServerCertificate=Yes"
        },
        "Logging": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*"
    }
    ```
    
4. Register the context in `Program.cs`  just below the line `builder.Services.AddControllersWithViews();` otherwise it will not work:
    
    ```csharp
    builder.Services.AddDbContext<StudentDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDbConnectionString"))
    );
    ```
    
5. Create and apply migrations:
    
    ```
    Add-Migration InitialCreate
    Update-Database
    ```
    

---

### Step 4: Now Create more Models

In the `Models`folder create another folder named `ViewModel`and create more two models `EditStudentRequest.cs` and `PostStudentRequest.cs`:

**EditStudentRequest.cs**

```jsx
namespace StudentInfo.Models.ViewModel
{
    public class EditStudentRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Session { get; set; }
    }
}
```

**PostStudentRequest.cs**

```jsx
namespace StudentInfo.Models.ViewModel
{
    public class PostStudentRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Session { get; set; }
    }
}
```

---

### Step 5: Creating the Controller

In the `Controllers`folder create a controller `AdminStudentController.cs` :

```
using Microsoft.AspNetCore.Mvc;
using StudentInfo.Data;
using StudentInfo.Models.Domain;
using StudentInfo.Models.ViewModel;

namespace StudentInfo.Controllers
{
    public class AdminStudentController : Controller
    {
        private readonly StudentDbContext studentDbContext;

        public AdminStudentController(StudentDbContext studentDbContext) 
        {
            this.studentDbContext = studentDbContext;
        }

        // Get the student list
        [HttpGet]
        public IActionResult GetStudents()
        {
            var studentList = studentDbContext.StudentInfos.ToList();
            return View(studentList);
        }

        // Get the add a new student form
        [HttpGet]
        public IActionResult PostStudent()
        {
            return View();
        }

        // Create new student
        [HttpPost]
        public IActionResult PostStudent(PostStudentRequest postStudentRequest)
        {
            var student = new Student
            {
                Name = postStudentRequest.Name,
                Department = postStudentRequest.Department,
                Session = postStudentRequest.Session,
            };
            studentDbContext.StudentInfos.Add(student);
            studentDbContext.SaveChanges();
            return RedirectToAction("GetStudents");
        }

        // Get the form to edit student with id
        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var student = studentDbContext.StudentInfos.SingleOrDefault(x => x.Id == id);
            if (student != null)
            {
                var editStudentRequest = new EditStudentRequest()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Department = student.Department,
                    Session = student.Session,
                };
                return View(editStudentRequest);
            }
            return View(null);
        }

        // Post the updated student
        [HttpPost]
        public IActionResult EditStudent(EditStudentRequest editStudentRequest)
        {
            var student = new Student()
            {
                Id = editStudentRequest.Id,
                Name = editStudentRequest.Name,
                Department = editStudentRequest.Department,
                Session = editStudentRequest.Session,
            };
            var existingStudent = studentDbContext.StudentInfos.Find(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Department = student.Department;
                existingStudent.Session = student.Session;
                studentDbContext.SaveChanges();

            }
            return RedirectToAction("GetStudents");
        }

        [HttpPost]
        public IActionResult DeleteStudent(EditStudentRequest editStudentRequest)
        {
            var student = studentDbContext.StudentInfos.Find(editStudentRequest.Id);
            if (student != null)
            {
                studentDbContext.StudentInfos.Remove(student);
                studentDbContext.SaveChanges();
            }
            return RedirectToAction("GetStudents");
        }
    }
}

```

---

### Step 6: Create Views for the Actions

Create a `AdminStudent` folder in the `Views` folder. Then create three views `EditStudent.cshtml`, `GetStudents.cshtml`  and `PostStudent.cshtml`:

**EditStudent.cshtml:**

```jsx
@{
	ViewData["Title"] = "Edit Student";
}

@model StudentInfo.Models.ViewModel.EditStudentRequest

<div class="bg-body-tertiary">
	<div class="container">
		<div class="card w-50 mx-auto">
			<form method="post">
				<div class="card-body">
					<h5 class="card-title text-center">Edit Student</h5>
					<input asp-for="Id" hidden />
					<div class="mb-3">
						<label class="form-label">Full Name</label>
						<input type="text" class="form-control" id="name" asp-for="Name" readonly>
					</div>
					<div class="mb-3">
						<label class="form-label">Department</label>
						<input type="text" class="form-control" id="department" asp-for="Department">
					</div>
					<div class="mb-3">
						<label class="form-label">Session</label>
						<input type="text" class="form-control" id="session" asp-for="Session">
					</div>
					<div class="d-flex justify-content-end">
						<button class="btn btn-dark me-2" type="submit" asp-action="EditStudent">Update</button>
						<button class="btn btn-danger" type="submit" asp-controller="AdminStudent" asp-action="DeleteStudent">Delete</button>
					</div>
				</div>
			</form>
		</div>
	</div>
</div>
```

**GetStudent.cshtml:**

```jsx
@model List<StudentInfo.Models.Domain.Student>

﻿@{
	ViewData["Title"] = "Get Student";
}

@{
	var students = Model ?? new List<StudentInfo.Models.Domain.Student>();
}

@if (!students.Any())
{
	<div class="card text-bg-warning" style="max-width: 18rem;">
		<div class="card-body">
			<h5 class="card-title">Student Database Is Empty</h5>
			<p class="card-text">There is no student data in the database.</p>
		</div>
	</div>
}
else
{
	<div class="bg-body-tertiary">
		<div class="container">
			<table class="table table-striped w-75 mx-auto">
				<thead>
					<tr>
						<th>ID</th>
						<th>Name</th>
						<th>Department</th>
						<th>Session</th>
						<th>Action</th>
					</tr>
				</thead>
				<tbody>
					@foreach (var student in students)
					{
						<tr>
							<th>@student.Id</th>
							<td>@student.Name</td>
							<td>@student.Department</td>
							<td>@student.Session</td>
							<td>
								<a class="btn btn-secondary" asp-controller="AdminStudent" asp-action="EditStudent" asp-route-id="@student.Id">Edit</a>
							</td>
						</tr>
					}
				</tbody>
			</table>
		</div>
	</div>
}

```

**PostStudent.cshtml:**

```jsx
@model StudentInfo.Models.ViewModel.PostStudentRequest

﻿@{
    ViewData["Title"] = "Post Student";
}

<div class="bg-body-tertiary">
    <div class="container">
        <div class="card w-50 mx-auto">
            <form method="post">
                <div class="card-body">
                    <h5 class="card-title text-center">Add New Student</h5>
                    <div class="mb-3">
                        <label class="form-label">Full Name</label>
                        <input type="text" class="form-control" id="name" asp-for="Name">
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Department</label>
                        <input type="text" class="form-control" id="department" asp-for="Department">
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Session</label>
                        <input type="text" class="form-control" id="session" asp-for="Session">
                    </div>
                    <button class="btn btn-dark" type="submit">Submit</button >
                </div>
            </form>
        </div>
    </div>
</div>

```

**Now edit the _Layout.cshtml in the `Views→Shared` folder to get the navigation:**

```jsx
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - StudentInfo</title>
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
    <link rel="stylesheet" href="~/StudentInfo.styles.css" asp-append-version="true" />
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container-fluid">
                <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">StudentInfo</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                Student
                            </a>
                            <ul class="dropdown-menu">
                                <li><a class="dropdown-item" asp-area="" asp-controller="AdminStudent" asp-action="GetStudents">Student List</a></li>
                                <li><a class="dropdown-item" asp-area="" asp-controller="AdminStudent" asp-action="PostStudent">Add New Student</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    <div class="container">
        <main role="main" class="pb-3">
            @RenderBody()
        </main>
    </div>

    <footer class="border-top footer text-muted">
        <div class="container">
            &copy; 2024 - StudentInfo - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
        </div>
    </footer>
    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="~/js/site.js" asp-append-version="true"></script>
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
```

---

### Step 7: Run the Application

1. Press `Crtl + F5` to run the applicatoin
2. Navigate to `Student -> Student List / Add Student` 
3. Test all CRUD operations.

Follow the steps properly to build a web application using [ASP.NET](http://ASP.NET) Core MVC.

Thank you everyone 💖