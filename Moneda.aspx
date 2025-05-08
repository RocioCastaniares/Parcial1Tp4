<asp:Label ID="lblNombre" runat="server" Text="Nombre Moneda:"></asp:Label>
<asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
<br />
<asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
<asp:GridView ID="gvMonedas" runat="server" AutoGenerateColumns="False" DataKeyNames="IdMoneda" OnRowDeleting="gvMonedas_RowDeleting">
    <Columns>
        <asp:BoundField DataField="IdMoneda" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        <asp:CommandField ShowDeleteButton="True" />
    </Columns>
</asp:GridView>
