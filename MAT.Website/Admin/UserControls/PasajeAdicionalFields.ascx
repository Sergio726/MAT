<%@ Control Language="C#" ClassName="PasajeAdicionalFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPasajeAdicionalId" runat="server" Text="Pasaje Adicional Id:" AssociatedControlID="dataPasajeAdicionalId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPasajeAdicionalId" Value='<%# Bind("PasajeAdicionalId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPasajeId" runat="server" Text="Pasaje Id:" AssociatedControlID="dataPasajeId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPasajeId" DataSourceID="PasajeIdPasajeDataSource" DataTextField="FechaReserva" DataValueField="PasajeId" SelectedValue='<%# Bind("PasajeId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:PasajeDataSource ID="PasajeIdPasajeDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataAdicionalId" runat="server" Text="Adicional Id:" AssociatedControlID="dataAdicionalId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataAdicionalId" DataSourceID="AdicionalIdAdicionalDataSource" DataTextField="Monto" DataValueField="AdicionalId" SelectedValue='<%# Bind("AdicionalId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:AdicionalDataSource ID="AdicionalIdAdicionalDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


