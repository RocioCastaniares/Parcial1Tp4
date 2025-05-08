using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Proveedor : System.Web.UI.Page
{
    string cadena = WebConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargarProveedores();
    }

    private void CargarProveedores()
    {
        using (SqlConnection con = new SqlConnection(cadena))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Proveedores", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvProveedores.DataSource = dt;
            gvProveedores.DataBind();
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(cadena))
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Proveedores (Nombre) VALUES (@Nombre)", con);
            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        Logger.Registrar($"Alta Proveedor: {txtNombre.Text}");
        txtNombre.Text = "";
        CargarProveedores();
    }

    protected void gvProveedores_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        int id = (int)gvProveedores.DataKeys[e.RowIndex].Value;

        using (SqlConnection con = new SqlConnection(cadena))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Proveedores WHERE IdProveedor = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        Logger.Registrar($"Baja Proveedor ID: {id}");
        CargarProveedores();
    }
}
