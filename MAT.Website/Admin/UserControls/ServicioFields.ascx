<%@ Control Language="C#" ClassName="ServicioFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataServicioId" runat="server" Text="Servicio Id:" AssociatedControlID="dataServicioId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataServicioId" Value='<%# Bind("ServicioId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDescripcion" runat="server" Text="Descripcion:" AssociatedControlID="dataDescripcion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDescripcion" Text='<%# Bind("Descripcion") %>' MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataDescripcion" runat="server" Display="Dynamic" ControlToValidate="dataDescripcion" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPrecio" runat="server" Text="Precio:" AssociatedControlID="dataPrecio" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPrecio" Text='<%# Bind("Precio") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataPrecio" runat="server" Display="Dynamic" ControlToValidate="dataPrecio" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMoneda" runat="server" Text="Moneda:" AssociatedControlID="dataMoneda" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMoneda" Text='<%# Bind("Moneda") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataIva" runat="server" Text="Iva:" AssociatedControlID="dataIva" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataIva" Text='<%# Bind("Iva") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataAlicuota" runat="server" Text="Alicuota:" AssociatedControlID="dataAlicuota" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataAlicuota" Text='<%# Bind("Alicuota") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataAlicuota" runat="server" Display="Dynamic" ControlToValidate="dataAlicuota" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataValidez" runat="server" Text="Validez:" AssociatedControlID="dataValidez" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataValidez" Text='<%# Bind("Validez", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataValidez" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataVisibilidadTarifa" runat="server" Text="Visibilidad Tarifa:" AssociatedControlID="dataVisibilidadTarifa" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataVisibilidadTarifa" Text='<%# Bind("VisibilidadTarifa") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataVisibilidadTarifa" runat="server" Display="Dynamic" ControlToValidate="dataVisibilidadTarifa" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataProveedorId" runat="server" Text="Proveedor Id:" AssociatedControlID="dataProveedorId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataProveedorId" DataSourceID="ProveedorIdProveedorDataSource" DataTextField="RazonSocial" DataValueField="ProveedorId" SelectedValue='<%# Bind("ProveedorId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:ProveedorDataSource ID="ProveedorIdProveedorDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTransporteId" runat="server" Text="Transporte Id:" AssociatedControlID="dataTransporteId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataTransporteId" DataSourceID="TransporteIdTransporteDataSource" DataTextField="NroCoche" DataValueField="TransporteId" SelectedValue='<%# Bind("TransporteId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:TransporteDataSource ID="TransporteIdTransporteDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataHotelId" runat="server" Text="Hotel Id:" AssociatedControlID="dataHotelId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataHotelId" DataSourceID="HotelIdHotelDataSource" DataTextField="Nombre" DataValueField="HotelId" SelectedValue='<%# Bind("HotelId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:HotelDataSource ID="HotelIdHotelDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTipoServicio" runat="server" Text="Tipo Servicio:" AssociatedControlID="dataTipoServicio" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTipoServicio" Text='<%# Bind("TipoServicio") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataTipoServicio" runat="server" Display="Dynamic" ControlToValidate="dataTipoServicio" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


