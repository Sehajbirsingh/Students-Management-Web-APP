using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;

namespace Students.Pages.StudentsData
{
    public class EditModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=STUDENTS;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM students WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentInfo.id = "" + reader.GetInt32(0);
                                studentInfo.name = reader.GetString(1);
                                studentInfo.email = reader.GetString(2);
                                studentInfo.phone = reader.GetString(3);
                                studentInfo.address = reader.GetString(4);
                                studentInfo.father_name = reader.GetString(5);
                                studentInfo.mother_name = reader.GetString(6);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            studentInfo.id = Request.Form["id"];
            studentInfo.name = Request.Form["name"];    
            studentInfo.email = Request.Form["email"];
            studentInfo.address = Request.Form["address"];
            studentInfo.father_name = Request.Form["father_name"];
            studentInfo.mother_name = Request.Form["mother_name"];

            if (studentInfo.name.Length == 0 || studentInfo.email.Length == 0 || studentInfo.phone.Length == 0 || studentInfo.address.Length == 0 || studentInfo.father_name.Length == 0 || studentInfo.mother_name.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=STUDENTS;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE students" + "SET name=@name,email=@email,phone=@phone,address=@address,father_name=@father_name,mother_name=@mother_name" + "WHERE id=@id";

                    using (SqlCommand command=new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", studentInfo.name);
                        command.Parameters.AddWithValue("@email", studentInfo.email);
                        command.Parameters.AddWithValue("@phone", studentInfo.phone);
                        command.Parameters.AddWithValue("@address", studentInfo.address);
                        command.Parameters.AddWithValue("@father_name", studentInfo.father_name);
                        command.Parameters.AddWithValue("@mother_name", studentInfo.mother_name);
                        command.Parameters.AddWithValue("@id", studentInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/StudentsData/Index");
        }
    }
}
