<%@ Control Language="C#" ClassName="ViajeFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataViajeId" runat="server" Text="Viaje Id:" AssociatedControlID="dataViajeId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataViajeId" Value='<%# Bind("ViajeId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaqueteId" runat="server" Text="Paquete Id:" AssociatedControlID="dataPaqueteId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPaqueteId" DataSourceID="PaqueteIdPaqueteDataSource" DataTextField="Descripcion" DataValueField="PaqueteId" SelectedValue='<%# Bind("PaqueteId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:PaqueteDataSource ID="PaqueteIdPaqueteDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataOrigen" runat="server" Text="Origen:" AssociatedControlID="dataOrigen" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataOrigen" Text='<%# Bind("Origen") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFechaSalida" runat="server" Text="Fecha Salida:" AssociatedControlID="dataFechaSalida" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFechaSalida" Text='<%# Bind("FechaSalida", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFechaSalida" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataHoraSalida" runat="server" Text="Hora Salida:" AssociatedControlID="dataHoraSalida" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataHoraSalida" Text='<%# Bind("HoraSalida") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaisOrigen" runat="server" Text="Pais Origen:" AssociatedControlID="dataPaisOrigen" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPaisOrigen" Text='<%# Bind("PaisOrigen") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaisDestino" runat="server" Text="Pais Destino:" AssociatedControlID="dataPaisDestino" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPaisDestino" Text='<%# Bind("PaisDestino") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaso" runat="server" Text="Paso:" AssociatedControlID="dataPaso" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPaso" Text='<%# Bind("Paso") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMedio" runat="server" Text="Medio:" AssociatedControlID="dataMedio" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMedio" Text='<%# Bind("Medio") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataBusId" runat="server" Text="Bus Id:" AssociatedControlID="dataBusId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataBusId" DataSourceID="BusIdTransporteDataSource" DataTextField="NroCoche" DataValueField="TransporteId" SelectedValue='<%# Bind("BusId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:TransporteDataSource ID="BusIdTransporteDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFechaRegreso" runat="server" Text="Fecha Regreso:" AssociatedControlID="dataFechaRegreso" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFechaRegreso" Text='<%# Bind("FechaRegreso", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFechaRegreso" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataHoraRegreso" runat="server" Text="Hora Regreso:" AssociatedControlID="dataHoraRegreso" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataHoraRegreso" Text='<%# Bind("HoraRegreso") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDescripcion" runat="server" Text="Descripcion:" AssociatedControlID="dataDescripcion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDescripcion" Text='<%# Bind("Descripcion") %>' MaxLength="200"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPrecioSemicama" runat="server" Text="Precio Semicama:" AssociatedControlID="dataPrecioSemicama" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPrecioSemicama" Text='<%# Bind("PrecioSemicama") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataPrecioSemicama" runat="server" Display="Dynamic" ControlToValidate="dataPrecioSemicama" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPrecioCama" runat="server" Text="Precio Cama:" AssociatedControlID="dataPrecioCama" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPrecioCama" Text='<%# Bind("PrecioCama") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataPrecioCama" runat="server" Display="Dynamic" ControlToValidate="dataPrecioCama" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPrecioPromocional" runat="server" Text="Precio Promocional:" AssociatedControlID="dataPrecioPromocional" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPrecioPromocional" Text='<%# Bind("PrecioPromocional") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataPrecioPromocional" runat="server" Display="Dynamic" ControlToValidate="dataPrecioPromocional" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFechaPromocion" runat="server" Text="Fecha Promocion:" AssociatedControlID="dataFechaPromocion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFechaPromocion" Text='<%# Bind("FechaPromocion", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFechaPromocion" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNDias" runat="server" Text="N Dias:" AssociatedControlID="dataNDias" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNDias" Text='<%# Bind("NDias") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataNDias" runat="server" Display="Dynamic" ControlToValidate="dataNDias" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNNoches" runat="server" Text="N Noches:" AssociatedControlID="dataNNoches" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNNoches" Text='<%# Bind("NNoches") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataNNoches" runat="server" Display="Dynamic" ControlToValidate="dataNNoches" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


