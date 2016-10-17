using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCategory();
        }
    }
    /// <summary>
    /// Allows the user to display list of category
    /// from the table Categories to the dropdownlist control.
    /// </summary>
    void GetCategory()
    {
        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"SELECT CatID, Category FROM Categories";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    ddlCategories.DataSource = dr;
                    ddlCategories.DataTextField = "Category";
                    ddlCategories.DataValueField = "CatID";
                    ddlCategories.DataBind();

                    ddlCategories.Items.Insert(0, new ListItem("Select one...", ""));
                }
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string fileName1 = Path.GetFileName(txtImage.PostedFile.FileName);
        txtImage.PostedFile.SaveAs(Server.MapPath("~/img/") + fileName1);

        using (SqlConnection con = new SqlConnection(Util.GetConnection()))
        {
            con.Open();
            string SQL = @"INSERT INTO Products VALUES
                (@Name, @CatID, @Code, @Description, @Image, @Price, @IsFeatured, @Available,
                @CriticalLevel, @Maximum, @Status, @DateAdded, @DateModified)";

            using (SqlCommand cmd = new SqlCommand(SQL, con))
            {
                cmd.Parameters.AddWithValue("@Name", txtProductName.Text);
                cmd.Parameters.AddWithValue("@CatID", ddlCategories.SelectedValue);          
                cmd.Parameters.AddWithValue("@Code", txtCode.Text);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@Image", txtImage.Value);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@IsFeatured", txtIsFeatured.Text);
                cmd.Parameters.AddWithValue("@Available", txtAvailable.Text);
                cmd.Parameters.AddWithValue("@CriticalLevel", txtCriticalLevel.Text);
                cmd.Parameters.AddWithValue("@Maximum", txtMaximum.Text);
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModified", DBNull.Value);
                cmd.ExecuteNonQuery();
                Response.Redirect("Default.aspx");
            }
        }
    }
}