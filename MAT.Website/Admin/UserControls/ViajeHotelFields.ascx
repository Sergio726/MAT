<%@ Control Language="C#" ClassName="ViajeHotelFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataViajeHotelId" runat="server" Text="Viaje Hotel Id:" AssociatedControlID="dataViajeHotelId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataViajeHotelId" Value='<%# Bind("ViajeHotelId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataViajeId" runat="server" Text="Viaje Id:" AssociatedControlID="dataViajeId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataViajeId" DataSourceID="ViajeIdViajeDataSource" DataTextField="Origen" DataValueField="ViajeId" SelectedValue='<%# Bind("ViajeId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:ViajeDataSource ID="ViajeIdViajeDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataHotelId" runat="server" Text="Hotel Id:" AssociatedControlID="dataHotelId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataHotelId" DataSourceID="HotelIdHotelDataSource" DataTextField="Nombre" DataValueField="HotelId" SelectedValue='<%# Bind("HotelId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:HotelDataSource ID="HotelIdHotelDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDesde" runat="server" Text="Desde:" AssociatedControlID="dataDesde" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDesde" Text='<%# Bind("Desde") %>' MaxLength="10"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataHasta" runat="server" Text="Hasta:" AssociatedControlID="dataHasta" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataHasta" Text='<%# Bind("Hasta") %>' MaxLength="10"></asp:TextBox>
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
			
		</table>

	</ItemTemplate>
</asp:FormView>


