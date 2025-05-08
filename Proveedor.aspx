<h2>ABM Proveedores</h2>

<asp:Label ID="lblNombre" runat="server" Text="Nombre Proveedor:"></asp:Label>
<asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
<br /><br />

<asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
<br /><br />

<asp:GridView ID="gvProveedores" runat="server" AutoGenerateColumns="False"
    DataKeyNames="IdProveedor" OnRowDeleting="gvProveedores_RowDeleting">
    <Columns>
        <asp:BoundField DataField="IdProveedor" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        <asp:CommandField ShowDeleteButton="True" />
    </Columns>
</asp:GridView>
