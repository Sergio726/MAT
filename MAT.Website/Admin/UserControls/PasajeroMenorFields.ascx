<%@ Control Language="C#" ClassName="PasajeroMenorFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPasajeid" runat="server" Text="Pasajeid:" AssociatedControlID="dataPasajeid" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPasajeid" Value='<%# Bind("Pasajeid") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPasajeroid" runat="server" Text="Pasajeroid:" AssociatedControlID="dataPasajeroid" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPasajeroid" DataSourceID="PasajeroidClienteDataSource" DataTextField="RazonSocial" DataValueField="ClienteId" SelectedValue='<%# Bind("Pasajeroid") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:ClienteDataSource ID="PasajeroidClienteDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMenorid" runat="server" Text="Menorid:" AssociatedControlID="dataMenorid" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataMenorid" DataSourceID="MenoridClienteDataSource" DataTextField="RazonSocial" DataValueField="ClienteId" SelectedValue='<%# Bind("Menorid") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:ClienteDataSource ID="MenoridClienteDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


