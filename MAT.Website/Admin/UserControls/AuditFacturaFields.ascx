<%@ Control Language="C#" ClassName="AuditFacturaFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataFacturaId" runat="server" Text="Factura Id:" AssociatedControlID="dataFacturaId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataFacturaId" Value='<%# Bind("FacturaId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPersonaId" runat="server" Text="Persona Id:" AssociatedControlID="dataPersonaId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPersonaId" Value='<%# Bind("PersonaId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataVendedorId" runat="server" Text="Vendedor Id:" AssociatedControlID="dataVendedorId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataVendedorId" Value='<%# Bind("VendedorId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataAccion" runat="server" Text="Accion:" AssociatedControlID="dataAccion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataAccion" Text='<%# Bind("Accion") %>' MaxLength="200"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDescripcion" runat="server" Text="Descripcion:" AssociatedControlID="dataDescripcion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDescripcion" Text='<%# Bind("Descripcion") %>' MaxLength="200"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFecha" runat="server" Text="Fecha:" AssociatedControlID="dataFecha" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFecha" Text='<%# Bind("Fecha", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFecha" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" /><asp:RequiredFieldValidator ID="ReqVal_dataFecha" runat="server" Display="Dynamic" ControlToValidate="dataFecha" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


