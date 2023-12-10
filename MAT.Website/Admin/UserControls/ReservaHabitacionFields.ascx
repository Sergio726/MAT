<%@ Control Language="C#" ClassName="ReservaHabitacionFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataReservaHabitacionId" runat="server" Text="Reserva Habitacion Id:" AssociatedControlID="dataReservaHabitacionId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataReservaHabitacionId" Value='<%# Bind("ReservaHabitacionId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataHabitacionId" runat="server" Text="Habitacion Id:" AssociatedControlID="dataHabitacionId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataHabitacionId" DataSourceID="HabitacionIdHabitacionDataSource" DataTextField="NroHabitacion" DataValueField="HabitacionId" SelectedValue='<%# Bind("HabitacionId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:HabitacionDataSource ID="HabitacionIdHabitacionDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPasajeId" runat="server" Text="Pasaje Id:" AssociatedControlID="dataPasajeId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPasajeId" DataSourceID="PasajeIdPasajeDataSource" DataTextField="FechaReserva" DataValueField="PasajeId" SelectedValue='<%# Bind("PasajeId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:PasajeDataSource ID="PasajeIdPasajeDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFechaReserva" runat="server" Text="Fecha Reserva:" AssociatedControlID="dataFechaReserva" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFechaReserva" Text='<%# Bind("FechaReserva", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFechaReserva" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDesde" runat="server" Text="Desde:" AssociatedControlID="dataDesde" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDesde" Text='<%# Bind("Desde", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataDesde" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataHasta" runat="server" Text="Hasta:" AssociatedControlID="dataHasta" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataHasta" Text='<%# Bind("Hasta", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataHasta" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataExpiro" runat="server" Text="Expiro:" AssociatedControlID="dataExpiro" /></td>
        <td>
					<asp:RadioButtonList runat="server" ID="dataExpiro" SelectedValue='<%# Bind("Expiro") %>' RepeatDirection="Horizontal"><asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem><asp:ListItem Value="False" Text="No"></asp:ListItem></asp:RadioButtonList>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataHoraIngreso" runat="server" Text="Hora Ingreso:" AssociatedControlID="dataHoraIngreso" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataHoraIngreso" Text='<%# Bind("HoraIngreso") %>' MaxLength="10"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataHoraSalida" runat="server" Text="Hora Salida:" AssociatedControlID="dataHoraSalida" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataHoraSalida" Text='<%# Bind("HoraSalida") %>' MaxLength="10"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPasajeroId" runat="server" Text="Pasajero Id:" AssociatedControlID="dataPasajeroId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataPasajeroId" DataSourceID="PasajeroIdPersonaDataSource" DataTextField="Apellido" DataValueField="PersonaId" SelectedValue='<%# Bind("PasajeroId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:PersonaDataSource ID="PasajeroIdPersonaDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataViajeId" runat="server" Text="Viaje Id:" AssociatedControlID="dataViajeId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataViajeId" Value='<%# Bind("ViajeId") %>'></asp:HiddenField>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


