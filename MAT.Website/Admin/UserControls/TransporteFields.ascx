<%@ Control Language="C#" ClassName="TransporteFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataTransporteId" runat="server" Text="Transporte Id:" AssociatedControlID="dataTransporteId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataTransporteId" Value='<%# Bind("TransporteId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNroCoche" runat="server" Text="Nro Coche:" AssociatedControlID="dataNroCoche" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNroCoche" Text='<%# Bind("NroCoche") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMaxPasajeros" runat="server" Text="Max Pasajeros:" AssociatedControlID="dataMaxPasajeros" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMaxPasajeros" Text='<%# Bind("MaxPasajeros") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataMaxPasajeros" runat="server" Display="Dynamic" ControlToValidate="dataMaxPasajeros" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataKmRecorridos" runat="server" Text="Km Recorridos:" AssociatedControlID="dataKmRecorridos" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataKmRecorridos" Text='<%# Bind("KmRecorridos") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataKmRecorridos" runat="server" Display="Dynamic" ControlToValidate="dataKmRecorridos" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataUltimoService" runat="server" Text="Ultimo Service:" AssociatedControlID="dataUltimoService" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataUltimoService" Text='<%# Bind("UltimoService", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataUltimoService" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMatricula" runat="server" Text="Matricula:" AssociatedControlID="dataMatricula" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMatricula" Text='<%# Bind("Matricula") %>' MaxLength="10"></asp:TextBox>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


