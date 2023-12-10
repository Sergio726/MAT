<%@ Control Language="C#" ClassName="VendedorFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataVendedorId" runat="server" Text="Vendedor Id:" AssociatedControlID="dataVendedorId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataVendedorId" DataSourceID="VendedorIdPersonaDataSource" DataTextField="Apellido" DataValueField="PersonaId" SelectedValue='<%# Bind("VendedorId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:PersonaDataSource ID="VendedorIdPersonaDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDescripcion" runat="server" Text="Descripcion:" AssociatedControlID="dataDescripcion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDescripcion" Text='<%# Bind("Descripcion") %>' MaxLength="100"></asp:TextBox>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


