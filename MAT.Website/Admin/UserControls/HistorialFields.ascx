<%@ Control Language="C#" ClassName="HistorialFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataHistorialId" runat="server" Text="Historial Id:" AssociatedControlID="dataHistorialId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataHistorialId" Value='<%# Bind("HistorialId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTabla" runat="server" Text="Tabla:" AssociatedControlID="dataTabla" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTabla" Text='<%# Bind("Tabla") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataTabla" runat="server" Display="Dynamic" ControlToValidate="dataTabla" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataTabla" runat="server" Display="Dynamic" ControlToValidate="dataTabla" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataOperacion" runat="server" Text="Operacion:" AssociatedControlID="dataOperacion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataOperacion" Text='<%# Bind("Operacion") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataOperacion" runat="server" Display="Dynamic" ControlToValidate="dataOperacion" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataOperacion" runat="server" Display="Dynamic" ControlToValidate="dataOperacion" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFechaHoraRegistro" runat="server" Text="Fecha Hora Registro:" AssociatedControlID="dataFechaHoraRegistro" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFechaHoraRegistro" Text='<%# Bind("FechaHoraRegistro", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFechaHoraRegistro" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" /><asp:RequiredFieldValidator ID="ReqVal_dataFechaHoraRegistro" runat="server" Display="Dynamic" ControlToValidate="dataFechaHoraRegistro" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCliente" runat="server" Text="Cliente:" AssociatedControlID="dataCliente" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataCliente" Value='<%# Bind("Cliente") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataVendedor" runat="server" Text="Vendedor:" AssociatedControlID="dataVendedor" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataVendedor" Value='<%# Bind("Vendedor") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataObservaciones" runat="server" Text="Observaciones:" AssociatedControlID="dataObservaciones" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataObservaciones" Text='<%# Bind("Observaciones") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMonto" runat="server" Text="Monto:" AssociatedControlID="dataMonto" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMonto" Text='<%# Bind("Monto") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataMonto" runat="server" Display="Dynamic" ControlToValidate="dataMonto" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataMonto" runat="server" Display="Dynamic" ControlToValidate="dataMonto" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


