<%@ Control Language="C#" ClassName="TipoClienteFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataDescripcion" runat="server" Text="Descripcion:" AssociatedControlID="dataDescripcion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDescripcion" Text='<%# Bind("Descripcion") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTipoId" runat="server" Text="Tipo Id:" AssociatedControlID="dataTipoId" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTipoId" Text='<%# Bind("TipoId") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataTipoId" runat="server" Display="Dynamic" ControlToValidate="dataTipoId" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataTipoId" runat="server" Display="Dynamic" ControlToValidate="dataTipoId" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


