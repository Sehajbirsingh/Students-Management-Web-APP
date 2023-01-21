using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Students.Pages.StudentsData
{
    public class CreateModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            studentInfo.name = Request.Form["name"];
            studentInfo.name = Request.Form["email"];
            studentInfo.name = Request.Form["phone"];
            studentInfo.name = Request.Form["address"];
            studentInfo.name = Request.Form["father_name"];
            studentInfo.name = Request.Form["mother_name"];

            if (studentInfo.name.Length ==0 || studentInfo.email.Length ==0 || studentInfo.phone.Length == 0 || studentInfo.address.Length == 0 || studentInfo.father_name.Length == 0 || studentInfo.mother_name.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new student into the database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=STUDENTS;Integrated Security=True";
                using (SqlConnection connection= new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO students" + "(name,email,phone,address,father_name,mother_name)VALUES" + "(@name,@email,@phone,@address,@father_name,@mother_name)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", studentInfo.name);
                        command.Parameters.AddWithValue("@email", studentInfo.email);
                        command.Parameters.AddWithValue("@phone", studentInfo.phone);
                        command.Parameters.AddWithValue("@address", studentInfo.address);
                        command.Parameters.AddWithValue("@father_name", studentInfo.name);
                        command.Parameters.AddWithValue("@mother_name", studentInfo.name);

                        command.ExecuteNonQuery();
                    }
                }             
            }
            catch (Exception ex)
            {
                errorMessage= ex.Message;
                return;
            }

            studentInfo.name = ""; studentInfo.email = ""; studentInfo.phone = ""; studentInfo.address = ""; studentInfo.father_name = ""; studentInfo.mother_name = "";
            successMessage = "New Student Added Correctly";
            Response.Redirect("/StudentsData/Index");
        }
    }
}
