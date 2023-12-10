<%@ Control Language="C#" ClassName="NotaFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataNotaId" runat="server" Text="Nota Id:" AssociatedControlID="dataNotaId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataNotaId" Value='<%# Bind("NotaId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPorcentajeRetencion" runat="server" Text="Porcentaje Retencion:" AssociatedControlID="dataPorcentajeRetencion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPorcentajeRetencion" Text='<%# Bind("PorcentajeRetencion") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataPorcentajeRetencion" runat="server" Display="Dynamic" ControlToValidate="dataPorcentajeRetencion" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMontoRetencion" runat="server" Text="Monto Retencion:" AssociatedControlID="dataMontoRetencion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMontoRetencion" Text='<%# Bind("MontoRetencion") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataMontoRetencion" runat="server" Display="Dynamic" ControlToValidate="dataMontoRetencion" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFecha" runat="server" Text="Fecha:" AssociatedControlID="dataFecha" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFecha" Text='<%# Bind("Fecha", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFecha" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDias" runat="server" Text="Dias:" AssociatedControlID="dataDias" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDias" Text='<%# Bind("Dias") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataDias" runat="server" Display="Dynamic" ControlToValidate="dataDias" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataClienteId" runat="server" Text="Cliente Id:" AssociatedControlID="dataClienteId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataClienteId" DataSourceID="ClienteIdClienteDataSource" DataTextField="RazonSocial" DataValueField="ClienteId" SelectedValue='<%# Bind("ClienteId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:ClienteDataSource ID="ClienteIdClienteDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataVendedorId" runat="server" Text="Vendedor Id:" AssociatedControlID="dataVendedorId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataVendedorId" DataSourceID="VendedorIdVendedorDataSource" DataTextField="Descripcion" DataValueField="VendedorId" SelectedValue='<%# Bind("VendedorId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:VendedorDataSource ID="VendedorIdVendedorDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNroNota" runat="server" Text="Nro Nota:" AssociatedControlID="dataNroNota" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNroNota" Text='<%# Bind("NroNota") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMontoNota" runat="server" Text="Monto Nota:" AssociatedControlID="dataMontoNota" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMontoNota" Text='<%# Bind("MontoNota") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataMontoNota" runat="server" Display="Dynamic" ControlToValidate="dataMontoNota" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


