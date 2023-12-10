<%@ Control Language="C#" ClassName="EstadoPasajeFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataDescripcion" runat="server" Text="Descripcion:" AssociatedControlID="dataDescripcion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDescripcion" Text='<%# Bind("Descripcion") %>' MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataDescripcion" runat="server" Display="Dynamic" ControlToValidate="dataDescripcion" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


