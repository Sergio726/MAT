<%@ Control Language="C#" ClassName="CuentaCorrienteFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataCuentaCorrienteId" runat="server" Text="Cuenta Corriente Id:" AssociatedControlID="dataCuentaCorrienteId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataCuentaCorrienteId" Value='<%# Bind("CuentaCorrienteId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFecha" runat="server" Text="Fecha:" AssociatedControlID="dataFecha" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFecha" Text='<%# Bind("Fecha", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFecha" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMonto" runat="server" Text="Monto:" AssociatedControlID="dataMonto" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMonto" Text='<%# Bind("Monto") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataMonto" runat="server" Display="Dynamic" ControlToValidate="dataMonto" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataClienteId" runat="server" Text="Cliente Id:" AssociatedControlID="dataClienteId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataClienteId" DataSourceID="ClienteIdClienteDataSource" DataTextField="RazonSocial" DataValueField="ClienteId" SelectedValue='<%# Bind("ClienteId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:ClienteDataSource ID="ClienteIdClienteDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


