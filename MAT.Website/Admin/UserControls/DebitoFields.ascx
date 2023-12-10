<%@ Control Language="C#" ClassName="DebitoFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataDebitoId" runat="server" Text="Debito Id:" AssociatedControlID="dataDebitoId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataDebitoId" Value='<%# Bind("DebitoId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFecha" runat="server" Text="Fecha:" AssociatedControlID="dataFecha" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFecha" Text='<%# Bind("Fecha", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFecha" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataClienteId" runat="server" Text="Cliente Id:" AssociatedControlID="dataClienteId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataClienteId" Value='<%# Bind("ClienteId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataVendedorId" runat="server" Text="Vendedor Id:" AssociatedControlID="dataVendedorId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataVendedorId" Value='<%# Bind("VendedorId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMontoDebito" runat="server" Text="Monto Debito:" AssociatedControlID="dataMontoDebito" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMontoDebito" Text='<%# Bind("MontoDebito") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataMontoDebito" runat="server" Display="Dynamic" ControlToValidate="dataMontoDebito" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


