<%@ Control Language="C#" ClassName="ProvinciaFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataNombre" runat="server" Text="Nombre:" AssociatedControlID="dataNombre" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNombre" Text='<%# Bind("Nombre") %>' MaxLength="250"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataNombre" runat="server" Display="Dynamic" ControlToValidate="dataNombre" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataIdPais" runat="server" Text="Id Pais:" AssociatedControlID="dataIdPais" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataIdPais" Value='<%# Bind("IdPais") %>'></asp:HiddenField>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


