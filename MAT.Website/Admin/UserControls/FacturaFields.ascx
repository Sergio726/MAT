<%@ Control Language="C#" ClassName="FacturaFields" %>

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
        <td class="literal"><asp:Label ID="lbldataNroFactura" runat="server" Text="Nro Factura:" AssociatedControlID="dataNroFactura" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNroFactura" Text='<%# Bind("NroFactura") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMonto" runat="server" Text="Monto:" AssociatedControlID="dataMonto" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMonto" Text='<%# Bind("Monto") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataMonto" runat="server" Display="Dynamic" ControlToValidate="dataMonto" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFecha" runat="server" Text="Fecha:" AssociatedControlID="dataFecha" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFecha" Text='<%# Bind("Fecha", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFecha" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTipo" runat="server" Text="Tipo:" AssociatedControlID="dataTipo" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTipo" Text='<%# Bind("Tipo") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataTipo" runat="server" Display="Dynamic" ControlToValidate="dataTipo" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataEstado" runat="server" Text="Estado:" AssociatedControlID="dataEstado" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataEstado" Text='<%# Bind("Estado") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataEstado" runat="server" Display="Dynamic" ControlToValidate="dataEstado" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
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
        <td class="literal"><asp:Label ID="lbldataVendedorId" runat="server" Text="Vendedor Id:" AssociatedControlID="dataVendedorId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataVendedorId" DataSourceID="VendedorIdVendedorDataSource" DataTextField="Descripcion" DataValueField="VendedorId" SelectedValue='<%# Bind("VendedorId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:VendedorDataSource ID="VendedorIdVendedorDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDescuentoAplicado" runat="server" Text="Descuento Aplicado:" AssociatedControlID="dataDescuentoAplicado" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDescuentoAplicado" Text='<%# Bind("DescuentoAplicado") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataDescuentoAplicado" runat="server" Display="Dynamic" ControlToValidate="dataDescuentoAplicado" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataDescuentoAplicado" runat="server" Display="Dynamic" ControlToValidate="dataDescuentoAplicado" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataObservaciones" runat="server" Text="Observaciones:" AssociatedControlID="dataObservaciones" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataObservaciones" Text='<%# Bind("Observaciones") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDiasPreReserva" runat="server" Text="Dias Pre Reserva:" AssociatedControlID="dataDiasPreReserva" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDiasPreReserva" Text='<%# Bind("DiasPreReserva") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataDiasPreReserva" runat="server" Display="Dynamic" ControlToValidate="dataDiasPreReserva" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


