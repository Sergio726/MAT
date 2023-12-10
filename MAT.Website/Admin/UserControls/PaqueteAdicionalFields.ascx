<%@ Control Language="C#" ClassName="PaqueteAdicionalFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaqueteAdicionalId" runat="server" Text="Paquete Adicional Id:" AssociatedControlID="dataPaqueteAdicionalId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPaqueteAdicionalId" Value='<%# Bind("PaqueteAdicionalId") %>'></asp:HiddenField>
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
        <td class="literal"><asp:Label ID="lbldataAdicionalId" runat="server" Text="Adicional Id:" AssociatedControlID="dataAdicionalId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataAdicionalId" DataSourceID="AdicionalIdAdicionalDataSource" DataTextField="Monto" DataValueField="AdicionalId" SelectedValue='<%# Bind("AdicionalId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:AdicionalDataSource ID="AdicionalIdAdicionalDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


