<%@ Control Language="C#" ClassName="PaqueteServicioFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaqueteServicioId" runat="server" Text="Paquete Servicio Id:" AssociatedControlID="dataPaqueteServicioId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPaqueteServicioId" Value='<%# Bind("PaqueteServicioId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataServicioId" runat="server" Text="Servicio Id:" AssociatedControlID="dataServicioId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataServicioId" DataSourceID="ServicioIdServicioDataSource" DataTextField="Descripcion" DataValueField="ServicioId" SelectedValue='<%# Bind("ServicioId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:ServicioDataSource ID="ServicioIdServicioDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaqueteId" runat="server" Text="Paquete Id:" AssociatedControlID="dataPaqueteId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPaqueteId" DataSourceID="PaqueteIdPaqueteDataSource" DataTextField="Descripcion" DataValueField="PaqueteId" SelectedValue='<%# Bind("PaqueteId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:PaqueteDataSource ID="PaqueteIdPaqueteDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


