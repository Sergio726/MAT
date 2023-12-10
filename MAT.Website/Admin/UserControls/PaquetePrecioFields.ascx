<%@ Control Language="C#" ClassName="PaquetePrecioFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaquetePrecioId" runat="server" Text="Paquete Precio Id:" AssociatedControlID="dataPaquetePrecioId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPaquetePrecioId" Value='<%# Bind("PaquetePrecioId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaqueteId" runat="server" Text="Paquete Id:" AssociatedControlID="dataPaqueteId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPaqueteId" DataSourceID="PaqueteIdPaqueteDataSource" DataTextField="Descripcion" DataValueField="PaqueteId" SelectedValue='<%# Bind("PaqueteId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:PaqueteDataSource ID="PaqueteIdPaqueteDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPrecioId" runat="server" Text="Precio Id:" AssociatedControlID="dataPrecioId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPrecioId" DataSourceID="PrecioIdPrecioDataSource" DataTextField="Monto" DataValueField="PrecioId" SelectedValue='<%# Bind("PrecioId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:PrecioDataSource ID="PrecioIdPrecioDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


