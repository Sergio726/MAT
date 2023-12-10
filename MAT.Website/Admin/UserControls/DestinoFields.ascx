<%@ Control Language="C#" ClassName="DestinoFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataLocalidadId" runat="server" Text="Localidad Id:" AssociatedControlID="dataLocalidadId" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataLocalidadId" Text='<%# Bind("LocalidadId") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataLocalidadId" runat="server" Display="Dynamic" ControlToValidate="dataLocalidadId" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDestinoId" runat="server" Text="Destino Id:" AssociatedControlID="dataDestinoId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataDestinoId" Value='<%# Bind("DestinoId") %>'></asp:HiddenField>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


