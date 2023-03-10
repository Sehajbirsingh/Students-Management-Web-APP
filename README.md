
# Class students information Management 

Here I created an asp.net core web
application that aims to connect an sql
server database and performs CRUD
operation which means that the application allows 
us to create new students to read the
list of students from the database to
update the student's data and also to soft 
delete student data which means which mean
on deleting a student, it should disappear 
from the View screen, however, should be present in the DB records.
Apart from this a sorting function was also addeded to sort student's name and address based on custom requirements.



## Methodology

- In Visual Studio created a new ASP.NET Core Web Application project. Selected the "Web Application" template and with "ASP.NET Core 6.x" version.

- Once the project is created, open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console) and run the following command to install the necessary packages:




```bash
  Install-Package Microsoft.EntityFrameworkCore.SqlServer

```
```bash
  Install-Package Microsoft.AspNetCore.Cors

```


- In the Models folder, create a new class for the Student model. This class should have properties for the student's id, name, father's name, mother's name, age, address, registration date and a flag for soft delete.
```c#
//Soft delete: "soft deletion," I added a new column to the table called "IsDeleted" with a default value of 0 (false).
//Then, we would change the SQL query in the code to update the "IsDeleted" column to 1 (true) instead of deleting the row.
@page
@using System.Data.SqlClient;
@{
    try
    {
        String id = Request.Query["id"];

        String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=STUDENTS;Integrated Security=True";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            String sql = "UPDATE clients SET IsDeleted = 1 WHERE id=@id";
            using (SqlCommand command=new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
    }
    Response.Redirect("/StudentsData/Index");
}

```
- In the Models folder, create a new class that inherits from DbContext. This class will be used to interact with the database. Gave it a DbSet for the Student model and create a constructor that accepts a DbContextOptions object.

- In the appsettings.json file, added a new ConnectionStrings section and added the connection string for the SQL Server using SQLEXPRESS.

- In the Startup.cs file, configured the DbContext to use the SQL Server connection string and enabled CORS in order to perform CRUD operations successfully.

- In the Controllers folder, create a new controller for the Student. This controller should have methods for handling the different HTTP requests such as GET, POST
### SQL database 

```sql
CREATE TABLE students (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    name VARCHAR (100) NOT NULL,
    email VARCHAR (150) NOT NULL UNIQUE,
    phone VARCHAR(20) NULL,
    address VARCHAR(100) NULL,
    father_name VARCHAR (100) NOT NULL,
    mother_name VARCHAR (100) NOT NULL,
    registered_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO students (name, email, phone, address,father_name,mother_name)
VALUES
('Bill Gates', 'bill.gates@microsoft.com', '+123456789',
 '32-A,sham nagar,New York, USA','Ramu','kaki'),
('Elon Musk', 'elon.musk@spacex.com', '+111222333',
 '54, lawrence road,Florida, USA','sunny','pinky'),
('Will Smith', 'will.smith@gmail.com', '+111333555',
 '36-I,kailash nagar,California, USA','umesh','dolly'),
('Bob Marley', 'bob@gmail.com', '+111555999',
 'BRS nagar,Texas, USA','sham','pia'),
('Cristiano Ronaldo', 'cristiano.ronaldo@gmail.com',
 '+32447788993', '32-webley street,Manchester, England','sam','molly'),
('Boris Johnson', 'boris.johnson@gmail.com', '+4499778855',
 '10 dowing street,London, England','kaku','chinky');
```

### SCREENSHOT OF WORKING PROJECT
 1. Read only 
 
  ![Read](https://user-images.githubusercontent.com/51527083/213648262-84848428-7c11-4266-955f-727a8883093b.png)
 
 2. Sorting the data by name and address(Home City)
  
  ![sort by name](https://user-images.githubusercontent.com/51527083/213648766-108e760a-8620-4000-ba74-16ec96acca6e.png)
  
  ![sort by address](https://user-images.githubusercontent.com/51527083/213648793-8ad7b8e6-c0b9-495f-bc1d-4bccee382ed3.png)
 
 3. Creating New Student 
 
 ![creating new student](https://user-images.githubusercontent.com/51527083/213648361-29669334-0bd1-4753-944c-525410a98f0b.png)
 
 After Submitting we can see the added student comes at last position with new 
  
  id number i.e. 7 with different timestamp as well
  ![after creating](https://user-images.githubusercontent.com/51527083/213648597-987a96f9-f4e2-440d-b2b4-aab56f1bc483.png)

4. On deleting the latest entry i.e. id=7
  
  ![soft delete](https://user-images.githubusercontent.com/51527083/213648921-b1327308-56fa-4626-b981-0948c64ac2de.png)



