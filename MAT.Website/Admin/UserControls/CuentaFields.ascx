<%@ Control Language="C#" ClassName="CuentaFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataCuentaId" runat="server" Text="Cuenta Id:" AssociatedControlID="dataCuentaId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataCuentaId" Value='<%# Bind("CuentaId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataClienteId" runat="server" Text="Cliente Id:" AssociatedControlID="dataClienteId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataClienteId" DataSourceID="ClienteIdClienteDataSource" DataTextField="RazonSocial" DataValueField="ClienteId" SelectedValue='<%# Bind("ClienteId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:ClienteDataSource ID="ClienteIdClienteDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataEstado" runat="server" Text="Estado:" AssociatedControlID="dataEstado" /></td>
        <td>
					<asp:RadioButtonList runat="server" ID="dataEstado" SelectedValue='<%# Bind("Estado") %>' RepeatDirection="Horizontal"><asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem><asp:ListItem Value="False" Text="No"></asp:ListItem></asp:RadioButtonList>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


