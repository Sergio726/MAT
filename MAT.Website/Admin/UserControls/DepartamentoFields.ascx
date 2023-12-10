<%@ Control Language="C#" ClassName="DepartamentoFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataIdProvincia" runat="server" Text="Id Provincia:" AssociatedControlID="dataIdProvincia" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataIdProvincia" Text='<%# Bind("IdProvincia") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataIdProvincia" runat="server" Display="Dynamic" ControlToValidate="dataIdProvincia" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataIdProvincia" runat="server" Display="Dynamic" ControlToValidate="dataIdProvincia" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNombre" runat="server" Text="Nombre:" AssociatedControlID="dataNombre" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNombre" Text='<%# Bind("Nombre") %>' MaxLength="250"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataNombre" runat="server" Display="Dynamic" ControlToValidate="dataNombre" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


