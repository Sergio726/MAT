<%@ Control Language="C#" ClassName="PasajeFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPasajeId" runat="server" Text="Pasaje Id:" AssociatedControlID="dataPasajeId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPasajeId" Value='<%# Bind("PasajeId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPasajeroId" runat="server" Text="Pasajero Id:" AssociatedControlID="dataPasajeroId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPasajeroId" DataSourceID="PasajeroIdPasajeroDataSource" DataTextField="Pasaporte" DataValueField="PasajeroId" SelectedValue='<%# Bind("PasajeroId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:PasajeroDataSource ID="PasajeroIdPasajeroDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataButacaId" runat="server" Text="Butaca Id:" AssociatedControlID="dataButacaId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataButacaId" DataSourceID="ButacaIdButacaDataSource" DataTextField="NroButaca" DataValueField="ButacaId" SelectedValue='<%# Bind("ButacaId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:ButacaDataSource ID="ButacaIdButacaDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFechaReserva" runat="server" Text="Fecha Reserva:" AssociatedControlID="dataFechaReserva" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFechaReserva" Text='<%# Bind("FechaReserva", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFechaReserva" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFechaCompra" runat="server" Text="Fecha Compra:" AssociatedControlID="dataFechaCompra" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFechaCompra" Text='<%# Bind("FechaCompra", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFechaCompra" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataViajeId" runat="server" Text="Viaje Id:" AssociatedControlID="dataViajeId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataViajeId" DataSourceID="ViajeIdViajeDataSource" DataTextField="Origen" DataValueField="ViajeId" SelectedValue='<%# Bind("ViajeId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:ViajeDataSource ID="ViajeIdViajeDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFacturaId" runat="server" Text="Factura Id:" AssociatedControlID="dataFacturaId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataFacturaId" DataSourceID="FacturaIdFacturaDataSource" DataTextField="NroFactura" DataValueField="FacturaId" SelectedValue='<%# Bind("FacturaId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:FacturaDataSource ID="FacturaIdFacturaDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataEstadoPasaje" runat="server" Text="Estado Pasaje:" AssociatedControlID="dataEstadoPasaje" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataEstadoPasaje" DataSourceID="EstadoPasajeEstadoPasajeDataSource" DataTextField="Descripcion" DataValueField="Id" SelectedValue='<%# Bind("EstadoPasaje") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:EstadoPasajeDataSource ID="EstadoPasajeEstadoPasajeDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataVoucherId" runat="server" Text="Voucher Id:" AssociatedControlID="dataVoucherId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataVoucherId" DataSourceID="VoucherIdVoucherDataSource" DataTextField="NroVoucher" DataValueField="VoucherId" SelectedValue='<%# Bind("VoucherId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:VoucherDataSource ID="VoucherIdVoucherDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPrecioId" runat="server" Text="Precio Id:" AssociatedControlID="dataPrecioId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPrecioId" DataSourceID="PrecioIdPrecioDataSource" DataTextField="Monto" DataValueField="PrecioId" SelectedValue='<%# Bind("PrecioId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:PrecioDataSource ID="PrecioIdPrecioDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


